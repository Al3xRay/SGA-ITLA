using SGA.Domain.Entidades.Personas;

namespace SGA.Domain.Repository;

public interface IPersonaRepository : IBaseRepository<Persona>
{
    Task<Persona?> GetByDocumentoAsync(string documentoIdentidad);
}
