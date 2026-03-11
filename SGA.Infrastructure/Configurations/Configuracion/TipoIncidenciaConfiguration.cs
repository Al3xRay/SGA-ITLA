using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Configuracion;

namespace SGA.Persistence.Configurations.Configuracion
{
    public class TipoIncidenciaConfiguration : IEntityTypeConfiguration<TipoIncidencia>
    {
        public void Configure(EntityTypeBuilder<TipoIncidencia> builder)
        {
            builder.ToTable("TipoIncidencias");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(255);
        }
    }
}
