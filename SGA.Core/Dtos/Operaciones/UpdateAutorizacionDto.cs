using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Operaciones;

public class UpdateAutorizacionDto : DtoBase
{

    public int Id { get; set; }
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public bool Activo { get; set; }

}
