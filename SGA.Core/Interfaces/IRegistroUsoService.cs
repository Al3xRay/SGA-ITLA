using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IRegistroUsoService
{
    Task<OperationResult<IReadOnlyList<RegistroUsoDto>>> GetAllAsync();
    Task<OperationResult<IReadOnlyList<RegistroUsoDto>>> GetByViajeAsync(int viajeId);
    Task<OperationResult<IReadOnlyList<RegistroUsoDto>>> GetByPersonaAsync(int personaId);
}
