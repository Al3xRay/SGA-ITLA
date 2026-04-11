using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Personas;

namespace SGA.Persistence.Configurations.Personas
{
    public class EmpleadoConfiguration : BaseEntityConfiguration<Empleado>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleados", t => t.Property(e => e.Id).HasColumnName("PersonaId"));

            builder.Property(e => e.CodigoEmpleado)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Departamento)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Cargo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaContratacion)
                .HasColumnType("date")
                .IsRequired();

            builder.HasIndex(e => e.CodigoEmpleado)
                .IsUnique();
        }
    }
}
