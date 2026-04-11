using Microsoft.AspNetCore.Mvc;
using SGA.Web.Helpers;
using SGA.Web.Models.Auth;
using SGA.Web.Models.Personas;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.Auth;

namespace SGA.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthApiService _authService;

    public AuthController(IAuthApiService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.Token)))
            return RedirectToAction("Index", "Home");

        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var response = await _authService.LoginAsync(new LoginRequest
        {
            Usuario = model.Usuario,
            Contrasena = model.Contrasena
        });

        if (response == null)
        {
            ModelState.AddModelError("", "Credenciales inválidas o el servidor no está disponible.");
            return View(model);
        }
        
        HttpContext.Session.SetString(SessionKeys.Token, response.Token);
        HttpContext.Session.SetString(SessionKeys.UserName, model.Usuario);
        HttpContext.Session.SetString(SessionKeys.UserRole, response.Rol);
        HttpContext.Session.SetString(SessionKeys.TokenExpiration, response.Expiration.ToString("O"));
        HttpContext.Session.SetInt32(SessionKeys.PersonaId, response.PersonaId);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.Token)))
            return RedirectToAction("Index", "Home");

        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _authService.RegisterAsync(new SavePersonaDto
        {
            Nombre = model.Nombre,
            Apellido = model.Apellido,
            DocumentoIdentidad = model.DocumentoIdentidad,
            TipoPersonaId = model.TipoPersonaId,
            Telefono = model.Telefono,
            Contrasena = model.Contrasena
        });

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        // Si el registro tiene exito, lo redirige al inicio de sesion
        TempData["SuccessMessage"] = "¡Cuenta creada exitosamente! Inicia sesión con tu matrícula y contraseña.";
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied() => View();
}
