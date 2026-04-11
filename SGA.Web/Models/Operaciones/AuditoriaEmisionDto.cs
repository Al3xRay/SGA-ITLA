using System;
using System.Collections.Generic;

namespace SGA.Web.Models.Operaciones;

public class AuditoriaEmisionDto
{
    public int EmisionId { get; set; }
    public string EmisorNombre { get; set; } = string.Empty;
    public string TitularNombre { get; set; } = string.Empty;
    public string TitularDocumento { get; set; } = string.Empty;
    public string TitularMatricula { get; set; } = string.Empty;
    public string TipoAutorizacion { get; set; } = string.Empty;
    public decimal MontoCobrado { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public IReadOnlyList<AuditoriaTicketDto> Consumos { get; set; } = new List<AuditoriaTicketDto>();
}
