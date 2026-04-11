using System.ComponentModel.DataAnnotations;
using SGA.Web.Models.Transporte;

namespace SGA.Web.ViewModels.Rutas;

public class RutaListViewModel
{
    public List<RutaDto> Rutas { get; set; } = new();
}

public class RutaFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es requerida.")]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El origen es requerido.")]
    [Display(Name = "Origen")]
    public string Origen { get; set; } = string.Empty;

    [Required(ErrorMessage = "El destino es requerido.")]
    [Display(Name = "Destino")]
    public string Destino { get; set; } = string.Empty;

    [Required(ErrorMessage = "La duración estimada es requerida.")]
    [Display(Name = "Duración Estimada (hh:mm:ss)")]
    public string DuracionEstimadaStr { get; set; } = "00:45:00";

    public TimeSpan DuracionEstimada =>
        TimeSpan.TryParse(DuracionEstimadaStr, out var ts) ? ts : TimeSpan.Zero;

    public List<SaveParadaDto> Paradas { get; set; } = new();

    public SaveRutaDto ToSaveDto() => new()
    {
        Nombre = Nombre, Descripcion = Descripcion,
        Origen = Origen, Destino = Destino, DuracionEstimada = DuracionEstimada,
        Paradas = Paradas
    };

    public UpdateRutaDto ToUpdateDto() => new()
    {
        Nombre = Nombre, Descripcion = Descripcion,
        Origen = Origen, Destino = Destino, DuracionEstimada = DuracionEstimada
    };

    public static RutaFormViewModel FromDto(RutaDto dto) => new()
    {
        Id = dto.Id, Nombre = dto.Nombre, Descripcion = dto.Descripcion,
        Origen = dto.Origen, Destino = dto.Destino,
        DuracionEstimadaStr = dto.DuracionEstimada.ToString(@"hh\:mm\:ss")
    };
}
