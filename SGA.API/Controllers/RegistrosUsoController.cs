using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/registros-uso")]
[Authorize]
public class RegistrosUsoController : BaseApiController
{
    private readonly IRegistroUsoService _registroUsoService;

    public RegistrosUsoController(IRegistroUsoService registroUsoService)
    {
        _registroUsoService = registroUsoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _registroUsoService.GetAllAsync();
        return ToHttpResponse(result);
    }

    [HttpGet("viaje/{viajeId}")]
    public async Task<IActionResult> GetByViaje(int viajeId)
    {
        var result = await _registroUsoService.GetByViajeAsync(viajeId);
        return ToHttpResponse(result);
    }

    [HttpGet("persona/{personaId}")]
    public async Task<IActionResult> GetByPersona(int personaId)
    {
        var result = await _registroUsoService.GetByPersonaAsync(personaId);
        return ToHttpResponse(result);
    }
}
