using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class RutaApiService : BaseApiService, IRutaApiService
{
    public RutaApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<RutaDto>> GetAllAsync()
        => await GetAsync<List<RutaDto>>("api/rutas") ?? new();

    public Task<RutaDto?> GetByIdAsync(int id)
        => GetAsync<RutaDto>($"api/rutas/{id}");

    public Task<ApiResponse> CreateAsync(SaveRutaDto dto)
        => PostAsync("api/rutas", dto);

    public Task<ApiResponse> UpdateAsync(int id, UpdateRutaDto dto)
        => PutAsync($"api/rutas/{id}", dto);

    public Task<ApiResponse> DeleteAsync(int id)
        => DeleteAsync($"api/rutas/{id}");
}
