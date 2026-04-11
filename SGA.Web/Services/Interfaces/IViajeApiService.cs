using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IViajeApiService
{
    Task<List<ViajeDto>> GetAllAsync();
    Task<ViajeDto?> GetByIdAsync(int id);
    Task<ApiResponse> CreateAsync(SaveViajeDto dto);
    Task<ApiResponse> UpdateAsync(int id, UpdateViajeDto dto);
    Task<ApiResponse> IniciarAsync(int id);
    Task<ApiResponse> FinalizarAsync(int id);
    Task<ApiResponse> DeleteAsync(int id);
}
