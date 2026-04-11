using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class FinanzasController : ControllerBase
{
    private readonly IFinanzaService _finanzaService;

    public FinanzasController(IFinanzaService finanzaService)
    {
        _finanzaService = finanzaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _finanzaService.GetAllAsync();
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpGet("totales")]
    public async Task<IActionResult> GetIngresosTotales()
    {
        var result = await _finanzaService.GetIngresosTotalesAsync();
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveTransaccionFinancieraDto dto)
    {
        var result = await _finanzaService.SaveAsync(dto);
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _finanzaService.DeleteAsync(id);
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }
}
