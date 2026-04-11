using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class AuditoriaController : ControllerBase
{
    private readonly IAuditoriaService _auditoriaService;

    public AuditoriaController(IAuditoriaService auditoriaService)
    {
        _auditoriaService = auditoriaService;
    }

    [HttpGet("estadisticas")]
    public async Task<IActionResult> GetEstadisticas()
    {
        var result = await _auditoriaService.GetEstadisticasGeneralesAsync();
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpGet("viajes-completos")]
    public async Task<IActionResult> GetViajesCompletos()
    {
        var result = await _auditoriaService.GetHistorialViajesCompletoAsync();
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpGet("consumos")]
    public async Task<IActionResult> GetConsumos()
    {
        var result = await _auditoriaService.GetHistorialConsumosGeneralesAsync();
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpGet("emisiones-completas")]
    public async Task<IActionResult> GetEmisionesCompletas()
    {
        var result = await _auditoriaService.GetHistorialEmisionesCompletoAsync();
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }
}
