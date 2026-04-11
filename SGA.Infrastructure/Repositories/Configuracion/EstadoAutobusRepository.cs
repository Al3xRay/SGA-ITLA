using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class EstadoAutobusRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<EstadoAutobus> _dbSet;

        public EstadoAutobusRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<EstadoAutobus>();
        }

        public virtual async Task<EstadoAutobus?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<EstadoAutobus>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(EstadoAutobus entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(EstadoAutobus entity) => _dbSet.Update(entity);
        public virtual void Delete(EstadoAutobus entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
