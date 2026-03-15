using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class IncidenciasController : BaseApiController
{
    private readonly IIncidenciaService _incidenciaService;

    public IncidenciasController(IIncidenciaService incidenciaService)
    {
        _incidenciaService = incidenciaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _incidenciaService.GetAllAsync();
        return ToHttpResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _incidenciaService.GetByIdAsync(id);
        return ToHttpResponse(result);
    }

    [HttpGet("viaje/{viajeId}")]
    public async Task<IActionResult> GetByViaje(int viajeId)
    {
        var result = await _incidenciaService.GetByViajeAsync(viajeId);
        return ToHttpResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveIncidenciaDto dto)
    {
        var result = await _incidenciaService.SaveAsync(dto);
        return ToHttpResponse(result);
    }

    [HttpPut("{id}/resolver")]
    public async Task<IActionResult> Resolver(int id, [FromBody] ResolverIncidenciaDto dto)
    {
        var result = await _incidenciaService.ResolverAsync(id, dto.Resolucion);
        return ToHttpResponse(result);
    }
}

public class ResolverIncidenciaDto
{
    public string Resolucion { get; set; } = string.Empty;
}
