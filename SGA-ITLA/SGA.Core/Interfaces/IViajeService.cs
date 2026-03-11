using SGA.Domain.Base;
using SGAITLA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;

namespace SGAITLA.Application.Interfaces;

public interface IViajeService
{
    Task<OperationResult<List<ViajeDto>>> GetAll();
    Task<OperationResult<ViajeDto>> GetById(int id);
    Task<OperationResult<int>> Save(SaveViajeDto dto);
    Task<OperationResult<int>> Update(UpdateViajeDto dto);
    Task<OperationResult<bool>> Remove(RemoveViajeDto dto);
    Task<OperationResult<List<ViajeDto>>> GetActivos();
    Task<OperationResult<List<ViajeDto>>> GetByRuta(int rutaId);
    Task<OperationResult<List<ViajeDto>>> GetByConductor(int conductorId);
    Task<OperationResult<bool>> IniciarViaje(int viajeId);
    Task<OperationResult<bool>> FinalizarViaje(int viajeId);
}
