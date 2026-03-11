using SGA.Domain.Base;
using SGAITLA.Application.Dtos.Transporte;
using SGA.Application.Dtos.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;

namespace SGAITLA.Application.Interfaces;

public interface IRutaService
{
    Task<OperationResult<List<RutaDto>>> GetAll();
    Task<OperationResult<RutaDto>> GetById(int id);
    Task<OperationResult<int>> Save(SaveRutaDto dto);
    Task<OperationResult<int>> Update(UpdateRutaDto dto);
    Task<OperationResult<bool>> Remove(RemoveRutaDto dto);
    Task<OperationResult<List<RutaDto>>> GetActivas();
}
