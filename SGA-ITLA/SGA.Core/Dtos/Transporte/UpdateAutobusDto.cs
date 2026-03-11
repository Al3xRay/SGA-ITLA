using SGAITLA.Domain.Entidades.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;

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
