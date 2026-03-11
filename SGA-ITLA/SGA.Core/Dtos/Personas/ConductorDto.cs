using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
