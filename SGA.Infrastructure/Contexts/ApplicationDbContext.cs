using Microsoft.EntityFrameworkCore;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Personas;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Entidades.Configuracion;
using System.Reflection;

namespace SGA.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Entidades de Personas 
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        // Entidades de Transporte
        public DbSet<Autobus> Autobuses { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Parada> Paradas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Viaje> Viajes { get; set; }

        // Entidades de Operaciones 
        public DbSet<Autorizacion> Autorizaciones { get; set; }
        public DbSet<RegistroUso> RegistrosUso { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }

        // Tablas de Configuración y Catalogo
        public DbSet<EstadoAutobus> EstadoAutobuses { get; set; }
        public DbSet<EstadoConductor> EstadoConductores { get; set; }
        public DbSet<EstadoIncidencia> EstadoIncidencias { get; set; }
        public DbSet<EstadoViaje> EstadoViajes { get; set; }
        public DbSet<TipoAutorizacion> TipoAutorizaciones { get; set; }
        public DbSet<TipoIncidencia> TipoIncidencias { get; set; }
        public DbSet<TipoPersona> TipoPersonas { get; set; }
        public DbSet<TipoRegistro> TipoRegistros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsAssignableTo(typeof(AuditEntity))
                    && entityType.BaseType == null)
                {
                    var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType);
                    var property = System.Linq.Expressions.Expression.PropertyOrField(parameter, "Activo");
                    var body = System.Linq.Expressions.Expression.Equal(property,

                    System.Linq.Expressions.Expression.Constant(true));
                    var lambda = System.Linq.Expressions.Expression.Lambda(body, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var auditedEntries = ChangeTracker.Entries<AuditEntity>();

            foreach (var entry in auditedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.FechaCreacion = now;
                        entry.Entity.Activo = true;
                        break;

                    case EntityState.Modified:
                        entry.Entity.FechaModificacion = now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.Activo = false;
                        entry.Entity.FechaModificacion = now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}