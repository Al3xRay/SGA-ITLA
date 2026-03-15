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

            builder.Property(e => e.FechaProgramada)
                .HasColumnType("datetime2").IsRequired();
            builder.Property(e => e.HoraInicioReal)
                .HasColumnType("datetime2");
            builder.Property(e => e.HoraFinReal)
                .HasColumnType("datetime2");
            builder.Property(e => e.OcupacionActual).IsRequired();
            builder.HasCheckConstraint("CK_Viaje_OcupacionActual", "[OcupacionActual] >= 0");
            builder.Property(e => e.Observaciones).HasMaxLength(500);

            builder.HasOne(e => e.Ruta)
                .WithMany(r => r.Viajes)
                .HasForeignKey(e => e.RutaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Autobus)
                .WithMany(a => a.Viajes)
                .HasForeignKey(e => e.AutobusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Conductor)
                .WithMany(c => c.ViajesAsignados)
                .HasForeignKey(e => e.ConductorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Horario)
                .WithMany(h => h.Viajes)
                .HasForeignKey(e => e.HorarioId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Estado)
                .WithMany()
                .HasForeignKey(e => e.EstadoViajeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
