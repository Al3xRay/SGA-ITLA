using SGA.Domain.Entidades.Transporte;

namespace SGA.Domain.Repository;

public interface IHorarioRepository : IBaseRepository<Horario>
{
    Task<IReadOnlyList<Horario>> GetByRutaAsync(int rutaId);
}
