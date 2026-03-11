using SGA.Domain.Entidades.Personas;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Personas
{
    public class AdministradorRepository : BaseRepository<Administrador>
    {
        public AdministradorRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
