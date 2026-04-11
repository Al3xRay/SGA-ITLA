using SGA.Web.Models.Operaciones;

namespace SGA.Web.ViewModels.RegistrosUso;

public class RegistroUsoListViewModel
{
    public List<RegistroUsoDto> Registros { get; set; } = new();
    public string? FiltroTipo { get; set; }
}
