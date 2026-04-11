using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Incidencias;

namespace SGA.Web.Controllers;

public class IncidenciasController : Controller
{
    private readonly IIncidenciaApiService _service;
    public IncidenciasController(IIncidenciaApiService service) => _service = service;

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(new IncidenciaListViewModel { Incidencias = data ?? new() });
    }

    public async Task<IActionResult> Details(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(dto);
    }

    public async Task<IActionResult> Resolver(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(new IncidenciaResolverViewModel { Incidencia = dto });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Resolver(int id, IncidenciaResolverViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var dto = await _service.GetByIdAsync(id);
            model.Incidencia = dto ?? new();
            return View(model);
        }
        var result = await _service.ResolverAsync(id, model.Resolucion);
        if (result.Success) { AlertHelper.SetSuccess(this, "Incidencia resuelta exitosamente."); return RedirectToAction(nameof(Index)); }
        AlertHelper.SetError(this, result.Message);
        return View(model);
    }
}
