using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IRutaApiService
{
    Task<List<RutaDto>> GetAllAsync();
    Task<RutaDto?> GetByIdAsync(int id);
    Task<ApiResponse> CreateAsync(SaveRutaDto dto);
    Task<ApiResponse> UpdateAsync(int id, UpdateRutaDto dto);
    Task<ApiResponse> DeleteAsync(int id);
}
