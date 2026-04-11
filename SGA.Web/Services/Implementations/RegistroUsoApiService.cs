using SGA.Web.Models.Operaciones;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class RegistroUsoApiService : BaseApiService, IRegistroUsoApiService
{
    public RegistroUsoApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<RegistroUsoDto>> GetAllAsync()
        => await GetAsync<List<RegistroUsoDto>>("api/registros-uso") ?? new();

    public async Task<List<RegistroUsoDto>> GetByViajeAsync(int viajeId)
        => await GetAsync<List<RegistroUsoDto>>($"api/registros-uso/viaje/{viajeId}") ?? new();

    public async Task<List<RegistroUsoDto>> GetByPersonaAsync(int personaId)
        => await GetAsync<List<RegistroUsoDto>>($"api/registros-uso/persona/{personaId}") ?? new();
}
