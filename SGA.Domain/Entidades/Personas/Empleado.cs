namespace SGA.Domain.Entidades.Personas;

public class Empleado : Persona
{
    public string CodigoEmpleado { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public DateTime FechaContratacion { get; set; }
}
