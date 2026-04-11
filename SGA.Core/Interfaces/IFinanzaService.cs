using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IFinanzaService
{
    Task<OperationResult<IReadOnlyList<TransaccionFinancieraDto>>> GetAllAsync();
    Task<OperationResult> SaveAsync(SaveTransaccionFinancieraDto dto);
    Task<OperationResult<decimal>> GetIngresosTotalesAsync();
    Task<OperationResult> DeleteAsync(int id);
}
