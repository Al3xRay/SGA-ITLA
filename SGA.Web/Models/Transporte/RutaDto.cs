namespace SGA.Web.Models.Transporte;

public class RutaDto
{
    public int Id { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Origen { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public TimeSpan DuracionEstimada { get; set; }
    public List<ParadaDto> Paradas { get; set; } = new();
}
