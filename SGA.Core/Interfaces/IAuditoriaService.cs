using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IAuditoriaService
{
    Task<OperationResult<AuditoriaGeneralDto>> GetEstadisticasGeneralesAsync();
    Task<OperationResult<IReadOnlyList<AuditoriaViajeDto>>> GetHistorialViajesCompletoAsync();
    Task<OperationResult<IReadOnlyList<AuditoriaTicketDto>>> GetHistorialConsumosGeneralesAsync();
    Task<OperationResult<IReadOnlyList<AuditoriaEmisionDto>>> GetHistorialEmisionesCompletoAsync();
}
