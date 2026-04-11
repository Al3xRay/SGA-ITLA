using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;

namespace SGA.Application.Interfaces;

public interface IAccesoService
{
    Task<OperationResult<ResultadoAccesoDto>> ValidarAccesoAsync(ValidarAccesoDto dto);
}
