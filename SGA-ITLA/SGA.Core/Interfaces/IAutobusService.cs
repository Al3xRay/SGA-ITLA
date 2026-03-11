using SGA.Application.Base;
using SGA.Domain.Base;
using SGAITLA.Application.Dtos.Transporte;
using SGAITLA.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGAITLA.Application.Interfaces;

public interface IAutobusService
{
    Task<OperationResult<List<AutobusDto>>> GetAll();
    Task<OperationResult<AutobusDto>> GetById(int id);
    Task<OperationResult<int>> Save(SaveAutobusDto dto);
    Task<OperationResult<int>> Update(UpdateAutobusDto dto);
    Task<OperationResult<bool>> Remove(RemoveAutobusDto dto);
    Task<OperationResult<List<AutobusDto>>> GetDisponibles();
}
