using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Personas;

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
