namespace SGA.Application.Dtos.Personas;

public class EstudianteDto : PersonaDto
{
    public string Matricula { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; }
}
