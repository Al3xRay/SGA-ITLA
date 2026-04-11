using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IFinanzaApiService
{
    Task<IReadOnlyList<TransaccionFinancieraDto>> GetAllAsync();
    Task<decimal> GetIngresosTotalesAsync();
    Task<ApiResponse> CrearAsync(SaveTransaccionFinancieraDto dto);
    Task<ApiResponse> EliminarAsync(int id);
}
