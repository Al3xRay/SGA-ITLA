using SGA.Domain.Entidades.Personas;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Personas
{
    public class AdministradorRepository : BaseRepository<Administrador>, IAdministradorRepository
    {
        public AdministradorRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
