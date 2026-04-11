using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class AutobusApiService : BaseApiService, IAutobusApiService
{
    public AutobusApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<AutobusDto>> GetAllAsync()
        => await GetAsync<List<AutobusDto>>("api/autobuses") ?? new();

    public Task<AutobusDto?> GetByIdAsync(int id)
        => GetAsync<AutobusDto>($"api/autobuses/{id}");

    public async Task<List<AutobusDto>> GetDisponiblesAsync()
        => await GetAsync<List<AutobusDto>>("api/autobuses/disponibles") ?? new();

    public Task<ApiResponse> CreateAsync(SaveAutobusDto dto)
        => PostAsync("api/autobuses", dto);

    public Task<ApiResponse> UpdateAsync(int id, UpdateAutobusDto dto)
        => PutAsync($"api/autobuses/{id}", dto);

    public Task<ApiResponse> DeleteAsync(int id)
        => DeleteAsync($"api/autobuses/{id}");
}
