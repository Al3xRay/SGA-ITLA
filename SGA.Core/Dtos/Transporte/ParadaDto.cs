namespace SGA.Application.Dtos.Transporte;

public class ParadaDto
{
    public int Id { get; set; }
    public int RutaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public int Orden { get; set; }
    public TimeSpan TiempoDesdeOrigen { get; set; }
}
