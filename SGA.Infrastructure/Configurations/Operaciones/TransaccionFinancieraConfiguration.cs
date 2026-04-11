using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Operaciones;

namespace SGA.Persistence.Configurations.Operaciones
{
    public class TransaccionFinancieraConfiguration : BaseEntityConfiguration<TransaccionFinanciera>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<TransaccionFinanciera> builder)
        {
            builder.ToTable("TransaccionesFinanciera");

            builder.Property(e => e.Concepto).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Tipo).IsRequired().HasMaxLength(20);
            builder.Property(e => e.MetodoPago).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Referencia).HasMaxLength(255);
            builder.Property(e => e.Monto).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Fecha).IsRequired();

            builder.HasOne(e => e.ProcesadoPor)
                .WithMany()
                .HasForeignKey(e => e.ProcesadoPorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
