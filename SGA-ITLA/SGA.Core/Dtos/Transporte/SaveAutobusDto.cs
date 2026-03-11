using SGA.Application.Dtos;
using SGAITLA.Domain.Entidades.Configuracion;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Transporte;

public class SaveAutobusDto : DtoBase
{

    public string Placa {  get; set; } = string.Empty;
    public string Marca {  get; set; } = string.Empty;
    public string Modelo {  get; set; } = string.Empty;
    public int Capacidad {  get; set; }

    public EstadoAutobus Estado { get; set; } = EstadoAutobus.Disponible;

}


