namespace SGA.Application.Dtos.Transporte;

public class AutobusDto : DtoBase
{
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public string EstadoAutobusNombre { get; set; } = string.Empty;
}
