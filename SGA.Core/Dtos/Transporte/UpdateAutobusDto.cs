using SGA.Domain.Entidades.Configuracion;
using SGA.Application.Dtos;

namespace SGAITLA.Application.Dtos.Transporte;

public class UpdateAutobusDto : DtoBase
{

    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }

    public EstadoAutobus Estado { get; set; }
    public bool Activo { get; set; }

}
