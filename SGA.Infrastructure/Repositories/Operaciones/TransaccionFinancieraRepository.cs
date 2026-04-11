using Microsoft.EntityFrameworkCore;
using SGA.Domain.Entidades.Operaciones;
using SGA.Persistence.Contexts;
using SGA.Persistence.Repositories.Base;
using SGA.Domain.Repository;

namespace SGA.Persistence.Repositories.Operaciones;

public class TransaccionFinancieraRepository : BaseRepository<TransaccionFinanciera>, ITransaccionFinancieraRepository
{
    private readonly ApplicationDbContext _context;
    
    public TransaccionFinancieraRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<decimal> GetIngresosTotalesAsync()
    {
        return await _context.TransaccionesFinanciera
            .Where(t => t.Activo && t.Tipo == "Ingreso")
            .SumAsync(t => t.Monto);
    }
}
