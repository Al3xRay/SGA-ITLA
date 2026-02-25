using SGAITLA.Domain.Entidades.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Usuarios;

public class UsuarioEstudiante
{
    public int Id { get; set; }
    public int EstudianteId { get; set; }
    public string Correo { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; }

    public Estudiante Estudiante { get; set; } = null!;
}
