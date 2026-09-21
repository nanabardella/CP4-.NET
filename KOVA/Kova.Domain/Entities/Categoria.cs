using Kova.Domain.Commons;
using Kova.Domain.Exceptions;

namespace Kova.Domain.Entities;

public class Categoria : BaseEntity
{
    public string Nome { get; private set; }

    public string Descricao { get; private set; }

    public List<Produto> Produtos { get; private set; } = new();

    public Categoria(string nome, string descricao)
    {
        if (nome == null || nome.Length < 2)
            throw new DomainException("Nome da categoria inválido.");

        Nome = nome;

        if (descricao == null || descricao.Length < 10)
            throw new DomainException("Descrição da categoria inválida.");

        Descricao = descricao;
    }
}