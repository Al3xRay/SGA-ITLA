using SGAITLA.Domain.Entidades.Configuracion;
using SGAITLA.Domain.Entidades.Personas;
using SGAITLA.Domain.Entidades.Transporte;
using SGAITLA.Domain.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Operaciones;

public class RegistroUso
{
    public int Id { get; set; }
    public int PersonaId { get; set; }              
    public int ViajeId { get; set; }
    public int? AutorizacionId { get; set; }
    public DateTime FechaHora { get; set; }
    public bool AccesoPermitido { get; set; }
    public string? MotivoRechazo { get; set; }
    public int TipoRegistroId { get; set; }
    public int? ValidadoPorConductorId { get; set; } 

    public Persona Persona { get; set; } = null!;   
    public Viaje Viaje { get; set; } = null!;
    public Autorizacion? Autorizacion { get; set; }
    public TipoRegistro Tipo { get; set; } = null!;
    public Conductor? ValidadoPor { get; set; }
}
