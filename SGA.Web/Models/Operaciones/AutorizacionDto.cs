namespace SGA.Web.Models.Operaciones;

public class AutorizacionDto
{
    public int Id { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int PersonaId { get; set; }
    public string PersonaNombre { get; set; } = string.Empty;
    public string TipoAutorizacionNombre { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaVencimiento { get; set; }
}
