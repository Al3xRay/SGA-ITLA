using SGA.Web.Models.Personas;
using SGA.Web.Models.Shared;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Services.Implementations;

public class PersonaApiService : BaseApiService, IPersonaApiService
{
    public PersonaApiService(IHttpClientFactory factory, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        : base(factory, accessor, loggerFactory) { }

    public async Task<List<PersonaDto>> GetAllAsync()
        => await GetAsync<List<PersonaDto>>("api/personas") ?? new();

    public Task<PersonaDto?> GetByIdAsync(int id)
        => GetAsync<PersonaDto>($"api/personas/{id}");

    public Task<ApiResponse> CreateAsync(SavePersonaDto dto)
        => PostAsync("api/personas", dto);

    public Task<ApiResponse> UpdateAsync(int id, UpdatePersonaDto dto)
        => PutAsync($"api/personas/{id}", dto);

    public Task<ApiResponse> DeleteAsync(int id)
        => DeleteAsync($"api/personas/{id}");

    public async Task<List<PersonaDto>> BuscarAsync(string query)
        => await GetAsync<List<PersonaDto>>($"api/personas/buscar?query={Uri.EscapeDataString(query)}") ?? new();
}
