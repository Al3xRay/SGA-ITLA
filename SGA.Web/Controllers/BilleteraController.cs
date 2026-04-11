using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Billetera;

namespace SGA.Web.Controllers;


/// Módulo de Billetera para Estudiantes y Empleados.


public class BilleteraController : Controller
{
    private readonly IBilleteraApiService _billeteraService;

    public BilleteraController(IBilleteraApiService billeteraService)
    {
        _billeteraService = billeteraService;
    }

    public async Task<IActionResult> Index()
    {
        // Obtener PersonaId de la sesión (almacenado como Int32 en el Login)
        int? personaId = HttpContext.Session.GetInt32(SessionKeys.PersonaId);

        if (personaId == null || personaId <= 0)
        {
            // Sin PersonaId o inválido: mostrar vista vacía con mensaje informativo
            return View(new BilleteraViewModel());
        }

        var autorizaciones = await _billeteraService.GetMisAutorizacionesAsync(personaId.Value);
        var historial = await _billeteraService.GetMisViajesAsync(personaId.Value);

        var model = new BilleteraViewModel
        {
            Autorizaciones = autorizaciones,
            HistorialViajes = historial
        };

        return View(model);
    }
}
