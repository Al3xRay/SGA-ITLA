namespace SGA.Web.Models.Operaciones;

public class SaveIncidenciaDto
{
    public int ViajeId { get; set; }
    public int TipoIncidenciaId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool EsGrave { get; set; }
    public string? EvidenciaUrl { get; set; }
    public int ReportadoPorConductorId { get; set; }
}
