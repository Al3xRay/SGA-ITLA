namespace SGA.Application.Dtos.Operaciones;

public class IncidenciaDto
{
    public int Id { get; set; }
    public int ViajeId { get; set; }
    public string TipoIncidenciaNombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaReporte { get; set; }
    public bool EsGrave { get; set; }
    public string? EvidenciaUrl { get; set; }
    public string EstadoIncidenciaNombre { get; set; } = string.Empty;
    public string? Resolucion { get; set; }
    public DateTime? FechaResolucion { get; set; }
    public string ReportadoPorNombre { get; set; } = string.Empty;
}
