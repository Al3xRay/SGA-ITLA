using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Personas;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Personas
{
    public class PersonaRepository : BaseRepository<Persona>, IPersonaRepository
    {
        public PersonaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Persona?> GetByDocumentoAsync(string documentoIdentidad)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.DocumentoIdentidad == documentoIdentidad);
        }
    }
}
