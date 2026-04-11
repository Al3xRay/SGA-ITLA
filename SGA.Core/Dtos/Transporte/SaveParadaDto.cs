using System.ComponentModel.DataAnnotations;

namespace SGA.Application.Dtos.Transporte;

public class SaveParadaDto
{
    [Required]
    public int RutaId { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public int Orden { get; set; }
    public TimeSpan TiempoDesdeOrigen { get; set; }
}

public class UpdateParadaDto : SaveParadaDto
{
}
