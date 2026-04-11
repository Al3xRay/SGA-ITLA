namespace SGA.Web.Models.Operaciones;

public class RegistroUsoDto
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public string PersonaNombre { get; set; } = string.Empty;
    public int ViajeId { get; set; }
    public DateTime FechaHora { get; set; }
    public bool AccesoPermitido { get; set; }
    public string? MotivoRechazo { get; set; }
    public string TipoRegistroNombre { get; set; } = string.Empty;
}
