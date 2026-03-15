namespace SGA.Application.Dtos.Personas;

public class SaveEstudianteDto : SavePersonaDto
{
    public string Matricula { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; }
}
