using SGA.Application.Dtos.Personas;
using SGAITLA.Application.Dtos.Personas;
using SGA.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;


namespace SGAITLA.Application.Interfaces;

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
