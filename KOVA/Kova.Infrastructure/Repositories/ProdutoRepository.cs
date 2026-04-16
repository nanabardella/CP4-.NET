using Microsoft.EntityFrameworkCore;

using Kova.Application.Interfaces.Repositories;

using Kova.Domain.Entities;

using Kova.Infrastructure.Persistence;
 
namespace Kova.Infrastructure.Repositories;
 
public class ProdutoRepository(KovaDbContext context) : IProdutoRepository

{

    public IReadOnlyCollection<Produto> GetAll()

    {

        return context.Produtos

            .Include(x => x.Categoria)

            .Include(x => x.Pedidos)

            .ToList();

    }
 
    public Produto? GetById(Guid id)

    {

        return context.Produtos

            .Include(x => x.Categoria)

            .Include(x => x.Pedidos)

            .FirstOrDefault(x => x.Id == id);

    }
 
    public void Add(Produto produto)

    {

        context.Produtos.Add(produto);

    }
 
    public void Update(Produto produto)

    {

        context.Produtos.Update(produto);

    }
 
    public void Delete(Produto produto)

    {

        context.Produtos.Remove(produto);

    }
 
    public void SaveChanges()

    {

        context.SaveChanges();

    }

}