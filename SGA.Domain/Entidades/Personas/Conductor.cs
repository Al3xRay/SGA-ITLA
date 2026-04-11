using SGA.Domain.Entidades.Configuracion;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Entidades.Transporte;

namespace SGA.Domain.Entidades.Personas;

public class Conductor : Persona
{
    public string LicenciaConducir { get; set; } = string.Empty;
    public DateTime FechaVencimientoLicencia { get; set; }
    public string CategoriaLicencia { get; set; } = string.Empty;
    public DateTime FechaContratacion { get; set; }

    public ICollection<Viaje> ViajesAsignados { get; set; } = new List<Viaje>();
    public ICollection<Incidencia> IncidenciasReportadas { get; set; } = new List<Incidencia>();
}
