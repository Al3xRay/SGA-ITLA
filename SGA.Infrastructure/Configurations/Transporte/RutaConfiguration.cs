using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Transporte;

namespace SGA.Persistence.Configurations.Transporte
{
    public class RutaConfiguration : BaseEntityConfiguration<Ruta>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Ruta> builder)
        {
            builder.ToTable("Rutas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(500);

            builder.HasMany(e => e.Paradas)
                .WithOne(p => p.Ruta)
                .HasForeignKey(p => p.RutaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
