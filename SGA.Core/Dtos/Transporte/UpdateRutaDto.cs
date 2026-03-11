using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Transporte;

public class UpdateRutaDto : DtoBase
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    public bool Activo { get; set; }
}
