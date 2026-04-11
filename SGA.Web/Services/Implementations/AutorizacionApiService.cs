using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class AutorizacionApiService : BaseApiService, IAutorizacionApiService
{
    public AutorizacionApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<AutorizacionDto>> GetAllAsync()
        => await GetAsync<List<AutorizacionDto>>("api/autorizaciones") ?? new();

    public Task<AutorizacionDto?> GetByIdAsync(int id)
        => GetAsync<AutorizacionDto>($"api/autorizaciones/{id}");

    public async Task<List<AutorizacionDto>> GetByPersonaAsync(int personaId)
        => await GetAsync<List<AutorizacionDto>>($"api/autorizaciones/persona/{personaId}") ?? new();

    public Task<ApiResponse> CreateAsync(SaveAutorizacionDto dto)
        => PostAsync("api/autorizaciones", dto);

    public Task<ApiResponse> UpdateAsync(int id, UpdateAutorizacionDto dto)
        => PutAsync($"api/autorizaciones/{id}", dto);

    public Task<ApiResponse> DeleteAsync(int id)
        => DeleteAsync($"api/autorizaciones/{id}");
}
