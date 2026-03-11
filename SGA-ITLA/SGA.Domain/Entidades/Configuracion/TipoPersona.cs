using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Domain.Entidades.Configuracion;

public class TipoPersona
{
    public const int Estudiante = 1;
    public const int Administrativo = 2;
    public const int Conductor = 3;

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}
