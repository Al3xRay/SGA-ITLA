using System.ComponentModel.DataAnnotations;
using SGA.Web.Models.Personas;

namespace SGA.Web.ViewModels.Conductores;

public class ConductorListViewModel
{
    public List<ConductorDto> Conductores { get; set; } = new();
}

public class ConductorFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es requerido.")]
    [Display(Name = "Apellido")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El documento es requerido.")]
    [Display(Name = "Documento de Identidad")]
    public string DocumentoIdentidad { get; set; } = string.Empty;

    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Display(Name = "Contraseña")]
    public string? Contrasena { get; set; }

    [Required(ErrorMessage = "El número de licencia es requerido.")]
    [Display(Name = "Número de Licencia")]
    public string LicenciaConducir { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de vencimiento de licencia es requerida.")]
    [Display(Name = "Vencimiento de Licencia")]
    [DataType(DataType.Date)]
    public DateTime FechaVencimientoLicencia { get; set; } = DateTime.Today.AddYears(1);

    [Required(ErrorMessage = "La categoría de licencia es requerida.")]
    [Display(Name = "Categoría de Licencia")]
    public string CategoriaLicencia { get; set; } = string.Empty;

    [Display(Name = "Estado del Conductor")]
    public int EstadoConductorId { get; set; } = 1;

    [Required(ErrorMessage = "La fecha de contratación es requerida.")]
    [Display(Name = "Fecha de Contratación")]
    [DataType(DataType.Date)]
    public DateTime FechaContratacion { get; set; } = DateTime.Today;

    public SaveConductorDto ToSaveDto() => new()
    {
        Nombre = Nombre, Apellido = Apellido, DocumentoIdentidad = DocumentoIdentidad,
        TipoPersonaId = 3,
        Telefono = Telefono, Direccion = Direccion,
        Contrasena = Contrasena,
        LicenciaConducir = LicenciaConducir, FechaVencimientoLicencia = FechaVencimientoLicencia,
        CategoriaLicencia = CategoriaLicencia, EstadoConductorId = EstadoConductorId,
        FechaContratacion = FechaContratacion
    };

    public UpdateConductorDto ToUpdateDto() => new()
    {
        Nombre = Nombre,
        Apellido = Apellido,
        DocumentoIdentidad = DocumentoIdentidad,
        Telefono = Telefono,
        Direccion = Direccion,
        Contrasena = Contrasena,
        LicenciaConducir = LicenciaConducir,
        CategoriaLicencia = CategoriaLicencia,
        FechaVencimientoLicencia = FechaVencimientoLicencia,
        FechaContratacion = FechaContratacion
    };

    public static ConductorFormViewModel FromDto(ConductorDto dto) => new()
    {
        Id = dto.Id, Nombre = dto.Nombre, Apellido = dto.Apellido,
        DocumentoIdentidad = dto.DocumentoIdentidad, Telefono = dto.Telefono,
        Direccion = dto.Direccion, LicenciaConducir = dto.LicenciaConducir,
        FechaVencimientoLicencia = dto.FechaVencimientoLicencia,
        CategoriaLicencia = dto.CategoriaLicencia, FechaContratacion = dto.FechaContratacion
    };
}
