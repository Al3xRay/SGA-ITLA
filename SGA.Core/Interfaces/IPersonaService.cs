using SGA.Application.Dtos.Personas;
using SGA.Domain.Base;


namespace SGA.Application.Interfaces;

public interface IPersonaService
{
    Task<OperationResult<List<PersonaDto>>> GetAll();
    Task<OperationResult<PersonaDto>> GetById(int id);
    Task<OperationResult<int>> Save(SavePersonaDto dto);
    Task<OperationResult<int>> Update(UpdatePersonaDto dto);
    Task<OperationResult<bool>> Remove(RemovePersonaDto dto);
    Task<OperationResult<List<PersonaDto>>> GetEstudiantes();
    Task<OperationResult<List<PersonaDto>>> GetConductoresDisponibles();
}
