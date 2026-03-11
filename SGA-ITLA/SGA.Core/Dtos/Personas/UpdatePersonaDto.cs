using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;

namespace SGAITLA.Application.Dtos.Personas;

public class UpdatePersonaDto : DtoBase
{

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public bool Activo { get; set; }

}
