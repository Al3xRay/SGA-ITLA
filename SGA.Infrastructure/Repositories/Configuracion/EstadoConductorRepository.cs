using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class EstadoConductorRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<EstadoConductor> _dbSet;

        public EstadoConductorRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<EstadoConductor>();
        }

        public virtual async Task<EstadoConductor?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<EstadoConductor>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(EstadoConductor entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(EstadoConductor entity) => _dbSet.Update(entity);
        public virtual void Delete(EstadoConductor entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
