using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Autobuses;

namespace SGA.Web.Controllers;

public class AutobusesController : Controller
{
    private readonly IAutobusApiService _service;
    public AutobusesController(IAutobusApiService service) => _service = service;

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(new AutobusListViewModel { Autobuses = data ?? new() });
    }

    public IActionResult Create() => View(new AutobusFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AutobusFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.CreateAsync(model.ToSaveDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Autobús registrado exitosamente."); return RedirectToAction(nameof(Index)); }
        AlertHelper.SetError(this, result.Message);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(AutobusFormViewModel.FromDto(dto));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AutobusFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.UpdateAsync(id, model.ToUpdateDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Autobús actualizado exitosamente."); return RedirectToAction(nameof(Index)); }
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
        AlertHelper.SetSuccess(this, result.Success ? "Autobús eliminado." : result.Message);
        if (!result.Success) AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Index));
    }
}
