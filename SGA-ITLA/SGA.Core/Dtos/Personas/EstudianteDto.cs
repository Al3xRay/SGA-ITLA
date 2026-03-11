using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Personas;

public class EstudianteDto : DtoBase
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string? Carrera { get; set; }
    public bool Activo { get; set; }
}
