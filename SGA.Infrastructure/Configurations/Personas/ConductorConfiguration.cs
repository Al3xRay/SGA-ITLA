using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Personas;

namespace SGA.Persistence.Configurations.Personas
{
    public class ConductorConfiguration : BaseEntityConfiguration<Conductor>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Conductor> builder)
        {
            builder.ToTable("Conductores", t => t.Property(e => e.Id).HasColumnName("PersonaId"));


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
