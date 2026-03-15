namespace SGA.Application.Dtos.Personas;

public class ConductorDto : PersonaDto
{
    public string LicenciaConducir { get; set; } = string.Empty;
    public DateTime FechaVencimientoLicencia { get; set; }
    public string CategoriaLicencia { get; set; } = string.Empty;
    public string EstadoConductorNombre { get; set; } = string.Empty;
    public DateTime FechaContratacion { get; set; }
}
