namespace SGA.Application.Dtos.Operaciones;

public class ResultadoAccesoDto
{
    public bool AccesoPermitido { get; set; }
    public string? MotivoRechazo { get; set; }
    public string PersonaNombre { get; set; } = string.Empty;
    public int? RegistroUsoId { get; set; }
}
