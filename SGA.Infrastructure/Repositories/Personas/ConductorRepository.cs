using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Personas;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Personas
{
    public class ConductorRepository : BaseRepository<Conductor>
    {
        public ConductorRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Conductor?> GetByLicenciaAsync(string licencia)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.LicenciaConducir == licencia);
        }
        public async Task<IReadOnlyList<Conductor>> GetWithExpiringLicenseAsync(int daysUntilExpiry)
        {
            var expiryDate = DateTime.UtcNow.AddDays(daysUntilExpiry);
            return await _dbSet
                .Where(e => e.FechaVencimientoLicencia <= expiryDate)
                .ToListAsync();
        }
    }
}
