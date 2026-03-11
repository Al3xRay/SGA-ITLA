using SGA.Application.Dtos;
using SGA.Domain.Entidades.Configuracion;

namespace SGA.Application.Dtos.Transporte;

public class SaveAutobusDto : DtoBase
{

    public string Placa {  get; set; } = string.Empty;
    public string Marca {  get; set; } = string.Empty;
    public string Modelo {  get; set; } = string.Empty;
    public int Capacidad {  get; set; }

    public EstadoAutobus Estado { get; set; } = EstadoAutobus.Disponible;

}


