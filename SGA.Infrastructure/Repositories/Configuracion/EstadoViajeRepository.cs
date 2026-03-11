using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class EstadoViajeRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<EstadoViaje> _dbSet;

        public EstadoViajeRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<EstadoViaje>();
        }

        public virtual async Task<EstadoViaje?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<EstadoViaje>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(EstadoViaje entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(EstadoViaje entity) => _dbSet.Update(entity);
        public virtual void Delete(EstadoViaje entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
