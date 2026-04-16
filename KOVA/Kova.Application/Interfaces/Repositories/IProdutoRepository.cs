using Kova.Domain.Entities;
 
namespace Kova.Application.Interfaces.Repositories;
 
public interface IProdutoRepository

{

    IReadOnlyCollection<Produto> GetAll();

    Produto? GetById(Guid id);

    void Add(Produto produto);

    void Update(Produto produto);

    void Delete(Produto produto);

    void SaveChanges();

}