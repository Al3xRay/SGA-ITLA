using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Transporte;

public class SaveViajeDto : DtoBase
{
    public int RutaId { get; set; }
    public int AutobusId { get; set; }
    public int ConductorId { get; set; }
    public DateTime FechaSalida { get; set; }
    public DateTime? FechaLlegadaEstimada { get; set; }
}
