using Microsoft.AspNetCore.Mvc;
using SGA.Web.Services.Interfaces;
namespace SGA.Web.Controllers;

public class AuditoriaController : Controller
{
    private readonly IAuditoriaApiService _auditoriaService;

    public AuditoriaController(IAuditoriaApiService auditoriaService)
    {
        _auditoriaService = auditoriaService;
    }

    public async Task<IActionResult> Index()
    {
        var viajes = await _auditoriaService.GetHistorialViajesCompletoAsync();
        var emisiones = await _auditoriaService.GetHistorialEmisionesCompletoAsync();
        
        var model = new SGA.Web.ViewModels.Auditoria.AuditoriaDashboardViewModel
        {
            Viajes = (viajes ?? new List<SGA.Web.Models.Operaciones.AuditoriaViajeDto>()).ToList(),
            Emisiones = (emisiones ?? new List<SGA.Web.Models.Operaciones.AuditoriaEmisionDto>()).ToList()
        };

        return View(model);
    }
}
