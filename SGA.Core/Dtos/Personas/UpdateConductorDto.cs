using System;

namespace SGA.Application.Dtos.Personas;

public class UpdateConductorDto : UpdatePersonaDto
{
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string LicenciaConducir { get; set; } = string.Empty;
    public string CategoriaLicencia { get; set; } = string.Empty;
    public DateTime FechaVencimientoLicencia { get; set; }
    public DateTime FechaContratacion { get; set; }
}
