using SGA.Domain.Base;
using SGA.Domain.Entidades.Personas;

namespace SGA.Domain.Entidades.Operaciones;

public class TransaccionFinanciera : AuditEntity
{
    public string Concepto { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string MetodoPago { get; set; } = string.Empty; 
    public string Referencia { get; set; } = string.Empty; 
    public int? ProcesadoPorId { get; set; }

    public Persona? ProcesadoPor { get; set; }
}
