using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Services.Interfaces;
using SGA.Web.Models.Operaciones;

namespace SGA.Web.Controllers;

public class HomeController : Controller
{
    private readonly IAuditoriaApiService _auditoriaService;
    private readonly IAutorizacionApiService _autorizacionService;

    public HomeController(IAuditoriaApiService auditoriaService, IAutorizacionApiService autorizacionService)
    {
        _auditoriaService = auditoriaService;
        _autorizacionService = autorizacionService;
    }

    public async Task<IActionResult> Index()
    {
        var role = HttpContext.Session.GetString(SessionKeys.UserRole) ?? "Estudiante";
        var personaId = HttpContext.Session.GetInt32(SessionKeys.PersonaId) ?? 0;
        
        ViewBag.Role = role;
        ViewBag.UserName = HttpContext.Session.GetString(SessionKeys.UserName) ?? "Usuario";
        
        if (role == "Administrador")
        {
            var stats = await _auditoriaService.GetEstadisticasGeneralesAsync();
            return View(stats ?? new AuditoriaGeneralDto());
        }

        // Para estudiantes y empleados, obtenemos su balance de autorizaciones
        if (personaId > 0)
        {
            var autorizaciones = await _autorizacionService.GetByPersonaAsync(personaId);
            var authPrincipal = autorizaciones.OrderByDescending(a => a.FechaVencimiento).FirstOrDefault();
            
            ViewBag.Saldo = authPrincipal?.Saldo.ToString("C") ?? "$0.00";
            ViewBag.ViajesRestantes = authPrincipal?.ViajesRestantes.ToString() ?? "0";
        }
        else
        {
            ViewBag.Saldo = "$0.00";
            ViewBag.ViajesRestantes = "0";
        }

        return View(new AuditoriaGeneralDto());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
