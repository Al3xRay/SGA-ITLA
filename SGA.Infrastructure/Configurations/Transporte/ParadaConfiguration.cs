using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Transporte;

namespace SGA.Persistence.Configurations.Transporte
{
    public class ParadaConfiguration : IEntityTypeConfiguration<Parada>
    {
        public void Configure(EntityTypeBuilder<Parada> builder)
        {
            builder.ToTable("Paradas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Ubicacion)
                .HasMaxLength(255);

            builder.Property(e => e.Orden)
                .IsRequired();

            builder.Property(e => e.TiempoDesdeOrigen)
                .HasColumnType("time");

            builder.HasOne(e => e.Ruta)
                .WithMany(r => r.Paradas)
                .HasForeignKey(e => e.RutaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
