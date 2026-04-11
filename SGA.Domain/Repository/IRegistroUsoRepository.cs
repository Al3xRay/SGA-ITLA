using SGA.Domain.Entidades.Operaciones;

namespace SGA.Domain.Repository;

public interface IRegistroUsoRepository
{
    Task<RegistroUso?> GetByIdAsync(int id);
    Task<IReadOnlyList<RegistroUso>> GetAllAsync();
    Task<IReadOnlyList<RegistroUso>> GetByViajeAsync(int viajeId);
    Task<IReadOnlyList<RegistroUso>> GetByPersonaAsync(int personaId);
    Task AddAsync(RegistroUso entity);
    void Update(RegistroUso entity);
    void Delete(RegistroUso entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id);
}
