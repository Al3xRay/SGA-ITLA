using Microsoft.EntityFrameworkCore.Storage;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Personas;
using SGA.Persistence.Repositories.Transporte;
using SGA.Persistence.Repositories.Operaciones;
using SGA.Persistence.Repositories.Configuracion;

namespace SGA.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        private PersonaRepository? _personaRepository;
        private EstudianteRepository? _estudianteRepository;
        private ConductorRepository? _conductorRepository;
        private EmpleadoRepository? _empleadoRepository;
        private AdministradorRepository? _administradorRepository;

        private AutobusRepository? _autobusRepository;
        private RutaRepository? _rutaRepository;
        private ParadaRepository? _paradaRepository;
        private HorarioRepository? _horarioRepository;
        private ViajeRepository? _viajeRepository;

        private AutorizacionRepository? _autorizacionRepository;
        private RegistroUsoRepository? _registroUsoRepository;
        private IncidenciaRepository? _incidenciaRepository;
        private TransaccionFinancieraRepository? _transaccionFinancieraRepository;

        private EstadoAutobusRepository? _estadoAutobusRepository;
        private EstadoIncidenciaRepository? _estadoIncidenciaRepository;
        private EstadoViajeRepository? _estadoViajeRepository;
        private TipoAutorizacionRepository? _tipoAutorizacionRepository;
        private TipoIncidenciaRepository? _tipoIncidenciaRepository;
        private TipoPersonaRepository? _tipoPersonaRepository;
        private TipoRegistroRepository? _tipoRegistroRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPersonaRepository Personas => _personaRepository ??= new PersonaRepository(_context);
        public IEstudianteRepository Estudiantes => _estudianteRepository ??= new EstudianteRepository(_context);
        public IConductorRepository Conductores => _conductorRepository ??= new ConductorRepository(_context);
        public IEmpleadoRepository Empleados => _empleadoRepository ??= new EmpleadoRepository(_context);
        public IAdministradorRepository Administradores => _administradorRepository ??= new AdministradorRepository(_context);

        public IAutobusRepository Autobuses => _autobusRepository ??= new AutobusRepository(_context);
        public IRutaRepository Rutas => _rutaRepository ??= new RutaRepository(_context);
        public IParadaRepository Paradas => _paradaRepository ??= new ParadaRepository(_context);
        public IHorarioRepository Horarios => _horarioRepository ??= new HorarioRepository(_context);
        public IViajeRepository Viajes => _viajeRepository ??= new ViajeRepository(_context);

        public IAutorizacionRepository Autorizaciones => _autorizacionRepository ??= new AutorizacionRepository(_context);
        public IRegistroUsoRepository RegistrosUso => _registroUsoRepository ??= new RegistroUsoRepository(_context);
        public IIncidenciaRepository Incidencias => _incidenciaRepository ??= new IncidenciaRepository(_context);
        public ITransaccionFinancieraRepository TransaccionesFinanciera => _transaccionFinancieraRepository ??= new TransaccionFinancieraRepository(_context);

        public EstadoAutobusRepository EstadoAutobuses => _estadoAutobusRepository ??= new EstadoAutobusRepository(_context);
        public EstadoIncidenciaRepository EstadoIncidencias => _estadoIncidenciaRepository ??= new EstadoIncidenciaRepository(_context);
        public EstadoViajeRepository EstadoViajes => _estadoViajeRepository ??= new EstadoViajeRepository(_context);
        public TipoAutorizacionRepository TipoAutorizaciones => _tipoAutorizacionRepository ??= new TipoAutorizacionRepository(_context);
        public TipoIncidenciaRepository TipoIncidencias => _tipoIncidenciaRepository ??= new TipoIncidenciaRepository(_context);
        public TipoPersonaRepository TipoPersonas => _tipoPersonaRepository ??= new TipoPersonaRepository(_context);
        public TipoRegistroRepository TipoRegistros => _tipoRegistroRepository ??= new TipoRegistroRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _context.SaveChangesAsync(ct);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            await _context.DisposeAsync();
        }
    }
}
