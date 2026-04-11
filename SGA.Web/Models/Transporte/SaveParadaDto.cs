namespace SGA.Web.Models.Transporte;

public class SaveParadaDto
{
    public int RutaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public int Orden { get; set; }
    public TimeSpan TiempoDesdeOrigen { get; set; }
}

public class UpdateParadaDto : SaveParadaDto
{
}
