using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using SGA.Web.Helpers;
using SGA.Web.Models.Shared;

namespace SGA.Web.Services.Implementations;

public abstract class BaseApiService
{
    private readonly IHttpClientFactory _factory;
    private readonly IHttpContextAccessor _accessor;
    private readonly ILogger<BaseApiService> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected BaseApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
    {
        _factory  = factory;
        _accessor = accessor;
        _logger   = loggerFactory.CreateLogger<BaseApiService>();
    }

    /// <summary>
    /// Crea un HttpClient configurado con el JWT token de la sesión actual.
    /// </summary>
    protected HttpClient CreateClient()
    {
        var client = _factory.CreateClient("SgaApi");

        var token = _accessor.HttpContext?.Session.GetString(SessionKeys.Token);
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }

    // ────────────────────────────────────────────────────────────────
    //  GET — Obtener datos de la API
    // ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Ejecuta un GET y deserializa la respuesta como T.
    /// Retorna default(T) si la respuesta no es exitosa y loguea el error.
    /// </summary>
    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        var client = CreateClient();

        try
        {
            var response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, JsonOptions);
            }

            var body = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("GET {Endpoint} falló con {StatusCode}: {Body}",
                endpoint, (int)response.StatusCode, body);

            return default;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error de conexión en GET {Endpoint}", endpoint);
            return default;
        }
    }


    // ────────────────────────────────────────────────────────────────
    //  POST — Crear recursos
    // ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Ejecuta un POST con body JSON y retorna un ApiResponse con el resultado.
    /// </summary>
    protected async Task<ApiResponse> PostAsync<T>(string endpoint, T data)
    {
        var client = CreateClient();

        try
        {
            var response = await client.PostAsJsonAsync(endpoint, data);
            return await ParseResponse(response);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse.Fail($"Error de conexión: {ex.Message}");
        }
    }

    /// <summary>
    /// Ejecuta un POST sin body y retorna un ApiResponse.
    /// Útil para acciones como /viajes/{id}/iniciar
    /// </summary>
    protected async Task<ApiResponse> PostAsync(string endpoint)
    {
        var client = CreateClient();

        try
        {
            var response = await client.PostAsync(endpoint, null);
            return await ParseResponse(response);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse.Fail($"Error de conexión: {ex.Message}");
        }
    }

    /// <summary>
    /// Ejecuta un POST y deserializa la respuesta como TResponse.
    /// Útil para endpoints que retornan datos (ej: /acceso/validar → ResultadoAccesoDto).
    /// </summary>
    protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        var client = CreateClient();

        try
        {
            var response = await client.PostAsJsonAsync(endpoint, data);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(json, JsonOptions);
            }

            return default;
        }
        catch (HttpRequestException)
        {
            return default;
        }
    }

    // ────────────────────────────────────────────────────────────────
    //  PUT — Actualizar recursos
    // ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Ejecuta un PUT con body JSON y retorna un ApiResponse.
    /// </summary>
    protected async Task<ApiResponse> PutAsync<T>(string endpoint, T data)
    {
        var client = CreateClient();

        try
        {
            var response = await client.PutAsJsonAsync(endpoint, data);
            return await ParseResponse(response);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse.Fail($"Error de conexión: {ex.Message}");
        }
    }

    // ────────────────────────────────────────────────────────────────
    //  DELETE — Eliminar recursos
    // ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Ejecuta un DELETE y retorna un ApiResponse.
    /// </summary>
    protected async Task<ApiResponse> DeleteAsync(string endpoint)
    {
        var client = CreateClient();

        try
        {
            var response = await client.DeleteAsync(endpoint);
            return await ParseResponse(response);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse.Fail($"Error de conexión: {ex.Message}");
        }
    }

    // ────────────────────────────────────────────────────────────────
    //  Helpers internos
    // ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Parsea una HttpResponseMessage a ApiResponse.
    /// Si es exitosa, retorna Success=true.
    /// Si falla, intenta extraer los errores del body JSON de la API.
    /// </summary>
    private static async Task<ApiResponse> ParseResponse(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            // Intentar extraer el mensaje del body
            try
            {
                var body = JsonSerializer.Deserialize<ApiErrorResponse>(content, JsonOptions);
                return ApiResponse.Ok(body?.Message ?? "Operación exitosa");
            }
            catch
            {
                return ApiResponse.Ok("Operación exitosa");
            }
        }

        // Parsear errores de la API
        try
        {
            var error = JsonSerializer.Deserialize<ApiErrorResponse>(content, JsonOptions);
            return ApiResponse.Fail(
                error?.Message ?? $"Error HTTP {(int)response.StatusCode}",
                error?.Errors
            );
        }
        catch
        {
            return ApiResponse.Fail($"Error HTTP {(int)response.StatusCode}: {content}");
        }
    }
}
