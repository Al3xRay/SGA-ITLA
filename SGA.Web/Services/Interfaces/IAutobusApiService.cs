using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IAutobusApiService
{
    Task<List<AutobusDto>> GetAllAsync();
    Task<AutobusDto?> GetByIdAsync(int id);
    Task<List<AutobusDto>> GetDisponiblesAsync();
    Task<ApiResponse> CreateAsync(SaveAutobusDto dto);
    Task<ApiResponse> UpdateAsync(int id, UpdateAutobusDto dto);
    Task<ApiResponse> DeleteAsync(int id);
}
