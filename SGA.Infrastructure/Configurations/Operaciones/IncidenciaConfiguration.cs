using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Operaciones;

namespace SGA.Persistence.Configurations.Operaciones
{
    public class IncidenciaConfiguration : IEntityTypeConfiguration<Incidencia>
    {
        public void Configure(EntityTypeBuilder<Incidencia> builder)
        {
            builder.ToTable("Incidencias");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(500);

            builder.Property(e => e.FechaReporte)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(e => e.EsGrave)
                .IsRequired();

            builder.Property(e => e.EvidenciaUrl)
                .HasMaxLength(500);

            builder.Property(e => e.Resolucion)
                .HasMaxLength(500);

            builder.Property(e => e.FechaResolucion)
                .HasColumnType("datetime2");

            // Relaciones
            builder.HasOne(e => e.Viaje)
                .WithMany(v => v.Incidencias)
                .HasForeignKey(e => e.ViajeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Tipo)
                .WithMany()
                .HasForeignKey(e => e.TipoIncidenciaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Estado)
                .WithMany()
                .HasForeignKey(e => e.EstadoIncidenciaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ReportadoPor)
                .WithMany()
                .HasForeignKey(e => e.ReportadoPorConductorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
