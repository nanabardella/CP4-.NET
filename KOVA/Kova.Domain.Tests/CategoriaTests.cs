using Kova.Domain.Entities;
using Kova.Domain.Exceptions;
using Xunit;

namespace Kova.Domain.Tests;

public sealed class CategoriaTests
{
    [Fact]
    public void CriacaoComDadosValidos_DevePreservarDados()
    {
        const string nome = "Camisetas";
        const string descricao = "Camisetas e pecas basicas";

        var categoria = new Categoria(nome, descricao);

        Assert.Equal(nome, categoria.Nome);
        Assert.Equal(descricao, categoria.Descricao);
        Assert.True(categoria.Active);
    }

    [Theory]
    [InlineData(null, "Descricao valida para teste")]
    [InlineData("A", "Descricao valida para teste")]
    [InlineData("Categoria valida", null)]
    [InlineData("Categoria valida", "curta")]
    public void CriacaoComDadosInvalidos_DeveLancarExcecaoDeDominio(string? nome, string? descricao)
    {
        Assert.Throws<DomainException>(() => new Categoria(nome!, descricao!));
    }
}
