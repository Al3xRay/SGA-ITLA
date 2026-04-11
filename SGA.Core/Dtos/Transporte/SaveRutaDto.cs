namespace SGA.Application.Dtos.Transporte;

public class SaveRutaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Origen { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public TimeSpan DuracionEstimada { get; set; }
    public List<SaveParadaDto> Paradas { get; set; } = new();
}
