using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Personas;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Personas
{
    public class EstudianteRepository : BaseRepository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Estudiante?> GetByMatriculaAsync(string matricula)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Matricula == matricula);
        }

        public async Task<IReadOnlyList<Estudiante>> GetByCarreraAsync(string carrera)
        {
            return await _dbSet
                .Where(e => e.Carrera == carrera)
                .ToListAsync();
        }
    }
}
