using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Personas;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class PersonasController : BaseApiController
{
    private readonly IPersonaService _personaService;

    public PersonasController(IPersonaService personaService)
    {
        _personaService = personaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _personaService.GetAllAsync();
        return ToHttpResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _personaService.GetByIdAsync(id);
        return ToHttpResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SavePersonaDto dto)
    {
        var result = await _personaService.SaveAsync(dto);
        return ToHttpResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonaDto dto)
    {
        var result = await _personaService.UpdateAsync(id, dto);
        return ToHttpResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _personaService.DeleteAsync(id);
        return ToHttpResponse(result);
    }
}
