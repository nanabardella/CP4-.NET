using Microsoft.EntityFrameworkCore;

using Kova.Application.Interfaces.Repositories;

using Kova.Domain.Entities;

using Kova.Infrastructure.Persistence;
 
namespace Kova.Infrastructure.Repositories;
 
public class PedidoRepository(KovaDbContext context) : IPedidoRepository

{

    public IReadOnlyCollection<Pedido> GetAll()

    {

        return context.Pedidos

            .Include(x => x.Cliente)

            .Include(x => x.Pagamento)

            .Include(x => x.Produtos)

            .ToList();

    }
 
    public Pedido? GetById(Guid id)

    {

        return context.Pedidos

            .Include(x => x.Cliente)

            .Include(x => x.Pagamento)

            .Include(x => x.Produtos)

            .FirstOrDefault(x => x.Id == id);

    }
 
    public void Add(Pedido pedido)

    {

        context.Pedidos.Add(pedido);

    }
 
    public void Update(Pedido pedido)

    {

        context.Pedidos.Update(pedido);

    }
 
    public void Delete(Pedido pedido)

    {

        context.Pedidos.Remove(pedido);

    }
 
    public void SaveChanges()

    {

        context.SaveChanges();

    }

}