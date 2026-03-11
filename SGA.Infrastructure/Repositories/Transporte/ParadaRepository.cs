using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Transporte;
using SGA.Persistence.Contexts;

namespace SGA.Persistence.Repositories.Transporte
{
    public class ParadaRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<Parada> _dbSet;

        public ParadaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Parada>();
        }

        public virtual async Task<Parada?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IReadOnlyList<Parada>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(Parada entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(Parada entity) => _dbSet.Update(entity);
        public virtual void Delete(Parada entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
    }
}

