using SGA.Domain.Entidades.Transporte;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Transporte
{
    public class RutaRepository : BaseRepository<Ruta>
    {
        public RutaRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
