using Microsoft.AspNetCore.Mvc;
using SGA.Web.Services.Interfaces;
using SGA.Web.ViewModels.RegistrosUso;

namespace SGA.Web.Controllers;

public class RegistrosUsoController : Controller
{
    private readonly IRegistroUsoApiService _service;
    public RegistrosUsoController(IRegistroUsoApiService service) => _service = service;

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        return View(new RegistroUsoListViewModel { Registros = data ?? new() });
    }
}
