using Kova.Domain.Entities;
using Microsoft.EntityFrameworkCore;
 
namespace Kova.Infrastructure.Persistence;
 
public class KovaDbContext(DbContextOptions<KovaDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KovaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}