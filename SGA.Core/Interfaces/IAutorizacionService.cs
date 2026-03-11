using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;


namespace SGA.Application.Interfaces;

public interface IAutorizacionService
{
    Task<OperationResult<List<AutorizacionDto>>> GetAll();
    Task<OperationResult<AutorizacionDto>> GetById(int id);
    Task<OperationResult<int>> Save(SaveAutorizacionDto dto);
    Task<OperationResult<int>> Update(UpdateAutorizacionDto dto);
    Task<OperationResult<bool>> Remove(RemoveAutorizacionDto dto);
    Task<OperationResult<List<AutorizacionDto>>> GetByPersonaId(int personaId);
    Task<OperationResult<decimal>> Recargar(int autorizacionId, decimal monto, int changeUser);
    Task<OperationResult<bool>> ValidarAcceso(int personaId, int viajeId);
}


