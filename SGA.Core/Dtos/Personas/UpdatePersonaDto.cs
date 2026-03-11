using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Personas;

public class UpdatePersonaDto : DtoBase
{

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public bool Activo { get; set; }

}
