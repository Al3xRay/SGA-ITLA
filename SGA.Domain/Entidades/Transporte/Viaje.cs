using SGA.Domain.Base;
using SGA.Domain.Entidades.Configuracion;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Entidades.Personas;

namespace SGA.Domain.Entidades.Transporte;

public class Viaje : AuditEntity
{
    public int RutaId { get; set; }
    public int AutobusId { get; set; }
    public int ConductorId { get; set; }              
    public int? HorarioId { get; set; }
    public int EstadoViajeId { get; set; }
    public DateTime FechaProgramada { get; set; }
    public DateTime? HoraInicioReal { get; set; }
    public DateTime? HoraFinReal { get; set; }
    public int OcupacionActual { get; set; }
    public string? Observaciones { get; set; }
    

    public Ruta Ruta { get; set; } = null!;
    public Autobus Autobus { get; set; } = null!;
    public Conductor Conductor { get; set; } = null!;  
    public Horario? Horario { get; set; }
    public EstadoViaje Estado { get; set; } = null!;
    public ICollection<RegistroUso> RegistrosAbordaje { get; set; } = new List<RegistroUso>();
    public ICollection<Incidencia> Incidencias { get; set; } = new List<Incidencia>();
}
