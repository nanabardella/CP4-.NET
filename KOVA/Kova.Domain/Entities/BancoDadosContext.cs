using Kova.Domain.Commons;

namespace Kova.Domain.Entities;

public class BancoDadosContext : BaseEntity
{
    private const int MinLength = 2;
    private const int MaxDescricaoLength = 100;

    public string Nome { get; private set; }

    public string Descricao { get; private set; }

    public List<Produto> Produtos { get; private set; } = new();

    public BancoDadosContext(string nome, string descricao)
    {
        UpdateNome(nome);
        UpdateDescricao(descricao);
    }

    private BancoDadosContext() { } 

    public void UpdateNome(string nome)
    {
        if (string.IsNullOrEmpty(nome))
            throw new Exception("Nome está vazio");

        Nome = nome.Length switch
        {
            < MinLength => throw new Exception("Nome deve ter no mínimo 2 caracteres"),
            _ => nome.Trim()
        };
    }

    public void UpdateDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new Exception("Descrição está vazia");

        Descricao = descricao.Length switch
        {
            < 10 => throw new Exception("Descrição deve ter no mínimo 10 caracteres"),
            > MaxDescricaoLength => throw new Exception("Descrição muito longa"),
            _ => descricao.Trim()
        };
    }

    public void Update(string nome, string descricao)
    {
        UpdateNome(nome);
        UpdateDescricao(descricao);
    }

    public override string ToString()
    {
        return $"Categoria: {Nome} - {Descricao}";
    }
}