using SGA.Web.Models.Transporte;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IParadaApiService
{
    Task<IReadOnlyList<ParadaDto>> GetByRutaIdAsync(int rutaId);
    Task<ApiResponse> CreateAsync(SaveParadaDto dto);
    Task<ApiResponse> UpdateAsync(int id, UpdateParadaDto dto);
    Task<ApiResponse> DeleteAsync(int id);
}
