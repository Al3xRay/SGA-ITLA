namespace SGA.Web.Models.Transporte;

public class AutobusDto
{
    public int Id { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public string EstadoAutobusNombre { get; set; } = string.Empty;
}
