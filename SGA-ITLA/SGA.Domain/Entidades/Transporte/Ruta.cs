using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;
using SGA.Domain.Entidades.Transporte;


namespace SGAITLA.Domain.Entidades.Transporte;

public class Ruta : AuditEntity
{
    public string Codigo { get; set; } = string.Empty;  
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; } = string.Empty;
    public string Origen { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public TimeSpan? HoraInicio { get; set; }  
    public TimeSpan? HoraFin { get; set; }     
    public TimeSpan DuracionEstimada { get; set; }

    public ICollection<Parada> Paradas { get; set; } = new List<Parada>();
    public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
    public ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}