using SGA.Domain.Entidades.Personas;

namespace SGA.Domain.Repository;

public interface IConductorRepository : IBaseRepository<Conductor>
{
    Task<IReadOnlyList<Conductor>> GetActivosAsync();
}
