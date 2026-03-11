using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Operaciones;

public class AutorizacionDto : DtoBase
{
    public int Id { get; set; }
    public int PersonaId { get; set; } 
    public string PersonaNombre { get; set; } = string.Empty;
    public string TipoAutorizacion { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public bool Activo { get; set; }
    public bool EsValida { get; set; }
}

