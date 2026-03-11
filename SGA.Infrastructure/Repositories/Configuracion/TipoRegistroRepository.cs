using SGA.Domain.Entidades.Configuracion;
using SGA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SGA.Persistence.Repositories.Configuracion
{
    public class TipoRegistroRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TipoRegistro> _dbSet;

        public TipoRegistroRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TipoRegistro>();
        }

        public virtual async Task<TipoRegistro?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<TipoRegistro>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(TipoRegistro entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(TipoRegistro entity) => _dbSet.Update(entity);
        public virtual void Delete(TipoRegistro entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}
