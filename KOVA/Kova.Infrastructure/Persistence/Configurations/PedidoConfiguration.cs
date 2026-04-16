using Kova.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace Kova.Infrastructure.Persistence.Configurations;
 
public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>

{

    public void Configure(EntityTypeBuilder<Pedido> builder)

    {

        builder.ToTable("Pedidos");
 
        builder.HasKey(x => x.Id);
 
        builder.Property(x => x.ValorTotal)

            .IsRequired()

            .HasColumnType("NUMBER(18,2)");
 
        builder.Property(x => x.Status)

            .IsRequired()

            .HasConversion<string>()

            .HasMaxLength(50);
 
        builder.Property(x => x.CreatedAt)

            .IsRequired();
 
        builder.Property(x => x.Active)

            .HasColumnType("NUMBER(1)")

            .HasConversion(

                v => v ? 1 : 0,

                v => v == 1

            )

            .IsRequired();
 
        builder.HasOne(x => x.Cliente)

            .WithMany(x => x.Pedidos)

            .HasForeignKey(x => x.ClienteId)

            .OnDelete(DeleteBehavior.Restrict);
 
        builder.HasOne(x => x.Pagamento)

            .WithOne()

            .HasForeignKey<Pedido>(x => x.PagamentoId)

            .OnDelete(DeleteBehavior.Restrict);
 
        builder.HasIndex(x => x.PagamentoId)

            .IsUnique();

    }

}