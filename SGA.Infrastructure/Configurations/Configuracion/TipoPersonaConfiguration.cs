using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Configuracion;

namespace SGA.Persistence.Configurations.Configuracion
{
    public class TipoPersonaConfiguration : IEntityTypeConfiguration<TipoPersona>
    {
        public void Configure(EntityTypeBuilder<TipoPersona> builder)
        {
            builder.ToTable("TipoPersonas");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(255);
        }
    }
}
