using System.ComponentModel.DataAnnotations;
using SGA.Web.Models.Transporte;
using SGA.Web.Models.Personas;

namespace SGA.Web.ViewModels.Viajes;

public class ViajeListViewModel
{
    public List<ViajeDto> Viajes { get; set; } = new();
}

public class ViajeFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La ruta es requerida.")]
    [Display(Name = "Ruta")]
    public int RutaId { get; set; }

    [Required(ErrorMessage = "El autobús es requerido.")]
    [Display(Name = "Autobús")]
    public int AutobusId { get; set; }

    [Required(ErrorMessage = "El conductor es requerido.")]
    [Display(Name = "Conductor")]
    public int ConductorId { get; set; }

    [Required(ErrorMessage = "La fecha programada es requerida.")]
    [Display(Name = "Fecha y Hora Programada")]
    [DataType(DataType.DateTime)]
    public DateTime FechaProgramada { get; set; } = DateTime.Today.AddHours(7);

    public List<RutaDto> Rutas { get; set; } = new();
    public List<AutobusDto> Autobuses { get; set; } = new();
    public List<ConductorDto> Conductores { get; set; } = new();

    public SaveViajeDto ToSaveDto() => new()
    {
        RutaId = RutaId, AutobusId = AutobusId,
        ConductorId = ConductorId, FechaProgramada = FechaProgramada
    };
}

public class ViajeDetailViewModel
{
    public ViajeDto Viaje { get; set; } = new();

    [Display(Name = "Estado del Viaje")]
    public int EstadoViajeId { get; set; }

    [Display(Name = "Observaciones")]
    public string? Observaciones { get; set; }

    public UpdateViajeDto ToUpdateDto() => new()
    {
        EstadoViajeId = EstadoViajeId, Observaciones = Observaciones
    };
}
