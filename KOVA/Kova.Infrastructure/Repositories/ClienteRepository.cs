using Microsoft.EntityFrameworkCore;

using Kova.Application.Interfaces.Repositories;

using Kova.Domain.Entities;

using Kova.Infrastructure.Persistence;
 
namespace Kova.Infrastructure.Repositories;
 
public class ClienteRepository(KovaDbContext context) : IClienteRepository

{

    public IReadOnlyCollection<Cliente> GetAll()

    {

        return context.Clientes

            .Include(x => x.Pedidos)

            .ToList();

    }
 
    public Cliente? GetById(Guid id)

    {

        return context.Clientes

            .Include(x => x.Pedidos)

            .FirstOrDefault(x => x.Id == id);

    }
 
    public void Add(Cliente cliente)

    {

        context.Clientes.Add(cliente);

    }
 
    public void Update(Cliente cliente)

    {

        context.Clientes.Update(cliente);

    }
 
    public void Delete(Cliente cliente)

    {

        context.Clientes.Remove(cliente);

    }
 
    public void SaveChanges()

    {

        context.SaveChanges();

    }

}