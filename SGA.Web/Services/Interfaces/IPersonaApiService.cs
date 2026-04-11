using SGA.Web.Models.Personas;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IPersonaApiService
{
    Task<List<PersonaDto>> GetAllAsync();
    Task<PersonaDto?> GetByIdAsync(int id);
    Task<ApiResponse> CreateAsync(SavePersonaDto dto);
    Task<ApiResponse> UpdateAsync(int id, UpdatePersonaDto dto);
    Task<ApiResponse> DeleteAsync(int id);
    Task<List<PersonaDto>> BuscarAsync(string query);
}
