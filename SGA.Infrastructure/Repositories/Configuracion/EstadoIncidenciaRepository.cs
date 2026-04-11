using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class EstadoIncidenciaRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<EstadoIncidencia> _dbSet;

        public EstadoIncidenciaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<EstadoIncidencia>();
        }

        public virtual async Task<EstadoIncidencia?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<EstadoIncidencia>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(EstadoIncidencia entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(EstadoIncidencia entity) => _dbSet.Update(entity);
        public virtual void Delete(EstadoIncidencia entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
