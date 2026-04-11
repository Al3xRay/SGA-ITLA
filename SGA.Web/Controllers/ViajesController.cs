using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Viajes;

namespace SGA.Web.Controllers;

public class ViajesController : Controller
{
    private readonly IViajeApiService _viajeService;
    private readonly IRutaApiService _rutaService;
    private readonly IAutobusApiService _autobusService;
    private readonly IConductorApiService _conductorService;

    public ViajesController(IViajeApiService viajeService, IRutaApiService rutaService,
        IAutobusApiService autobusService, IConductorApiService conductorService)
    {
        _viajeService = viajeService;
        _rutaService = rutaService;
        _autobusService = autobusService;
        _conductorService = conductorService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _viajeService.GetAllAsync();
        return View(new ViajeListViewModel { Viajes = data ?? new() });
    }

    public async Task<IActionResult> Create()
    {
        var model = new ViajeFormViewModel
        {
            Rutas = await _rutaService.GetAllAsync() ?? new(),
            Autobuses = await _autobusService.GetDisponiblesAsync() ?? new(),
            Conductores = await _conductorService.GetActivosAsync() ?? new()
        };
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ViajeFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Rutas = await _rutaService.GetAllAsync() ?? new();
            model.Autobuses = await _autobusService.GetDisponiblesAsync() ?? new();
            model.Conductores = await _conductorService.GetActivosAsync() ?? new();
            return View(model);
        }
        var result = await _viajeService.CreateAsync(model.ToSaveDto());
        if (result.Success) { AlertHelper.SetSuccess(this, "Viaje programado exitosamente."); return RedirectToAction(nameof(Index)); }
        AlertHelper.SetError(this, result.Message);
        model.Rutas = await _rutaService.GetAllAsync() ?? new();
        model.Autobuses = await _autobusService.GetDisponiblesAsync() ?? new();
        model.Conductores = await _conductorService.GetActivosAsync() ?? new();
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var dto = await _viajeService.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(new ViajeDetailViewModel { Viaje = dto });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Iniciar(int id)
    {
        var result = await _viajeService.IniciarAsync(id);
        if (result.Success) AlertHelper.SetSuccess(this, "Viaje iniciado exitosamente.");
        else AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Finalizar(int id)
    {
        var result = await _viajeService.FinalizarAsync(id);
        if (result.Success) AlertHelper.SetSuccess(this, "Viaje finalizado exitosamente.");
        else AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _viajeService.DeleteAsync(id);
        if (result.Success) AlertHelper.SetSuccess(this, "Viaje eliminado.");
        else AlertHelper.SetError(this, result.Message);
        return RedirectToAction(nameof(Index));
    }
}
