using SGAITLA.Domain.Base;
using SGA.Domain.Entidades.Configuracion;
using SGA.Domain.Entidades.Operaciones;
using SGAITLA.Domain.Entidades.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Operaciones;

public class Autorizacion : AuditEntity
{
    public int PersonaId { get; set; }              
    public int TipoAutorizacionId { get; set; }
    public decimal Saldo { get; set; }
    public int? ViajesRestantes { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaVencimiento { get; set; }

    public Persona Persona { get; set; } = null!;   
    public TipoAutorizacion Tipo { get; set; } = null!;
    public ICollection<RegistroUso> Consumos { get; set; } = new List<RegistroUso>();
}
