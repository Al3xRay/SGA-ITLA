using SGA.Application.Dtos.Personas;
using SGA.Application.Base;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IConductorService : IBaseService<ConductorDto, SaveConductorDto, UpdatePersonaDto>
{
    Task<OperationResult<IReadOnlyList<ConductorDto>>> GetActivosAsync();
}
