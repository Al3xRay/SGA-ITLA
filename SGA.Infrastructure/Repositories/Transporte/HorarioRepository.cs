using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Transporte
{
    public class HorarioRepository : BaseRepository<Horario>, IHorarioRepository
    {
        public HorarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Horario>> GetByRutaAsync(int rutaId)
        {
            return await _dbSet.Where(h => h.RutaId == rutaId).ToListAsync();
        }
    }
}
