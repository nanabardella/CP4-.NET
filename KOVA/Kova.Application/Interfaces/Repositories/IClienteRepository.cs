using Kova.Domain.Entities;
 
namespace Kova.Application.Interfaces.Repositories;
 
public interface IClienteRepository
{
    IReadOnlyCollection<Cliente> GetAll();
    Cliente? GetById(Guid id);
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(Cliente cliente);
    void SaveChanges();
}