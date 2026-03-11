using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Transporte;

public class ViajeDto : DtoBase
{
    public int Id { get; set; }
    public int RutaId { get; set; }
    public string RutaNombre { get; set; } = string.Empty;
    public int AutobusId { get; set; }
    public string AutobusPlaca { get; set; } = string.Empty;
    public int ConductorId { get; set; }
    public string ConductorNombre { get; set; } = string.Empty;
    public DateTime FechaSalida { get; set; }
    public DateTime? FechaLlegadaEstimada { get; set; }
    public DateTime? FechaLlegadaReal { get; set; }
    public int EstadoViajeId { get; set; }
    public string EstadoViajeNombre { get; set; } = string.Empty;
    public int OcupacionActual { get; set; }
    public int CapacidadAutobus { get; set; }
    public bool Activo { get; set; }
}
