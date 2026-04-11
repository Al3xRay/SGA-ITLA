using SGA.Web.Models.Auth;
using SGA.Web.Models.Personas;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class AuthApiService : BaseApiService, IAuthApiService
{
    public AuthApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var client = CreateClient();

        try
        {
            var response = await client.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>(
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            return null;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public Task<ApiResponse> RegisterAsync(SavePersonaDto dto)
        => PostAsync("api/auth/register", dto);
}
