using SGA.Application.Base;
using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IAutorizacionService : IBaseService<AutorizacionDto, SaveAutorizacionDto, UpdateAutorizacionDto>
{
    Task<OperationResult<IReadOnlyList<AutorizacionDto>>> GetByPersonaAsync(int personaId);
}
