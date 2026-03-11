using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Entidades.Personas;

namespace SGA.Persistence.Configurations.Personas
{
    public class PersonaConfiguration : BaseEntityConfiguration<Persona>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Personas");

            // Clave primaria
            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.DocumentoIdentidad)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Direccion)
                .HasMaxLength(255);

            builder.Property(e => e.FechaNacimiento)
                .HasColumnType("date");

            // Relaciones
            builder.HasOne(e => e.Tipo)
                .WithMany()
                .HasForeignKey(e => e.TipoPersonaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(e => e.DocumentoIdentidad)
                .IsUnique();
        }
    }
}
