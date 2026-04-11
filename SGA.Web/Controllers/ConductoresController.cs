using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Conductores;

namespace SGA.Web.Controllers;

public class ConductoresController : Controller
{
    private readonly IConductorApiService _service;
    public ConductoresController(IConductorApiService service) => _service = service;

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(new ConductorListViewModel { Conductores = data ?? new() });
    }

    public IActionResult Create() => View(new ConductorFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ConductorFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.CreateAsync(model.ToSaveDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Conductor registrado exitosamente."); return RedirectToAction(nameof(Index)); }
        AlertHelper.SetError(this, result.Message);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(ConductorFormViewModel.FromDto(dto));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ConductorFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.UpdateAsync(id, model.ToUpdateDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Conductor actualizado exitosamente."); return RedirectToAction(nameof(Index)); }
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
        if (result.Success) AlertHelper.SetSuccess(this, "Conductor eliminado.");
        else AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Index));
    }
}
