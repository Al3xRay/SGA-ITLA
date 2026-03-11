using SGAITLA.Domain.Entidades.Configuracion;
using SGAITLA.Domain.Entidades.Operaciones;
using SGAITLA.Domain.Entidades.Transporte;
using SGA.Domain.Entidades.Transporte;
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

    public EstadoConductor EstadoConductor { get; set; } = EstadoConductor.Disponible;

    public DateTime FechaContratacion { get; set; }
    public ICollection<Viaje> ViajesAsignados { get; set; } = new List<Viaje>();
    public ICollection<Incidencia> IncidenciasReportadas { get; set; } = new List<Incidencia>();
}
