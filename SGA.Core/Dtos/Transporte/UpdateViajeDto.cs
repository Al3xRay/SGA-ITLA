using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Transporte;

public class UpdateViajeDto : DtoBase
{
    public int Id { get; set; }
    public int RutaId { get; set; }
    public int AutobusId { get; set; }
    public int ConductorId { get; set; }
    public int EstadoViajeId { get; set; }
    public DateTime? FechaLlegadaReal { get; set; }
    public int OcupacionActual { get; set; }
    public bool Activo { get; set; }
}
