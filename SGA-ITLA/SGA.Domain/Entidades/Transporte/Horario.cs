using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Domain.Entidades.Transporte;
using SGAITLA.Domain.Base;

namespace SGAITLA.Domain.Entidades.Transporte;

public class Horario : AuditEntity
{
    public int RutaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeSpan HoraSalida { get; set; }
    public TimeSpan HoraLlegadaEstimada { get; set; }

    public Ruta Ruta { get; set; } = null!;
    public ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
