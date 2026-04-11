using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IIncidenciaApiService
{
    Task<List<IncidenciaDto>> GetAllAsync();
    Task<IncidenciaDto?> GetByIdAsync(int id);
    Task<List<IncidenciaDto>> GetByViajeAsync(int viajeId);
    Task<ApiResponse> ResolverAsync(int id, string resolucion);
}
