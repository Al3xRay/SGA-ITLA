using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGA.Domain.Base;

namespace SGA.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : AuditEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.FechaCreacion)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(e => e.FechaModificacion)
                .HasColumnType("datetime2");

            builder.Property(e => e.Activo)
                .IsRequired();
            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}
