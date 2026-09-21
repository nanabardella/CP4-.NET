using Kova.Domain.Commons;
using Kova.Domain.Exceptions;

namespace Kova.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Nome { get; private set; }

    public string Email { get; private set; }

    public string Telefone { get; private set; }

    public List<Pedido> Pedidos { get; private set; } = new();

    public Cliente(string nome, string email, string telefone)
    {
        if (nome == null || nome.Length < 2)
            throw new DomainException("Nome do cliente inválido.");

        Nome = nome;

        if (email == null || email.Length < 5)
            throw new DomainException("Email do cliente inválido.");

        Email = email;

        if (telefone == null || telefone.Length != 11)
            throw new DomainException("Telefone do cliente inválido.");

        Telefone = telefone;
    }
}
