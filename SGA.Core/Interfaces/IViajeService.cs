using SGA.Application.Base;
using SGA.Application.Dtos.Transporte;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IViajeService : IBaseService<ViajeDto, SaveViajeDto, UpdateViajeDto>
{
    Task<OperationResult> IniciarViajeAsync(int viajeId);
    Task<OperationResult> FinalizarViajeAsync(int viajeId);
}
