using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Operaciones
{
    public class AutorizacionRepository : BaseRepository<Autorizacion>, IAutorizacionRepository
    {
        public AutorizacionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<Autorizacion>> GetAllAsync()
        {
            return await _dbSet
                .Include(a => a.Persona)
                .Include(a => a.Tipo)
                .Include(a => a.EmitidoPor)
                .ToListAsync();
        }

        public override async Task<Autorizacion?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Persona)
                .Include(a => a.Tipo)
                .Include(a => a.EmitidoPor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IReadOnlyList<Autorizacion>> GetVigentesAsync(int personaId)
        {
            return await _dbSet
                .Include(a => a.Persona)
                .Include(a => a.Tipo)
                .Where(a => a.PersonaId == personaId && a.FechaVencimiento > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<Autorizacion?> GetActivaByPersonaAsync(int personaId)
        {
            return await _dbSet
                .Include(a => a.Persona)
                .Include(a => a.Tipo)
                .Where(a => a.PersonaId == personaId && a.FechaVencimiento > DateTime.UtcNow)
                .OrderByDescending(a => a.FechaEmision)
                .FirstOrDefaultAsync();
        }
    }
}
