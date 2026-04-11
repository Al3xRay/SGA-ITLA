using SGA.Domain.Entidades.Personas;

namespace SGA.Domain.Repository;

public interface IEstudianteRepository : IBaseRepository<Estudiante>
{
    Task<Estudiante?> GetByMatriculaAsync(string matricula);
}
