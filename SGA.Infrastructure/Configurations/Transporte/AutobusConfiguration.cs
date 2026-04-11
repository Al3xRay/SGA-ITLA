using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Transporte;

namespace SGA.Persistence.Configurations.Transporte
{
    public class AutobusConfiguration : BaseEntityConfiguration<Autobus>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Autobus> builder)
        {
            builder.ToTable("Autobuses");

            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.Placa)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Marca)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Modelo)
                .HasMaxLength(100);

            builder.Property(e => e.Capacidad)
                .IsRequired();

            // RESTRICCION: Capacidad > 0
            builder.HasCheckConstraint("CK_Autobus_Capacidad", "[Capacidad] > 0");

            // Relaciones
            builder.HasOne(e => e.Estado)
                .WithMany()
                .HasForeignKey(e => e.EstadoAutobusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice
            builder.HasIndex(e => e.Placa)
                .IsUnique();
        }
    }
}
