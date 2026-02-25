using SGAITLA.Domain.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Personas;

public class Estudiante : Persona
{
    public string Matricula { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; }

    public UsuarioEstudiante? Usuario { get; set; }
}
