using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Transporte
{
    public class AutobusRepository : BaseRepository<Autobus>, IAutobusRepository
    {
        public AutobusRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Autobus?> GetByPlacaAsync(string placa)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Placa == placa);
        }

        public async Task<IReadOnlyList<Autobus>> GetByCapacidadAsync(int capacidadMinima)
        {
            return await _dbSet
                .Where(e => e.Capacidad >= capacidadMinima)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Autobus>> GetDisponiblesAsync(int estadoDisponibleId)
        {
            return await _dbSet
                .Where(e => e.EstadoAutobusId == estadoDisponibleId)
                .ToListAsync();
        }
    }
}
