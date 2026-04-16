using Kova.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace Kova.Infrastructure.Persistence.Configurations;
 
public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>

{

    public void Configure(EntityTypeBuilder<Produto> builder)

    {

        builder.ToTable("Produtos");
 
        builder.HasKey(x => x.Id);
 
        builder.Property(x => x.Nome)

            .IsRequired()

            .HasMaxLength(120);
 
        builder.Property(x => x.Descricao)

            .IsRequired()

            .HasMaxLength(500);
 
        builder.Property(x => x.Preco)

            .IsRequired()

            .HasColumnType("NUMBER(18,2)");
 
        builder.Property(x => x.CreatedAt)

            .IsRequired();
 
        builder.Property(x => x.Active)

            .HasColumnType("NUMBER(1)")

            .HasConversion(

                v => v ? 1 : 0,

                v => v == 1

            )

            .IsRequired();
 
        builder.HasOne(x => x.Categoria)

            .WithMany(x => x.Produtos)

            .HasForeignKey(x => x.CategoriaId)

            .OnDelete(DeleteBehavior.Restrict);
 
        builder.HasMany(x => x.Pedidos)

            .WithMany(x => x.Produtos)

            .UsingEntity<Dictionary<string, object>>(

                "PedidoProduto",

                right => right

                    .HasOne<Pedido>()

                    .WithMany()

                    .HasForeignKey("PedidoId")

                    .OnDelete(DeleteBehavior.Cascade),

                left => left

                    .HasOne<Produto>()

                    .WithMany()

                    .HasForeignKey("ProdutoId")

                    .OnDelete(DeleteBehavior.Cascade),

                join =>

                {

                    join.ToTable("PedidoProdutos");

                    join.HasKey("PedidoId", "ProdutoId");

                });

    }

}