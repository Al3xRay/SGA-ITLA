using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Operaciones;

public class IncidenciaDto : DtoBase
{
    public int Id { get; set; }
    public int ViajeId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaReporte { get; set; }
    public bool Resuelta { get; set; }
}