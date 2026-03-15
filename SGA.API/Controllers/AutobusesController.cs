using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;

namespace SGA.API.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class AutobusesController : BaseApiController
{
    private readonly IAutobusService _autobusService;

    public AutobusesController(IAutobusService autobusService)
    {
        _autobusService = autobusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _autobusService.GetAllAsync();
        return ToHttpResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _autobusService.GetByIdAsync(id);
        return ToHttpResponse(result);
    }

    [HttpGet("disponibles")]
    public async Task<IActionResult> GetDisponibles()
    {
        var result = await _autobusService.GetDisponiblesAsync();
        return ToHttpResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveAutobusDto dto)
    {
        var result = await _autobusService.SaveAsync(dto);
        return ToHttpResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAutobusDto dto)
    {
        var result = await _autobusService.UpdateAsync(id, dto);
        return ToHttpResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _autobusService.DeleteAsync(id);
        return ToHttpResponse(result);
    }
}
