using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SGA.Web.Helpers;

namespace SGA.Web.Filters;

// Este modulo verifica si el usuario tiene permisos necesarios para acceder a modulos restringidos, de lo contrario, lo redirige a AccessDenied
public class AdminOnlyFilter : IActionFilter
{
    // Controladores de administrador
    private static readonly HashSet<string> AdminControllers = new(StringComparer.OrdinalIgnoreCase)
    {
        "Autobuses", "Personas", "Conductores", "Viajes",
        "Autorizaciones", "Incidencias", "Finanzas", "Auditoria"
    };

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controllerName = context.RouteData.Values["controller"]?.ToString() ?? "";

        if (!AdminControllers.Contains(controllerName))
            return;

        var role = context.HttpContext.Session.GetString(SessionKeys.UserRole) ?? "";

        if (!string.Equals(role, "Administrador", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new RedirectToActionResult("AccessDenied", "Auth", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
