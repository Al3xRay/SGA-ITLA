using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;

namespace SGA.Domain.Repository;

public interface ITransaccionFinancieraRepository : IBaseRepository<TransaccionFinanciera>
{
    Task<decimal> GetIngresosTotalesAsync();
}
