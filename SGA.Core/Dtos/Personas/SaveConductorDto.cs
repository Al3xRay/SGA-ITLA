namespace SGA.Application.Dtos.Personas;

public class SaveConductorDto : SavePersonaDto
{
    public string LicenciaConducir { get; set; } = string.Empty;
    public DateTime FechaVencimientoLicencia { get; set; }
    public string CategoriaLicencia { get; set; } = string.Empty;
    public int EstadoConductorId { get; set; }
    public DateTime FechaContratacion { get; set; }
}
