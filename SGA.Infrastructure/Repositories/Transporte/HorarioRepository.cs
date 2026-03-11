using SGA.Domain.Entidades.Transporte;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Transporte
{
    public class HorarioRepository : BaseRepository<Horario>
    {
        public HorarioRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
