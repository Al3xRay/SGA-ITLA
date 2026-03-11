using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Transporte;

public class UpdateViajeDto : DtoBase
{
    public int Id { get; set; }
    public int RutaId { get; set; }
    public int AutobusId { get; set; }
    public int ConductorId { get; set; }
    public int EstadoViajeId { get; set; }
    public DateTime? FechaLlegadaReal { get; set; }
    public int OcupacionActual { get; set; }
    public bool Activo { get; set; }
}
