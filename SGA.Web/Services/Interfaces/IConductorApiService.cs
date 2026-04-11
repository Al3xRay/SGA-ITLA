using SGA.Web.Models.Personas;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IConductorApiService
{
    Task<List<ConductorDto>> GetAllAsync();
    Task<ConductorDto?> GetByIdAsync(int id);
    Task<List<ConductorDto>> GetActivosAsync();
    Task<ApiResponse> CreateAsync(SaveConductorDto dto);
    Task<ApiResponse> UpdateAsync(int id, UpdateConductorDto dto);
    Task<ApiResponse> DeleteAsync(int id);
}
