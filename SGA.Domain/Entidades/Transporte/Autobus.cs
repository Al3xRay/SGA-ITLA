using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SGAITLA.Domain.Base;
using SGAITLA.Domain.Entidades.Configuracion;


namespace SGAITLA.Domain.Entidades.Transporte;

public class Autobus : AuditEntity
{
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public int EstadoAutobusId { get; set; }

    public EstadoAutobus Estado { get; set; } = null!;
    public ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
