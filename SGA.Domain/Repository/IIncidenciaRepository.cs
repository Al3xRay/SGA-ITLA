using SGA.Domain.Entidades.Operaciones;

namespace SGA.Domain.Repository;

public interface IIncidenciaRepository
{
    Task<Incidencia?> GetByIdAsync(int id);
    Task<IReadOnlyList<Incidencia>> GetAllAsync();
    Task<IReadOnlyList<Incidencia>> GetByViajeAsync(int viajeId);
    Task AddAsync(Incidencia entity);
    void Update(Incidencia entity);
    void Delete(Incidencia entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id);
}
