using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Transporte;

public class SaveViajeDto : DtoBase
{
    public int RutaId { get; set; }
    public int AutobusId { get; set; }
    public int ConductorId { get; set; }
    public DateTime FechaSalida { get; set; }
    public DateTime? FechaLlegadaEstimada { get; set; }
}
