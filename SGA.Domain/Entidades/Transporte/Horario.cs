using SGA.Domain.Base;

namespace SGA.Domain.Entidades.Transporte;

public class Horario : AuditEntity
{
    public int RutaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeSpan HoraSalida { get; set; }
    public TimeSpan HoraLlegadaEstimada { get; set; }

    public Ruta Ruta { get; set; } = null!;
    public ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
