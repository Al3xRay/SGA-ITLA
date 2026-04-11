using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class TipoAutorizacionRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TipoAutorizacion> _dbSet;

        public TipoAutorizacionRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TipoAutorizacion>();
        }

        public virtual async Task<TipoAutorizacion?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<TipoAutorizacion>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(TipoAutorizacion entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(TipoAutorizacion entity) => _dbSet.Update(entity);
        public virtual void Delete(TipoAutorizacion entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
