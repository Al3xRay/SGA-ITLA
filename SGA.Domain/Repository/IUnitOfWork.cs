namespace SGA.Domain.Repository;

public interface IUnitOfWork : IAsyncDisposable
{
    // Repositorios de Personas
    IPersonaRepository Personas { get; }
    IEstudianteRepository Estudiantes { get; }
    IConductorRepository Conductores { get; }
    IEmpleadoRepository Empleados { get; }
    IAdministradorRepository Administradores { get; }

    // Repositorios de Transporte
    IAutobusRepository Autobuses { get; }
    IRutaRepository Rutas { get; }
    IParadaRepository Paradas { get; }
    IHorarioRepository Horarios { get; }
    IViajeRepository Viajes { get; }

    // Repositorios de Operaciones
    IAutorizacionRepository Autorizaciones { get; }
    IRegistroUsoRepository RegistrosUso { get; }
    IIncidenciaRepository Incidencias { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
