using SGAITLA.Domain.Entidades.Configuracion;
using SGAITLA.Domain.Entidades.Operaciones;
using SGAITLA.Domain.Entidades.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Personas;

public class Conductor : Persona
{
    public string LicenciaConducir { get; set; } = string.Empty;
    public DateTime FechaVencimientoLicencia { get; set; }
    public string CategoriaLicencia { get; set; } = string.Empty;
    public int EstadoConductorId { get; set; }
    public DateTime FechaContratacion { get; set; }

    public EstadoConductor Estado { get; set; } = null!;
    public ICollection<Viaje> ViajesAsignados { get; set; } = new List<Viaje>();
    public ICollection<Incidencia> IncidenciasReportadas { get; set; } = new List<Incidencia>();
}
