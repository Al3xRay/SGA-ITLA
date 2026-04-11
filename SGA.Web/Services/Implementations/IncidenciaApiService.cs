using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class IncidenciaApiService : BaseApiService, IIncidenciaApiService
{
    public IncidenciaApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<IncidenciaDto>> GetAllAsync()
        => await GetAsync<List<IncidenciaDto>>("api/incidencias") ?? new();

    public Task<IncidenciaDto?> GetByIdAsync(int id)
        => GetAsync<IncidenciaDto>($"api/incidencias/{id}");

    public async Task<List<IncidenciaDto>> GetByViajeAsync(int viajeId)
        => await GetAsync<List<IncidenciaDto>>($"api/incidencias/viaje/{viajeId}") ?? new();

    public Task<ApiResponse> ResolverAsync(int id, string resolucion)
        => PutAsync($"api/incidencias/{id}/resolver", new { Resolucion = resolucion });
}
