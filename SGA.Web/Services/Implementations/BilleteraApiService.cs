using SGA.Web.Models.Operaciones;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class BilleteraApiService : BaseApiService, IBilleteraApiService
{
    public BilleteraApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<AutorizacionDto>> GetMisAutorizacionesAsync(int personaId)
        => await GetAsync<List<AutorizacionDto>>($"api/autorizaciones/persona/{personaId}") ?? new();

    public async Task<List<RegistroUsoDto>> GetMisViajesAsync(int personaId)
        => await GetAsync<List<RegistroUsoDto>>($"api/registros-uso/persona/{personaId}") ?? new();
}
