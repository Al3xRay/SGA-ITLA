using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Personas;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class ConductoresController : BaseApiController
{
    private readonly IConductorService _conductorService;

    public ConductoresController(IConductorService conductorService)
    {
        _conductorService = conductorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _conductorService.GetAllAsync();
        return ToHttpResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _conductorService.GetByIdAsync(id);
        return ToHttpResponse(result);
    }

    [HttpGet("activos")]
    public async Task<IActionResult> GetActivos()
    {
        var result = await _conductorService.GetActivosAsync();
        return ToHttpResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveConductorDto dto)
    {
        var result = await _conductorService.SaveAsync(dto);
        return ToHttpResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonaDto dto)
    {
        var result = await _conductorService.UpdateAsync(id, dto);
        return ToHttpResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _conductorService.DeleteAsync(id);
        return ToHttpResponse(result);
    }
}
