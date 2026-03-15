namespace SGA.Application.Dtos.Operaciones;

public class UpdateAutorizacionDto
{
    public decimal? Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime? FechaVencimiento { get; set; }
}
