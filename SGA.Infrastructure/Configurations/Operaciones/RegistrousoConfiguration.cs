using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Operaciones;

namespace SGA.Persistence.Configurations.Operaciones
{
    public class RegistroUsoConfiguration : IEntityTypeConfiguration<RegistroUso>
    {
        public void Configure(EntityTypeBuilder<RegistroUso> builder)
        {
            builder.ToTable("RegistrosUso");

            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.FechaHora)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(e => e.AccesoPermitido)
                .IsRequired();

            builder.Property(e => e.MotivoRechazo)
                .HasMaxLength(255);

            // Relaciones
            builder.HasOne(e => e.Persona)
                .WithMany(p => p.RegistrosUso)
                .HasForeignKey(e => e.PersonaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Viaje)
                .WithMany(v => v.RegistrosAbordaje)
                .HasForeignKey(e => e.ViajeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Autorizacion)
                .WithMany(a => a.Consumos)
                .HasForeignKey(e => e.AutorizacionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Tipo)
                .WithMany()
                .HasForeignKey(e => e.TipoRegistroId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ValidadoPor)
                .WithMany()
                .HasForeignKey(e => e.ValidadoPorConductorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
