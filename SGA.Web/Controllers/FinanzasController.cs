using Microsoft.AspNetCore.Mvc;
using SGA.Web.Services.Interfaces;
namespace SGA.Web.Controllers;

public class FinanzasController : Controller
{
    private readonly IFinanzaApiService _finanzaService;

    public FinanzasController(IFinanzaApiService finanzaService)
    {
        _finanzaService = finanzaService;
    }

    public async Task<IActionResult> Index()
    {
        var transacciones = await _finanzaService.GetAllAsync();
        return View(transacciones ?? new List<SGA.Web.Models.Operaciones.TransaccionFinancieraDto>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new SGA.Web.Models.Operaciones.SaveTransaccionFinancieraDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(SGA.Web.Models.Operaciones.SaveTransaccionFinancieraDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var result = await _finanzaService.CrearAsync(dto);
        if (result.Success)
        {
            TempData["SuccessMessage"] = result.Message ?? "Transacción registrada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", result.Message ?? "Error al registrar la transacción.");
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _finanzaService.EliminarAsync(id);
        if (result.Success)
            TempData["SuccessMessage"] = "Transacción anulada correctamente.";
        else
            TempData["ErrorMessage"] = result.Message ?? "Ocurrió un error al anular la transacción.";
            
        return RedirectToAction(nameof(Index));
    }
}
