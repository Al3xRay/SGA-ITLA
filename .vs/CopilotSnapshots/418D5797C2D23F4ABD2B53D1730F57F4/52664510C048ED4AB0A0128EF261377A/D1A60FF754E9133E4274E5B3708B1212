using SGA.Domain.Entidades.Configuracion;
using SGA.Domain.Entidades.Personas;
using SGA.Domain.Entidades.Transporte;

namespace SGA.Domain.Entidades.Operaciones;

public class Incidencia
{
    public int Id { get; set; }
    public int ViajeId { get; set; }
    public int TipoIncidenciaId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaReporte { get; set; }
    public bool EsGrave { get; set; }
    public string? EvidenciaUrl { get; set; }
    public int EstadoIncidenciaId { get; set; }
    public string? Resolucion { get; set; }
    public DateTime? FechaResolucion { get; set; }
    public int ReportadoPorConductorId { get; set; }  

    public Viaje Viaje { get; set; } = null!;
    public TipoIncidencia Tipo { get; set; } = null!;
    public EstadoIncidencia Estado { get; set; } = null!;
    public Conductor ReportadoPor { get; set; } = null!;  
}