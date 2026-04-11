using System.ComponentModel.DataAnnotations;

namespace SGA.Web.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es requerido")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "La matrícula es requerido")]
    [Display(Name = "Documento o Matrícula")]
    public string DocumentoIdentidad { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe especificar si es Estudiante o Empleado")]
    [Display(Name = "Tipo de Usuario")]
    public int TipoPersonaId { get; set; }

    public string? Telefono { get; set; }

    [Required(ErrorMessage = "Debe crear una contraseña")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = string.Empty;

    [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Contraseña")]
    public string ConfirmarContrasena { get; set; } = string.Empty;
}
