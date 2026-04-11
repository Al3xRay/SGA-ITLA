using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IAutorizacionApiService
{
    Task<List<AutorizacionDto>> GetAllAsync();
    Task<AutorizacionDto?> GetByIdAsync(int id);
    Task<List<AutorizacionDto>> GetByPersonaAsync(int personaId);
    Task<ApiResponse> CreateAsync(SaveAutorizacionDto dto);
    Task<ApiResponse> UpdateAsync(int id, UpdateAutorizacionDto dto);
    Task<ApiResponse> DeleteAsync(int id);
}
