using SGA.Domain.Base;
using SGA.Application.Dtos.Operaciones;

namespace SGA.Application.Interfaces;

public interface IRegistroUsoService
{
    Task<OperationResult<List<RegistroUsoDto>>> GetAll();
    Task<OperationResult<RegistroUsoDto>> GetById(int id);
    Task<OperationResult<List<RegistroUsoDto>>> GetByPersonaId(int personaId);
    Task<OperationResult<List<RegistroUsoDto>>> GetByViajeId(int viajeId);
    Task<OperationResult<List<RegistroUsoDto>>> GetByFecha(DateTime fecha);
}
