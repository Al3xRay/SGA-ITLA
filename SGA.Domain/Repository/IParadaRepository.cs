using SGA.Domain.Entidades.Transporte;

namespace SGA.Domain.Repository;

public interface IParadaRepository
{
    Task<Parada?> GetByIdAsync(int id);
    Task<IReadOnlyList<Parada>> GetAllAsync();
    Task<IReadOnlyList<Parada>> GetByRutaAsync(int rutaId);
    Task AddAsync(Parada entity);
    void Update(Parada entity);
    void Delete(Parada entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id);
}
