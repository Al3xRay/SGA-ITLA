namespace SGA.Application.Dtos.Operaciones;

public class SaveAutorizacionDto
{
    public int PersonaId { get; set; }
    public int TipoAutorizacionId { get; set; }
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public decimal MontoCobrado { get; set; }
    public int? EmitidoPorId { get; set; }
}
