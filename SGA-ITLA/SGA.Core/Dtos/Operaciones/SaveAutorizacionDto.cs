using SGA.Application.Dtos;
using SGAITLA.Application.Dtos.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Operaciones;

public class SaveAutorizacionDto : DtoBase
{

    public int PersonaId { get; set; }
    public int TipoAutorizacionId { get; set; }
    public decimal SaldoInicial { get; set; }
    public int? ViajesIniciales { get; set; }
    public DateTime FechaVencimiento { get; set; }

}
