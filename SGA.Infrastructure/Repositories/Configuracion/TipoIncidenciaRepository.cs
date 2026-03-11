using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class TipoIncidenciaRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TipoIncidencia> _dbSet;

        public TipoIncidenciaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TipoIncidencia>();
        }

        public virtual async Task<TipoIncidencia?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<TipoIncidencia>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(TipoIncidencia entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(TipoIncidencia entity) => _dbSet.Update(entity);
        public virtual void Delete(TipoIncidencia entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
