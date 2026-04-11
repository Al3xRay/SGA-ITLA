using System.ComponentModel.DataAnnotations;
using SGA.Web.Models.Operaciones;

namespace SGA.Web.ViewModels.Incidencias;

public class IncidenciaListViewModel
{
    public List<IncidenciaDto> Incidencias { get; set; } = new();
}

public class IncidenciaResolverViewModel
{
    public IncidenciaDto Incidencia { get; set; } = new();

    [Required(ErrorMessage = "La resolución es requerida.")]
    [Display(Name = "Resolución")]
    [MinLength(10, ErrorMessage = "La resolución debe tener al menos 10 caracteres.")]
    public string Resolucion { get; set; } = string.Empty;
}
