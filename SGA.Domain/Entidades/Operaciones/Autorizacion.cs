using SGA.Domain.Base;
using SGA.Domain.Entidades.Configuracion;
using SGA.Domain.Entidades.Personas;
namespace SGA.Domain.Entidades.Operaciones;

public class Autorizacion : AuditEntity
{
    public int PersonaId { get; set; }              
    public int TipoAutorizacionId { get; set; }
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public int? EmitidoPorId { get; set; }
    public decimal MontoCobrado { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaVencimiento { get; set; }

    public Persona Persona { get; set; } = null!;   
    public Persona? EmitidoPor { get; set; }
    public TipoAutorizacion Tipo { get; set; } = null!;
    public ICollection<RegistroUso> Consumos { get; set; } = new List<RegistroUso>();
}
