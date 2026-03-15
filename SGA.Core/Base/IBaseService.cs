using SGA.Domain.Base;

namespace SGA.Application.Base;

public interface IBaseService<TDto, TSaveDto, TUpdateDto>
{
    Task<OperationResult<IReadOnlyList<TDto>>> GetAllAsync();
    Task<OperationResult<TDto>> GetByIdAsync(int id);
    Task<OperationResult> SaveAsync(TSaveDto dto);
    Task<OperationResult> UpdateAsync(int id, TUpdateDto dto);
    Task<OperationResult> DeleteAsync(int id);
}
