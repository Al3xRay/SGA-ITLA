using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class ParadaApiService : BaseApiService, IParadaApiService
{
    private const string ApiEndpoint = "api/Paradas";

    public ParadaApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory) 
        : base(factory, accessor, loggerFactory)
    {
    }

    public async Task<IReadOnlyList<ParadaDto>> GetByRutaIdAsync(int rutaId)
    {
        return await GetAsync<List<ParadaDto>>($"{ApiEndpoint}/ruta/{rutaId}") ?? new List<ParadaDto>();
    }

    public async Task<ApiResponse> CreateAsync(SaveParadaDto dto)
    {
        return await PostAsync(ApiEndpoint, dto);
    }

    public async Task<ApiResponse> UpdateAsync(int id, UpdateParadaDto dto)
    {
        return await PutAsync($"{ApiEndpoint}/{id}", dto);
    }

    public async Task<ApiResponse> DeleteAsync(int id)
    {
        return await DeleteAsync($"{ApiEndpoint}/{id}");
    }
}
