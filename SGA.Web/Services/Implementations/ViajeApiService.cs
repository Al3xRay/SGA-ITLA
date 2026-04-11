using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class ViajeApiService : BaseApiService, IViajeApiService
{
    public ViajeApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<ViajeDto>> GetAllAsync()
        => await GetAsync<List<ViajeDto>>("api/viajes") ?? new();

    public Task<ViajeDto?> GetByIdAsync(int id)
        => GetAsync<ViajeDto>($"api/viajes/{id}");

    public Task<ApiResponse> CreateAsync(SaveViajeDto dto)
        => PostAsync("api/viajes", dto);

    public Task<ApiResponse> UpdateAsync(int id, UpdateViajeDto dto)
        => PutAsync($"api/viajes/{id}", dto);

    public Task<ApiResponse> IniciarAsync(int id)
        => PostAsync($"api/viajes/{id}/iniciar");

    public Task<ApiResponse> FinalizarAsync(int id)
        => PostAsync($"api/viajes/{id}/finalizar");

    public Task<ApiResponse> DeleteAsync(int id)
        => DeleteAsync($"api/viajes/{id}");
}
