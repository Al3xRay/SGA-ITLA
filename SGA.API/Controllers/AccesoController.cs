using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = "ConductorOnly")]
public class AccesoController : BaseApiController
{
    private readonly IAccesoService _accesoService;

    public AccesoController(IAccesoService accesoService)
    {
        _accesoService = accesoService;
    }

    /// <summary>
    /// Endpoint crítico — Validar acceso al transporte.
    /// El conductor escanea la credencial QR del pasajero.
    /// </summary>
    [HttpPost("validar")]
    public async Task<IActionResult> ValidarAcceso([FromBody] ValidarAccesoDto dto)
    {
        var result = await _accesoService.ValidarAccesoAsync(dto);
        return ToHttpResponse(result);
    }
}
