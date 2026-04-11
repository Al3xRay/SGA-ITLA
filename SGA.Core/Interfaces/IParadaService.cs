using SGA.Application.Base;
using SGA.Application.Dtos.Transporte;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IParadaService : IBaseService<ParadaDto, SaveParadaDto, UpdateParadaDto>
{
    Task<OperationResult<IReadOnlyList<ParadaDto>>> GetByRutaIdAsync(int rutaId);
}
