using SGA.Domain.Entidades.Personas;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Personas
{
    public class PersonaRepository : BaseRepository<Persona>
    {
        public PersonaRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
