using SGA.Application.Dtos;
using SGA.Application.Dtos.Operaciones;

namespace SGA.Application.Dtos.Operaciones;

public class SaveAutorizacionDto : DtoBase
{

    public int PersonaId { get; set; }
    public int TipoAutorizacionId { get; set; }
    public decimal SaldoInicial { get; set; }
    public int? ViajesIniciales { get; set; }
    public DateTime FechaVencimiento { get; set; }

}
