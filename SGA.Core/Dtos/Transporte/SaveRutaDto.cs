using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Transporte;

public class SaveRutaDto : DtoBase
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
}
