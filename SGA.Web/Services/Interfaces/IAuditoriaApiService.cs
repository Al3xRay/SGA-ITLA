using SGA.Web.Models.Operaciones;

namespace SGA.Web.Services.Interfaces;

public interface IAuditoriaApiService
{
    Task<AuditoriaGeneralDto?> GetEstadisticasGeneralesAsync();
    Task<IReadOnlyList<AuditoriaViajeDto>> GetHistorialViajesCompletoAsync();
    Task<IReadOnlyList<AuditoriaTicketDto>?> GetHistorialConsumosGeneralesAsync();
    Task<IReadOnlyList<AuditoriaEmisionDto>?> GetHistorialEmisionesCompletoAsync();
}
