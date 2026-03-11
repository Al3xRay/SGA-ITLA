using SGA.Domain.Base;

namespace SGA.Domain.Entidades.Transporte;

public class Ruta : AuditEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Origen { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public TimeSpan DuracionEstimada { get; set; }

    public ICollection<Parada> Paradas { get; set; } = new List<Parada>();
    public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
    public ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}