using Kova.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace Kova.Infrastructure.Persistence.Configurations;
 
public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>

{

    public void Configure(EntityTypeBuilder<Categoria> builder)

    {

        builder.ToTable("Categorias");
 
        builder.HasKey(x => x.Id);
 
        builder.Property(x => x.Nome)

            .IsRequired()

            .HasMaxLength(100);
 
        builder.Property(x => x.Descricao)

            .IsRequired()

            .HasMaxLength(300);
 
        builder.Property(x => x.CreatedAt)

            .IsRequired();
 
        builder.Property(x => x.Active)

            .HasColumnType("NUMBER(1)")

            .HasConversion(

                v => v ? 1 : 0,

                v => v == 1

            )

            .IsRequired();
 
        builder.HasMany(x => x.Produtos)

            .WithOne(x => x.Categoria)

            .HasForeignKey(x => x.CategoriaId)

            .OnDelete(DeleteBehavior.Restrict);

    }

}