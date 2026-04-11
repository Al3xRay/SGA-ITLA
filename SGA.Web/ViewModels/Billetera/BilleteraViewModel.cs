using SGA.Web.Models.Operaciones;
using SGA.Web.Models.Transporte;

namespace SGA.Web.ViewModels.Billetera;

public class BilleteraViewModel
{
    public List<AutorizacionDto> Autorizaciones { get; set; } = new();
    public List<RegistroUsoDto> HistorialViajes { get; set; } = new();

    public AutorizacionDto? AutorizacionVigente =>
        Autorizaciones.FirstOrDefault(a => a.Activo && a.FechaVencimiento >= DateTime.Today);

    public decimal SaldoTotal =>
        Autorizaciones
            .Where(a => a.Activo && a.FechaVencimiento >= DateTime.Today)
            .Sum(a => a.Saldo);

    public int? ViajesRestantes =>
        AutorizacionVigente?.ViajesRestantes;

    public int TotalViajesRealizados =>
        HistorialViajes.Count(r => r.AccesoPermitido);

    public int TotalAccesosDenegados =>
        HistorialViajes.Count(r => !r.AccesoPermitido);
}

public class RutasPublicasViewModel
{
    public List<RutaDto> Rutas { get; set; } = new();
    public string? Busqueda { get; set; }

    public List<RutaDto> RutasFiltradas => string.IsNullOrWhiteSpace(Busqueda)
        ? Rutas
        : Rutas.Where(r =>
            r.Nombre.Contains(Busqueda, StringComparison.OrdinalIgnoreCase) ||
            r.Origen.Contains(Busqueda, StringComparison.OrdinalIgnoreCase) ||
            r.Destino.Contains(Busqueda, StringComparison.OrdinalIgnoreCase))
          .ToList();
}
