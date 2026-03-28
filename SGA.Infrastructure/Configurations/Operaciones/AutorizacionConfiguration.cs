using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Operaciones;

namespace SGA.Persistence.Configurations.Operaciones
{
    public class AutorizacionConfiguration : BaseEntityConfiguration<Autorizacion>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Autorizacion> builder)
        {
            builder.ToTable("Autorizaciones");

            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.FechaEmision)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.FechaVencimiento)
                .HasColumnType("date")
                .IsRequired();

            
            builder.HasCheckConstraint("CK_Autorizacion_Fechas", "[FechaVencimiento] > [FechaEmision]");

            builder.Property(e => e.Saldo)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();


            builder.HasOne(e => e.Persona)
                .WithMany(p => p.Autorizaciones)
                .HasForeignKey(e => e.PersonaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Tipo)
                .WithMany()
                .HasForeignKey(e => e.TipoAutorizacionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
