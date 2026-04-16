using Kova.Domain.Entities;
 
namespace Kova.Application.Interfaces.Repositories;
 
public interface ICategoriaRepository

{

    IReadOnlyCollection<Categoria> GetAll();

    Categoria? GetById(Guid id);

    void Add(Categoria categoria);

    void Update(Categoria categoria);

    void Delete(Categoria categoria);

    void SaveChanges();

}