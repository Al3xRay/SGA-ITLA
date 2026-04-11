using SGA.Web.Models.Transporte;

namespace SGA.Web.Models.Operaciones;

public class AuditoriaViajeDto
{
    public ViajeDto Viaje { get; set; } = null!;
    public List<AuditoriaTicketDto> Pasajeros { get; set; } = new List<AuditoriaTicketDto>();
}

public class AuditoriaTicketDto
{
    public int RegistroId { get; set; }
    public string PersonaNombre { get; set; } = string.Empty;
    public string PersonaDocumento { get; set; } = string.Empty;
    public string PersonaMatricula { get; set; } = string.Empty;
    public string TipoViaje { get; set; } = string.Empty;
    public decimal MontoDescontado { get; set; } 
    public DateTime FechaUso { get; set; }
    public bool AccesoPermitido { get; set; }
    public string MotivoRechazo { get; set; } = string.Empty;
}

public class AuditoriaGeneralDto
{
    public int ViajesRealizados { get; set; }
    public int IncidentesReportados { get; set; }
    public int PasajerosTotales { get; set; }
    public decimal RecaudadoPorTicketsSueltos { get; set; }
    public int AutobusesActivos { get; set; }
    public int RutasTotales { get; set; }
    public int ViajesProgramados { get; set; }
    public int IncidenciasPendientes { get; set; }
}
