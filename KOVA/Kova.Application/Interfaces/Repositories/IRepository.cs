using Kova.Domain.Commons;

namespace Kova.Application.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    IReadOnlyCollection<T> GetAll();
    T? GetById(Guid id);
    bool ExistsById(Guid id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SaveChanges();
}
