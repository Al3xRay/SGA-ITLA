using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Personas;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;

namespace SGA.Persistence.Repositories.Personas
{
    public class EmpleadoRepository : BaseRepository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Empleado?> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.CodigoEmpleado == codigo);
        }

        public async Task<IReadOnlyList<Empleado>> GetByDepartamentoAsync(string departamento)
        {
            return await _dbSet
                .Where(e => e.Departamento == departamento)
                .ToListAsync();
        }
    }
}
