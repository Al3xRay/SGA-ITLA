namespace SGA.Application.Dtos.Transporte;

public class UpdateViajeDto
{
    public int EstadoViajeId { get; set; }
    public DateTime? HoraInicioReal { get; set; }
    public DateTime? HoraFinReal { get; set; }
    public string? Observaciones { get; set; }
}
