namespace SGA.Web.Models.Operaciones;

public class TransaccionFinancieraDto
{
    public int Id { get; set; }
    public string Concepto { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;
    public string? ProcesadoPorNombre { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public class SaveTransaccionFinancieraDto
{
    public string Concepto { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string MetodoPago { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;
    public int? ProcesadoPorId { get; set; }
}
