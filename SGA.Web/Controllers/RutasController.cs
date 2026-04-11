using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Rutas;
using SGA.Web.ViewModels.Billetera;

namespace SGA.Web.Controllers;

public class RutasController : Controller
{
    private readonly IRutaApiService _service;
    public RutasController(IRutaApiService service) => _service = service;

    public async Task<IActionResult> Publicas(string? busqueda)
    {
        var rutas = await _service.GetAllAsync();
        return View(new RutasPublicasViewModel
        {
            Rutas = rutas,
            Busqueda = busqueda
        });
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(new RutaListViewModel { Rutas = data ?? new() });
    }

    public IActionResult Create() => View(new RutaFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RutaFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.CreateAsync(model.ToSaveDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Ruta creada exitosamente."); return RedirectToAction(nameof(Index)); }
        AlertHelper.SetError(this, result.Message);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(RutaFormViewModel.FromDto(dto));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RutaFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.UpdateAsync(id, model.ToUpdateDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Ruta actualizada exitosamente."); return RedirectToAction(nameof(Index)); }
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
        if (result.Success) AlertHelper.SetSuccess(this, "Ruta eliminada.");
        else AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Index));
    }
}
