using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Personas;

public class SaveConductorDto : DtoBase
{
    public int PersonaId { get; set; }
    public string LicenciaConducir { get; set; } = string.Empty;
    public DateTime? FechaVencimientoLicencia { get; set; }
}
