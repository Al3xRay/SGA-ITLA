using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class TipoPersonaRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TipoPersona> _dbSet;

        public TipoPersonaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TipoPersona>();
        }

        public virtual async Task<TipoPersona?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<TipoPersona>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(TipoPersona entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(TipoPersona entity) => _dbSet.Update(entity);
        public virtual void Delete(TipoPersona entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
