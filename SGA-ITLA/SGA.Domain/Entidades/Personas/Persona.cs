using SGAITLA.Domain.Base;
using SGAITLA.Domain.Entidades.Configuracion;
using SGAITLA.Domain.Entidades.Operaciones;
using SGA.Domain.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Personas;

public abstract class Persona : AuditEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public int TipoPersonaId { get; set; }

    public TipoPersona Tipo { get; set; } = null!;
    public ICollection<Autorizacion> Autorizaciones { get; set; } = new List<Autorizacion>();
    public ICollection<RegistroUso> RegistrosUso { get; set; } = new List<RegistroUso>();
}
