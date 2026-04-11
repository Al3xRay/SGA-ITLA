namespace SGA.Application.Dtos.Transporte;

public class UpdateRutaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Origen { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public TimeSpan DuracionEstimada { get; set; }
}
