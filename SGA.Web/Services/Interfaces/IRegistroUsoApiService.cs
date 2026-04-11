using SGA.Web.Models.Operaciones;

namespace SGA.Web.Services.Interfaces;

public interface IRegistroUsoApiService
{
    Task<List<RegistroUsoDto>> GetAllAsync();
    Task<List<RegistroUsoDto>> GetByViajeAsync(int viajeId);
    Task<List<RegistroUsoDto>> GetByPersonaAsync(int personaId);
}
