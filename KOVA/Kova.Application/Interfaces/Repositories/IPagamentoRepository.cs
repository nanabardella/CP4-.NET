using Kova.Domain.Entities;
 
namespace Kova.Application.Interfaces.Repositories;
 
public interface IPagamentoRepository
{
    IReadOnlyCollection<Pagamento> GetAll();
    Pagamento? GetById(Guid id);
    void Add(Pagamento pagamento);
    void Update(Pagamento pagamento);
    void Delete(Pagamento pagamento);
    void SaveChanges();
}