namespace SGA.Domain.Base;

public abstract class AuditEntity
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public bool Activo { get; set; }
}
