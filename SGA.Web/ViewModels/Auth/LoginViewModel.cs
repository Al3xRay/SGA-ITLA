using System.ComponentModel.DataAnnotations;

namespace SGA.Web.ViewModels.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "El usuario es requerido.")]
    [Display(Name = "Matrícula / Documento")]
    public string Usuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Contrasena { get; set; } = string.Empty;
}
