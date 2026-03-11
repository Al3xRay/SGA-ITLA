using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Personas;


public class SaveEstudianteDto : DtoBase
{
    public int PersonaId { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string? Carrera { get; set; }
}
