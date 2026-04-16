using Kova.Application.Interfaces.Repositories;

using Kova.Domain.Entities;

using Kova.Infrastructure.Persistence;
 
namespace Kova.Infrastructure.Repositories;
 
public class PagamentoRepository(KovaDbContext context) : IPagamentoRepository

{

    public IReadOnlyCollection<Pagamento> GetAll()

    {

        return context.Pagamentos.ToList();

    }
 
    public Pagamento? GetById(Guid id)

    {

        return context.Pagamentos.FirstOrDefault(x => x.Id == id);

    }
 
    public void Add(Pagamento pagamento)

    {

        context.Pagamentos.Add(pagamento);

    }
 
    public void Update(Pagamento pagamento)

    {

        context.Pagamentos.Update(pagamento);

    }
 
    public void Delete(Pagamento pagamento)

    {

        context.Pagamentos.Remove(pagamento);

    }
 
    public void SaveChanges()

    {

        context.SaveChanges();

    }

}