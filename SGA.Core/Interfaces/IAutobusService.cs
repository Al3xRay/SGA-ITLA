using SGA.Application.Base;
using SGA.Domain.Base;
using SGA.Application.Dtos.Transporte;


namespace SGA.Application.Interfaces;

public interface IAutobusService
{
    Task<OperationResult<List<AutobusDto>>> GetAll();
    Task<OperationResult<AutobusDto>> GetById(int id);
    Task<OperationResult<int>> Save(SaveAutobusDto dto);
    Task<OperationResult<int>> Update(UpdateAutobusDto dto);
    Task<OperationResult<bool>> Remove(RemoveAutobusDto dto);
    Task<OperationResult<List<AutobusDto>>> GetDisponibles();
}
