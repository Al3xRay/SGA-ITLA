using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IIncidenciaService
{
    Task<OperationResult<IReadOnlyList<IncidenciaDto>>> GetAllAsync();
    Task<OperationResult<IncidenciaDto>> GetByIdAsync(int id);
    Task<OperationResult> SaveAsync(SaveIncidenciaDto dto);
    Task<OperationResult<IReadOnlyList<IncidenciaDto>>> GetByViajeAsync(int viajeId);
    Task<OperationResult> ResolverAsync(int id, string resolucion);
}
