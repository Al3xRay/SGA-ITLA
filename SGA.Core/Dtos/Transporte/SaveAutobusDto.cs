namespace SGA.Application.Dtos.Transporte;

public class SaveAutobusDto
{
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public int EstadoAutobusId { get; set; }
}
