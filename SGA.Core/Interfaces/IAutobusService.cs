using SGA.Application.Base;
using SGA.Application.Dtos.Transporte;

namespace SGA.Application.Interfaces;

public interface IAutobusService : IBaseService<AutobusDto, SaveAutobusDto, UpdateAutobusDto>
{
    Task<Domain.Base.OperationResult<IReadOnlyList<AutobusDto>>> GetDisponiblesAsync();
}
