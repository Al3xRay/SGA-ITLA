using SGA.Web.Models.Operaciones;

namespace SGA.Web.Services.Interfaces;

public interface IBilleteraApiService
{
    Task<List<AutorizacionDto>> GetMisAutorizacionesAsync(int personaId);
    Task<List<RegistroUsoDto>> GetMisViajesAsync(int personaId);
}
