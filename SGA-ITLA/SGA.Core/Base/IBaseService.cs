using SGA.Domain.Base;

namespace SGA.Application.Base;

public interface IBaseService<TDtoSave, TDtoUpdate, TDtoRemove>
{
    Task<OperationResult> GetAll();
    Task<OperationResult> GetById(int Id);
    Task<OperationResult> Save(TDtoSave dto);
    Task<OperationResult> Update(TDtoUpdate dto);
    Task<OperationResult> Remove(TDtoRemove dto);
}
