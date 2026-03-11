using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;

namespace SGAITLA.Application.Dtos.Operaciones;

public class UpdateAutorizacionDto : DtoBase
{

    public int Id { get; set; }
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public bool Activo { get; set; }

}
