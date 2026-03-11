using SGA.Application.Dtos;


namespace SGA.Application.Dtos.Personas;

public class SaveConductorDto : DtoBase
{
    public int PersonaId { get; set; }
    public string LicenciaConducir { get; set; } = string.Empty;
    public DateTime? FechaVencimientoLicencia { get; set; }
}
