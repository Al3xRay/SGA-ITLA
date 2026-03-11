using SGA.Domain.Base;
using SGA.Application.Dtos.Transporte;

namespace SGA.Application.Interfaces;

public interface IRutaService
{
    Task<OperationResult<List<RutaDto>>> GetAll();
    Task<OperationResult<RutaDto>> GetById(int id);
    Task<OperationResult<int>> Save(SaveRutaDto dto);
    Task<OperationResult<int>> Update(UpdateRutaDto dto);
    Task<OperationResult<bool>> Remove(RemoveRutaDto dto);
    Task<OperationResult<List<RutaDto>>> GetActivas();
}
