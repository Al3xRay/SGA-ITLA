using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class AutorizacionesController : BaseApiController
{
    private readonly IAutorizacionService _autorizacionService;

    public AutorizacionesController(IAutorizacionService autorizacionService)
    {
        _autorizacionService = autorizacionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _autorizacionService.GetAllAsync();
        return ToHttpResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _autorizacionService.GetByIdAsync(id);
        return ToHttpResponse(result);
    }

    [HttpGet("persona/{personaId}")]
    public async Task<IActionResult> GetByPersona(int personaId)
    {
        var result = await _autorizacionService.GetByPersonaAsync(personaId);
        return ToHttpResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveAutorizacionDto dto)
    {
        var result = await _autorizacionService.SaveAsync(dto);
        return ToHttpResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAutorizacionDto dto)
    {
        var result = await _autorizacionService.UpdateAsync(id, dto);
        return ToHttpResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _autorizacionService.DeleteAsync(id);
        return ToHttpResponse(result);
    }
}
