using SGA.Domain.Entidades.Operaciones;

namespace SGA.Domain.Repository;

public interface IAutorizacionRepository : IBaseRepository<Autorizacion>
{
    Task<IReadOnlyList<Autorizacion>> GetVigentesAsync(int personaId);
    Task<Autorizacion?> GetActivaByPersonaAsync(int personaId);
}
