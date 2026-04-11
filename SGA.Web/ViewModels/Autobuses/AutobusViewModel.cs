using System.ComponentModel.DataAnnotations;
using SGA.Web.Models.Transporte;

namespace SGA.Web.ViewModels.Autobuses;

public class AutobusListViewModel
{
    public List<AutobusDto> Autobuses { get; set; } = new();
}

public class AutobusFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La placa es requerida.")]
    [Display(Name = "Placa")]
    public string Placa { get; set; } = string.Empty;

    [Required(ErrorMessage = "La marca es requerida.")]
    [Display(Name = "Marca")]
    public string Marca { get; set; } = string.Empty;

    [Required(ErrorMessage = "El modelo es requerido.")]
    [Display(Name = "Modelo")]
    public string Modelo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La capacidad es requerida.")]
    [Range(1, 200, ErrorMessage = "La capacidad debe estar entre 1 y 200.")]
    [Display(Name = "Capacidad")]
    public int Capacidad { get; set; }

    [Display(Name = "Estado")]
    public int? EstadoAutobusId { get; set; }

    public SaveAutobusDto ToSaveDto() => new()
    {
        Placa = Placa, Marca = Marca, Modelo = Modelo,
        Capacidad = Capacidad, EstadoAutobusId = EstadoAutobusId ?? 1
    };

    public UpdateAutobusDto ToUpdateDto() => new()
    {
        Marca = Marca, Modelo = Modelo,
        Capacidad = Capacidad, EstadoAutobusId = EstadoAutobusId ?? 1
    };

    public static AutobusFormViewModel FromDto(AutobusDto dto)
    {
        var model = new AutobusFormViewModel
        {
            Id = dto.Id, Placa = dto.Placa, Marca = dto.Marca,
            Modelo = dto.Modelo, Capacidad = dto.Capacidad
        };

        // Try to match the status name to an ID if not directly provided in DTO
        if(!string.IsNullOrEmpty(dto.EstadoAutobusNombre))
        {
            if (dto.EstadoAutobusNombre == "Disponible") model.EstadoAutobusId = 1;
            else if (dto.EstadoAutobusNombre == "En Mantenimiento") model.EstadoAutobusId = 2;
            else if (dto.EstadoAutobusNombre == "Fuera de Servicio") model.EstadoAutobusId = 3;
        }

        return model;
    }
}
