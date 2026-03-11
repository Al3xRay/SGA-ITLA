using SGA.Application.Dtos;

namespace SGAITLA.Application.Dtos.Personas;

public class ConductorDto : DtoBase
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string LicenciaConducir { get; set; } = string.Empty;
    public DateTime? FechaVencimientoLicencia { get; set; }
    public bool Activo { get; set; }
}
