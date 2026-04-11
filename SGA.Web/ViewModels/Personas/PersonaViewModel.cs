using System.ComponentModel.DataAnnotations;
using SGA.Web.Models.Personas;

namespace SGA.Web.ViewModels.Personas;

public class PersonaListViewModel
{
    public List<PersonaDto> Personas { get; set; } = new();
}

public class PersonaFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es requerido.")]
    [Display(Name = "Apellido")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El documento de identidad es requerido.")]
    [Display(Name = "Documento de Identidad")]
    public string DocumentoIdentidad { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de persona es requerido.")]
    [Display(Name = "Tipo de Persona")]
    public int TipoPersonaId { get; set; }

    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Display(Name = "Fecha de Nacimiento")]
    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }

    [Display(Name = "Contraseña")]
    public string? Contrasena { get; set; }

    public SavePersonaDto ToSaveDto() => new()
    {
        Nombre = Nombre, Apellido = Apellido, DocumentoIdentidad = DocumentoIdentidad,
        TipoPersonaId = TipoPersonaId, Telefono = Telefono,
        Direccion = Direccion, FechaNacimiento = FechaNacimiento,
        Contrasena = Contrasena
    };

    public UpdatePersonaDto ToUpdateDto() => new()
    {
        Nombre = Nombre, Apellido = Apellido,
        Telefono = Telefono, Direccion = Direccion,
        Contrasena = Contrasena
    };

    public static PersonaFormViewModel FromDto(PersonaDto dto) => new()
    {
        Id = dto.Id, Nombre = dto.Nombre, Apellido = dto.Apellido,
        DocumentoIdentidad = dto.DocumentoIdentidad, Telefono = dto.Telefono,
        Direccion = dto.Direccion, FechaNacimiento = dto.FechaNacimiento,
        TipoPersonaId = dto.TipoPersonaId
    };
}
