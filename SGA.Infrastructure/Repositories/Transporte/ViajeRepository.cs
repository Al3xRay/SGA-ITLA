using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Transporte;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Transporte
{
    public class ViajeRepository : BaseRepository<Viaje>
    {
        public ViajeRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IReadOnlyList<Viaje>> GetByRutaYFechaAsync(int rutaId, DateTime fecha)
        {
            var fechaInicio = fecha.Date;
            var fechaFin = fechaInicio.AddDays(1);

            return await _dbSet
                .Where(v => v.RutaId == rutaId && v.FechaProgramada >= fechaInicio && v.FechaProgramada < fechaFin)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Viaje>> GetByConductorYFechaAsync(int conductorId, DateTime fecha)
        {
            var fechaInicio = fecha.Date;
            var fechaFin = fechaInicio.AddDays(1);

            return await _dbSet
                .Where(v => v.ConductorId == conductorId && v.FechaProgramada >= fechaInicio && v.FechaProgramada < fechaFin)
                .ToListAsync();
        }
    }
}
