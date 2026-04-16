using Microsoft.EntityFrameworkCore;

using Kova.Application.Interfaces.Repositories;

using Kova.Domain.Entities;

using Kova.Infrastructure.Persistence;
 
namespace Kova.Infrastructure.Repositories;
 
public class CategoriaRepository(KovaDbContext context) : ICategoriaRepository

{

    public IReadOnlyCollection<Categoria> GetAll()

    {

        return context.Categorias

            .Include(x => x.Produtos)

            .ToList();

    }
 
    public Categoria? GetById(Guid id)

    {

        return context.Categorias

            .Include(x => x.Produtos)

            .FirstOrDefault(x => x.Id == id);

    }
 
    public void Add(Categoria categoria)

    {

        context.Categorias.Add(categoria);

    }
 
    public void Update(Categoria categoria)

    {

        context.Categorias.Update(categoria);

    }
 
    public void Delete(Categoria categoria)

    {

        context.Categorias.Remove(categoria);

    }
 
    public void SaveChanges()

    {

        context.SaveChanges();

    }

}