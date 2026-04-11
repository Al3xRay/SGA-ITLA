using Microsoft.AspNetCore.Mvc;
using SGA.Web.Models.Transporte;
using SGA.Web.Services.Interfaces;

namespace SGA.Web.Controllers;

[Route("paradas")]
public class ParadasController : Controller
{
    private readonly IParadaApiService _paradaService;

    public ParadasController(IParadaApiService paradaService)
    {
        _paradaService = paradaService;
    }

    [HttpGet("ruta/{rutaId}")]
    public async Task<IActionResult> GetByRuta(int rutaId)
    {
        var paradas = await _paradaService.GetByRutaIdAsync(rutaId);
        return Json(paradas);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveParadaDto dto)
    {
        var result = await _paradaService.CreateAsync(dto);
        if (result.Success) return Ok();
        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _paradaService.DeleteAsync(id);
        if (result.Success) return Ok();
        return BadRequest(result.Message);
    }
}
