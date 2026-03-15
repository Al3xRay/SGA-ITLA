using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;

namespace SGA.Persistence.Repositories.Operaciones
{
    public class RegistroUsoRepository : IRegistroUsoRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<RegistroUso> _dbSet;

        public RegistroUsoRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<RegistroUso>();
        }

        public virtual async Task<RegistroUso?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<RegistroUso>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task AddAsync(RegistroUso entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(RegistroUso entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(RegistroUso entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<RegistroUso>> GetByViajeAsync(int viajeId)
        {
            return await _dbSet
                .Where(r => r.ViajeId == viajeId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<RegistroUso>> GetByPersonaAsync(int personaId)
        {
            return await _dbSet
                .Where(r => r.PersonaId == personaId)
                .ToListAsync();
        }
    }
}
