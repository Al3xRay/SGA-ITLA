using SGAITLA.Domain.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Personas;

public class Administrador : Persona
{
    public string NivelAcceso { get; set; } = string.Empty;
    public string AreaResponsable { get; set; } = string.Empty;
    public DateTime FechaAsignacion { get; set; }

    public UsuarioAdministrador? Usuario { get; set; }
}
