using Kova.Domain.Entities;
 
namespace Kova.Application.Interfaces.Repositories;
 
public interface IPedidoRepository
{
    IReadOnlyCollection<Pedido> GetAll();
    Pedido? GetById(Guid id);
    void Add(Pedido pedido);
    void Update(Pedido pedido);
    void Delete(Pedido pedido);
    void SaveChanges();
}