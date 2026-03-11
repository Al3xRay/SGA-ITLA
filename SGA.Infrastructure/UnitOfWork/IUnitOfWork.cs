using SGA.Infrastructure.Repositories.Operaciones;
using SGA.Persistence.Repositories.Configuracion;
using SGA.Persistence.Repositories.Operaciones;
using SGA.Persistence.Repositories.Personas;
using SGA.Persistence.Repositories.Transporte;

namespace SGA.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        // Repositorios de Personas
        PersonaRepository Personas { get; }
        EstudianteRepository Estudiantes { get; }
        ConductorRepository Conductores { get; }
        EmpleadoRepository Empleados { get; }
        AdministradorRepository Administradores { get; }

        // Repositorios de Transporte
        AutobusRepository Autobuses { get; }
        RutaRepository Rutas { get; }
        ParadaRepository Paradas { get; }
        HorarioRepository Horarios { get; }
        ViajeRepository Viajes { get; }

        // Repositorios de Operaciones
        AutorizacionRepository Autorizaciones { get; }
        RegistroUsoRepository RegistrosUso { get; }
        IncidenciaRepository Incidencias { get; }

        // Repositorios de Configuración
        EstadoAutobusRepository EstadoAutobuses { get; }
        EstadoConductorRepository EstadoConductores { get; }
        EstadoIncidenciaRepository EstadoIncidencias { get; }
        EstadoViajeRepository EstadoViajes { get; }
        TipoAutorizacionRepository TipoAutorizaciones { get; }
        TipoIncidenciaRepository TipoIncidencias { get; }
        TipoPersonaRepository TipoPersonas { get; }
        TipoRegistroRepository TipoRegistros { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync();

        Task CommitAsync();

        Task RollbackAsync();
    }
}
