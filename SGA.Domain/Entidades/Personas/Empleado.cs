using SGAITLA.Domain.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Personas;

public class Empleado : Persona
{
    public string CodigoEmpleado { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public DateTime FechaContratacion { get; set; }

    public UsuarioEmpleado? Usuario { get; set; }
}
