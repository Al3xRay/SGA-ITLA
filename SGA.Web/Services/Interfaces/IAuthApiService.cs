using SGA.Web.Models.Auth;
using SGA.Web.Models.Personas;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Interfaces;

public interface IAuthApiService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<ApiResponse> RegisterAsync(SavePersonaDto dto);
}
