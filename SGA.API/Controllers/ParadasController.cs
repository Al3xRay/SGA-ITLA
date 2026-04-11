using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class ParadasController : BaseApiController
{
    private readonly IParadaService _paradaService;

    public ParadasController(IParadaService paradaService)
    {
        _paradaService = paradaService;
    }

    [HttpGet("ruta/{rutaId}")]
    public async Task<IActionResult> GetByRuta(int rutaId)
    {
        var result = await _paradaService.GetByRutaIdAsync(rutaId);
        return ToHttpResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveParadaDto dto)
    {
        var result = await _paradaService.SaveAsync(dto);
        return ToHttpResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateParadaDto dto)
    {
        var result = await _paradaService.UpdateAsync(id, dto);
        return ToHttpResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _paradaService.DeleteAsync(id);
        return ToHttpResponse(result);
    }
}
