namespace SGA.Web.Models.Personas;

public class PersonaDto
{
    public int Id { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string NombreCompleto => $"{Nombre} {Apellido}";
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public int TipoPersonaId { get; set; }
    public string TipoPersonaNombre { get; set; } = string.Empty;
}
