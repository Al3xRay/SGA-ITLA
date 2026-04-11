using Microsoft.AspNetCore.Mvc;

namespace SGA.Web.Helpers;


// Helpers para mostrar notificaciones en la UI.

public static class AlertHelper
{
    public static void SetSuccess(Controller controller, string message)
    {
        controller.TempData["AlertSuccess"] = message;
    }

    public static void SetError(Controller controller, string message)
    {
        controller.TempData["AlertError"] = message;
    }

    public static void SetWarning(Controller controller, string message)
    {
        controller.TempData["AlertWarning"] = message;
    }

    public static void SetInfo(Controller controller, string message)
    {
        controller.TempData["AlertInfo"] = message;
    }
}
