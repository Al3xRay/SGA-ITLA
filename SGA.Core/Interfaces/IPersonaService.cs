using SGA.Application.Base;
using SGA.Application.Dtos.Personas;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IPersonaService : IBaseService<PersonaDto, SavePersonaDto, UpdatePersonaDto>
{
    Task<OperationResult<IReadOnlyList<PersonaDto>>> BuscarAsync(string query);
    Task<bool> ValidarCredencialesAsync(string documento, string contrasena);
    Task<PersonaDto?> GetByDocumentoAsync(string documento);
}
