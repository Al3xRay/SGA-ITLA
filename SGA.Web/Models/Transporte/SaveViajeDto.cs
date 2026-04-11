namespace SGA.Web.Models.Transporte;

public class SaveViajeDto
{
    public int RutaId { get; set; }
    public int AutobusId { get; set; }
    public int ConductorId { get; set; }
    public int? HorarioId { get; set; }
    public DateTime FechaProgramada { get; set; }
}
