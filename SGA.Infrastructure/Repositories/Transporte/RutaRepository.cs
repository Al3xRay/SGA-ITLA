using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Transporte
{
    public class RutaRepository : BaseRepository<Ruta>, IRutaRepository
    {
        public RutaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Ruta?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(r => r.Paradas).FirstOrDefaultAsync(r => r.Id == id);
        }

        public override async Task<IReadOnlyList<Ruta>> GetAllAsync()
        {
            return await _dbSet.Include(r => r.Paradas).ToListAsync();
        }
    }
}
