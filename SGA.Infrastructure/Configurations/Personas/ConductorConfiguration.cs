using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Personas;

namespace SGA.Persistence.Configurations.Personas
{
    public class ConductorConfiguration : BaseEntityConfiguration<Conductor>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Conductor> builder)
        {
            builder.ToTable("Conductores");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.LicenciaConducir)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.FechaVencimientoLicencia)
                .HasColumnType("date")
                .IsRequired();

            builder.HasIndex(e => e.LicenciaConducir)
                .IsUnique();
        }
    }
}
