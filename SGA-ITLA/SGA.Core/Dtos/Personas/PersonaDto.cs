using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Personas;

public class PersonaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string TipoPersona { get; set; } = string.Empty;
    public bool Activo { get; set; }


    public string? Matricula { get; set; }
    public string? LicenciaConducir { get; set; }
}
