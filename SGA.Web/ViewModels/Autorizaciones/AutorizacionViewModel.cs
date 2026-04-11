using System.ComponentModel.DataAnnotations;
using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Personas;

namespace SGA.Web.ViewModels.Autorizaciones;

public class AutorizacionListViewModel
{
    public List<AutorizacionDto> Autorizaciones { get; set; } = new();
}

public class AutorizacionFormViewModel
{
    [Required(ErrorMessage = "La persona es requerida.")]
    [Display(Name = "Persona")]
    public int PersonaId { get; set; }

    [Required(ErrorMessage = "El tipo de autorización es requerido.")]
    [Display(Name = "Tipo de Autorización")]
    public int TipoAutorizacionId { get; set; }

    [Display(Name = "Saldo")]
    public decimal Saldo { get; set; }

    [Display(Name = "Viajes Restantes")]
    public int? ViajesRestantes { get; set; }

    [Display(Name = "Fecha de Vencimiento")]
    [DataType(DataType.Date)]
    public DateTime? FechaVencimiento { get; set; }

    [Display(Name = "Efectivo Cobrado")]
    [Required(ErrorMessage = "Indique el monto cobrado.")]
    public decimal MontoCobrado { get; set; }

    public SaveAutorizacionDto ToSaveDto() => new()
    {
        PersonaId = PersonaId, 
        TipoAutorizacionId = TipoAutorizacionId,
        Saldo = Saldo, 
        ViajesRestantes = ViajesRestantes, 
        FechaVencimiento = FechaVencimiento ?? DateTime.MaxValue,
        MontoCobrado = MontoCobrado
    };
}
