using SGA.Web.Models.Operaciones;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class AuditoriaApiService : BaseApiService, IAuditoriaApiService
{
    private const string ApiEndpoint = "api/Auditoria";

    public AuditoriaApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory) 
        : base(factory, accessor, loggerFactory)
    {
    }

    public async Task<AuditoriaGeneralDto?> GetEstadisticasGeneralesAsync()
    {
        return await GetAsync<AuditoriaGeneralDto>($"{ApiEndpoint}/estadisticas");
    }

    public async Task<IReadOnlyList<AuditoriaViajeDto>> GetHistorialViajesCompletoAsync()
    {
        return await GetAsync<List<AuditoriaViajeDto>>($"{ApiEndpoint}/viajes-completos") ?? new List<AuditoriaViajeDto>();
    }

    public async Task<IReadOnlyList<AuditoriaTicketDto>?> GetHistorialConsumosGeneralesAsync()
    {
        return await GetAsync<List<AuditoriaTicketDto>>($"{ApiEndpoint}/consumos");
    }

    public async Task<IReadOnlyList<AuditoriaEmisionDto>?> GetHistorialEmisionesCompletoAsync()
    {
        return await GetAsync<List<AuditoriaEmisionDto>>($"{ApiEndpoint}/emisiones-completas");
    }
}
