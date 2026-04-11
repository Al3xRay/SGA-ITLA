using SGA.Web.Models.Personas;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class ConductorApiService : BaseApiService, IConductorApiService
{
    public ConductorApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<ConductorDto>> GetAllAsync()
        => await GetAsync<List<ConductorDto>>("api/conductores") ?? new();

    public Task<ConductorDto?> GetByIdAsync(int id)
        => GetAsync<ConductorDto>($"api/conductores/{id}");

    public async Task<List<ConductorDto>> GetActivosAsync()
        => await GetAsync<List<ConductorDto>>("api/conductores/activos") ?? new();

    public Task<ApiResponse> CreateAsync(SaveConductorDto dto)
        => PostAsync("api/conductores", dto);

    public Task<ApiResponse> UpdateAsync(int id, UpdateConductorDto dto)
        => PutAsync($"api/conductores/{id}", dto);

    public Task<ApiResponse> DeleteAsync(int id)
        => DeleteAsync($"api/conductores/{id}");
}
