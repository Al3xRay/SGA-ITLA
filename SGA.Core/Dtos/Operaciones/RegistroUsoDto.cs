using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Operaciones;

public class RegistroUsoDto : DtoBase
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public string PersonaNombre { get; set; } = string.Empty;
    public int ViajeId { get; set; }
    public int? AutorizacionId { get; set; }
    public DateTime FechaHora { get; set; }
    public bool AccesoPermitido { get; set; }
    public int TipoRegistroId { get; set; }
    public string TipoRegistroNombre { get; set; } = string.Empty;
}
