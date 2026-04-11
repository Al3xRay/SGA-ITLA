namespace SGA.Web.Models.Transporte;

public class UpdateAutobusDto
{
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public int EstadoAutobusId { get; set; }
}
