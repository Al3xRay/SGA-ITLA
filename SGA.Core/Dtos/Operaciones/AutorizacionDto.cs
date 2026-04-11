namespace SGA.Application.Dtos.Operaciones;

public class AutorizacionDto : DtoBase
{
    public int PersonaId { get; set; }
    public string PersonaNombre { get; set; } = string.Empty;
    public string TipoAutorizacionNombre { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaVencimiento { get; set; }
}
