using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Personas;


public class SaveEstudianteDto : DtoBase
{
    public int PersonaId { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string? Carrera { get; set; }
}
