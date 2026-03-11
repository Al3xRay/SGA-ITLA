using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Operaciones;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Operaciones
{
    public class AutorizacionRepository : BaseRepository<Autorizacion>
    {
        public AutorizacionRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IReadOnlyList<Autorizacion>> GetVigentesAsync(int personaId)
        {
            return await _dbSet
                .Where(a => a.PersonaId == personaId && a.FechaVencimiento > DateTime.UtcNow)
                .ToListAsync();
        }
    }
}

