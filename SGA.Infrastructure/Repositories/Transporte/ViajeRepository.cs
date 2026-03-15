using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Transporte
{
    public class ViajeRepository : BaseRepository<Viaje>, IViajeRepository
    {
        public ViajeRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IReadOnlyList<Viaje>> GetViajesByFechaAsync(DateTime fecha)
        {
            var fechaInicio = fecha.Date;
            var fechaFin = fechaInicio.AddDays(1);

            return await _dbSet
                .Where(v => v.FechaProgramada >= fechaInicio && v.FechaProgramada < fechaFin)
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

        public async Task<Viaje?> GetViajeActivoByAutobusAsync(int autobusId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(v => v.AutobusId == autobusId && v.HoraInicioReal != null && v.HoraFinReal == null);
        }
    }
}
