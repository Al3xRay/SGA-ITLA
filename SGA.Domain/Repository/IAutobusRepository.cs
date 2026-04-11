using SGA.Domain.Entidades.Transporte;

namespace SGA.Domain.Repository;

public interface IAutobusRepository : IBaseRepository<Autobus>
{
    Task<Autobus?> GetByPlacaAsync(string placa);
    Task<IReadOnlyList<Autobus>> GetDisponiblesAsync(int estadoDisponibleId);
}
