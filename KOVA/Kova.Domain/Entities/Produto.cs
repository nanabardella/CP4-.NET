using Kova.Domain.Commons;
using Kova.Domain.Exceptions;

namespace Kova.Domain.Entities;

public class Produto : BaseEntity
{
    public string Nome { get; private set; }

    public string Descricao { get; private set; }

    public decimal Preco { get; private set; }

    public Guid CategoriaId { get; private set; }

    public Categoria Categoria { get; private set; }

    public List<Pedido> Pedidos { get; private set; } = new();

    public Produto(string nome, string descricao, decimal preco, Guid categoriaId)
    {
        if (nome == null || nome.Length < 2)
            throw new DomainException("Nome do produto inválido.");

        Nome = nome;

        if (descricao == null || descricao.Length < 10)
            throw new DomainException("Descrição do produto inválida.");

        Descricao = descricao;

        Preco = preco;

        CategoriaId = categoriaId;
    }
}
