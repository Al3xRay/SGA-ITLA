using SGA.Domain.Base;
using SGAITLA.Application.Dtos.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;

namespace SGAITLA.Application.Interfaces;

public interface IRegistroUsoService
{
    Task<OperationResult<List<RegistroUsoDto>>> GetAll();
    Task<OperationResult<RegistroUsoDto>> GetById(int id);
    Task<OperationResult<List<RegistroUsoDto>>> GetByPersonaId(int personaId);
    Task<OperationResult<List<RegistroUsoDto>>> GetByViajeId(int viajeId);
    Task<OperationResult<List<RegistroUsoDto>>> GetByFecha(DateTime fecha);
}
