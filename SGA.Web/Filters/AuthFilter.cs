using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SGA.Web.Helpers;

namespace SGA.Web.Filters;

/// Verifica si el usuario tiene un token JWT para acceder, de lo contrario lo redirige al login.
public class AuthFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // No aplicar filtro a AuthController
        var controller = context.Controller as Controller;
        var controllerName = context.RouteData.Values["controller"]?.ToString();

        if (string.Equals(controllerName, "Auth", StringComparison.OrdinalIgnoreCase))
            return;

        var token = context.HttpContext.Session.GetString(SessionKeys.Token);

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}
