using Kova.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace Kova.Infrastructure.Persistence.Configurations;
 
public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>

{

    public void Configure(EntityTypeBuilder<Pagamento> builder)

    {

        builder.ToTable("Pagamentos");
 
        builder.HasKey(x => x.Id);
 
        builder.Property(x => x.Status)

            .IsRequired()

            .HasConversion<string>()

            .HasMaxLength(50);
 
        builder.Property(x => x.Tipo)

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

    }

}