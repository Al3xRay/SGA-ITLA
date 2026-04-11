using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;

namespace SGA.Persistence.Repositories.Operaciones
{
    public class IncidenciaRepository : IIncidenciaRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<Incidencia> _dbSet;

        public IncidenciaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Incidencia>();
        }

        public virtual async Task<Incidencia?> GetByIdAsync(int id) 
        {
            return await _dbSet
                .Include(i => i.Viaje)
                .Include(i => i.Tipo)
                .Include(i => i.Estado)
                .Include(i => i.ReportadoPor)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public virtual async Task<IReadOnlyList<Incidencia>> GetAllAsync()
        {
            return await _dbSet
                .Include(i => i.Viaje)
                .Include(i => i.Tipo)
                .Include(i => i.Estado)
                .Include(i => i.ReportadoPor)
                .ToListAsync();
        }

        public virtual async Task AddAsync(Incidencia entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(Incidencia entity) => _dbSet.Update(entity);
        public virtual void Delete(Incidencia entity) => _dbSet.Remove(entity);
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);

        public async Task<IReadOnlyList<Incidencia>> GetByViajeAsync(int viajeId)
        {
            return await _dbSet
                .Include(i => i.Viaje)
                .Include(i => i.Tipo)
                .Include(i => i.Estado)
                .Include(i => i.ReportadoPor)
                .Where(i => i.ViajeId == viajeId)
                .ToListAsync();
        }
    }
}
