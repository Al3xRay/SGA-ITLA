namespace SGA.Application.Dtos.Transporte;

public class ViajeDto : DtoBase
{
    public int RutaId { get; set; }
    public string RutaNombre { get; set; } = string.Empty;
    public int AutobusId { get; set; }
    public string AutobusPlaca { get; set; } = string.Empty;
    public int ConductorId { get; set; }
    public string ConductorNombre { get; set; } = string.Empty;
    public DateTime FechaProgramada { get; set; }
    public DateTime? HoraInicioReal { get; set; }
    public DateTime? HoraFinReal { get; set; }
    public int OcupacionActual { get; set; }
    public int Capacidad { get; set; }
    public string EstadoViajeNombre { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
}
