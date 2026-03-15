using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[Authorize]  // ← solo requiere estar autenticado, la política va en cada método
public class ViajesController : BaseApiController
{
    private readonly IViajeService _viajeService;

    public ViajesController(IViajeService viajeService)
    {
        _viajeService = viajeService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _viajeService.GetAllAsync();
        return ToHttpResponse(result);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminOrConductor")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _viajeService.GetByIdAsync(id);
        return ToHttpResponse(result);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create([FromBody] SaveViajeDto dto)
    {
        var result = await _viajeService.SaveAsync(dto);
        return ToHttpResponse(result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateViajeDto dto)
    {
        var result = await _viajeService.UpdateAsync(id, dto);
        return ToHttpResponse(result);
    }

    [HttpPost("{id}/iniciar")]
    [Authorize(Policy = "AdminOrConductor")]
    public async Task<IActionResult> Iniciar(int id)
    {
        var result = await _viajeService.IniciarViajeAsync(id);
        return ToHttpResponse(result);
    }

    [HttpPost("{id}/finalizar")]
    [Authorize(Policy = "AdminOrConductor")]
    public async Task<IActionResult> Finalizar(int id)
    {
        var result = await _viajeService.FinalizarViajeAsync(id);
        return ToHttpResponse(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _viajeService.DeleteAsync(id);
        return ToHttpResponse(result);
    }
}