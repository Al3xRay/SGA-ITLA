using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Personas;

namespace SGA.Web.Controllers;

public class PersonasController : Controller
{
    private readonly IPersonaApiService _service;
    public PersonasController(IPersonaApiService service) => _service = service;

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(new PersonaListViewModel { Personas = data ?? new() });
    }

    public IActionResult Create() => View(new PersonaFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PersonaFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.CreateAsync(model.ToSaveDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Persona registrada exitosamente."); return RedirectToAction(nameof(Index)); }
        AlertHelper.SetError(this, result.Message);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(PersonaFormViewModel.FromDto(dto));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PersonaFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.UpdateAsync(id, model.ToUpdateDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Persona actualizada exitosamente."); return RedirectToAction(nameof(Index)); }
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
        if (result.Success) AlertHelper.SetSuccess(this, "Persona eliminada.");
        else AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Buscar(string query)
    {
        var data = await _service.BuscarAsync(query);
        return Json(data);
    }
}
