using SGA.Domain.Entidades.Transporte;

namespace SGA.Domain.Repository;

public interface IViajeRepository : IBaseRepository<Viaje>
{
    Task<IReadOnlyList<Viaje>> GetViajesByFechaAsync(DateTime fecha);
    Task<IReadOnlyList<Viaje>> GetByConductorYFechaAsync(int conductorId, DateTime fecha);
    Task<Viaje?> GetViajeActivoByAutobusAsync(int autobusId);
}
