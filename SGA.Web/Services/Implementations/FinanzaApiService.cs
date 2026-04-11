using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class FinanzaApiService : BaseApiService, IFinanzaApiService
{
    private const string ApiEndpoint = "api/Finanzas";

    public FinanzaApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory) 
        : base(factory, accessor, loggerFactory)
    {
    }

    public async Task<IReadOnlyList<TransaccionFinancieraDto>> GetAllAsync()
    {
        return await GetAsync<List<TransaccionFinancieraDto>>(ApiEndpoint) ?? new List<TransaccionFinancieraDto>();
    }

    public async Task<decimal> GetIngresosTotalesAsync()
    {
        return await GetAsync<decimal>($"{ApiEndpoint}/totales");
    }

    public async Task<ApiResponse> CrearAsync(SaveTransaccionFinancieraDto dto)
    {
        return await PostAsync(ApiEndpoint, dto);
    }

    public async Task<ApiResponse> EliminarAsync(int id)
    {
        return await DeleteAsync($"{ApiEndpoint}/{id}");
    }
}
