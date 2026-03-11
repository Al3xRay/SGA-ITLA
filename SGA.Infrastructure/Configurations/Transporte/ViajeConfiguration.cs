using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Transporte;

namespace SGA.Persistence.Configurations.Transporte
{
    public class ViajeConfiguration : BaseEntityConfiguration<Viaje>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Viaje> builder)
        {
            builder.ToTable("Viajes");

            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.FechaProgramada)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(e => e.HoraInicioReal)
                .HasColumnType("datetime2");

            builder.Property(e => e.HoraFinReal)
                .HasColumnType("datetime2");

            builder.Property(e => e.OcupacionActual)
                .IsRequired();

            // RESTRICCIÓN: OcupacionActual >= 0
            builder.HasCheckConstraint("CK_Viaje_OcupacionActual", "[OcupacionActual] >= 0");

            builder.Property(e => e.Observaciones)
                .HasMaxLength(500);

            // Relaciones
            builder.HasOne(e => e.Ruta)
                .WithMany()
                .HasForeignKey(e => e.RutaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Autobus)
                .WithMany()
                .HasForeignKey(e => e.AutobusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Conductor)
                .WithMany()
                .HasForeignKey(e => e.ConductorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Horario)
                .WithMany()
                .HasForeignKey(e => e.HorarioId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Estado)
                .WithMany()
                .HasForeignKey(e => e.EstadoViajeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
