using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Autorizaciones;

namespace SGA.Web.Controllers;

public class AutorizacionesController : Controller
{
    private readonly IAutorizacionApiService _service;
    private readonly IPersonaApiService _personaService;

    public AutorizacionesController(IAutorizacionApiService service, IPersonaApiService personaService)
    {
        _service = service;
        _personaService = personaService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(new AutorizacionListViewModel { Autorizaciones = data ?? new() });
    }

    public IActionResult Create()
    {
        return View(new AutorizacionFormViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AutorizacionFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var saveDto = model.ToSaveDto();
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int emisorId))
        {
            saveDto.EmitidoPorId = emisorId;
        }
        var result = await _service.CreateAsync(saveDto);
        if (result.Success) { AlertHelper.SetSuccess(this, "Autorización emitida exitosamente."); return RedirectToAction(nameof(Index)); }
        AlertHelper.SetError(this, result.Message);
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (result.Success) AlertHelper.SetSuccess(this, "Autorización eliminada.");
        else AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Index));
    }
}
