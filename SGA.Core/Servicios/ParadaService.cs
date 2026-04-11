using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class ParadaService : IParadaService
{
    private readonly IUnitOfWork _unitOfWork;

    public ParadaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<ParadaDto>>> GetAllAsync()
    {
        var paradas = await _unitOfWork.Paradas.GetAllAsync();
        var dtos = paradas.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<ParadaDto>>.Ok(dtos);
    }

    public async Task<OperationResult<ParadaDto>> GetByIdAsync(int id)
    {
        var parada = await _unitOfWork.Paradas.GetByIdAsync(id);
        if (parada == null)
            return OperationResult<ParadaDto>.Fail("Parada no encontrada.");

        return OperationResult<ParadaDto>.Ok(MapToDto(parada));
    }

    public async Task<OperationResult<IReadOnlyList<ParadaDto>>> GetByRutaIdAsync(int rutaId)
    {
        var paradas = await _unitOfWork.Paradas.GetAllAsync();
        var dtos = paradas.Where(p => p.RutaId == rutaId).Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<ParadaDto>>.Ok(dtos);
    }

    public async Task<OperationResult> SaveAsync(SaveParadaDto dto)
    {
        var parada = new Parada
        {
            RutaId = dto.RutaId,
            Nombre = dto.Nombre,
            Ubicacion = dto.Ubicacion,
            Orden = dto.Orden,
            TiempoDesdeOrigen = dto.TiempoDesdeOrigen
        };

        await _unitOfWork.Paradas.AddAsync(parada);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Parada agregada exitosamente.");
    }

    public async Task<OperationResult> UpdateAsync(int id, UpdateParadaDto dto)
    {
        var parada = await _unitOfWork.Paradas.GetByIdAsync(id);
        if (parada == null)
            return OperationResult.Fail("Parada no encontrada.");

        parada.Nombre = dto.Nombre;
        parada.Ubicacion = dto.Ubicacion;
        parada.Orden = dto.Orden;
        parada.TiempoDesdeOrigen = dto.TiempoDesdeOrigen;

        _unitOfWork.Paradas.Update(parada);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Parada actualizada exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var parada = await _unitOfWork.Paradas.GetByIdAsync(id);
        if (parada == null)
            return OperationResult.Fail("Parada no encontrada.");

        _unitOfWork.Paradas.Delete(parada);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Parada eliminada exitosamente.");
    }

    private static ParadaDto MapToDto(Parada p) => new()
    {
        Id = p.Id,
        RutaId = p.RutaId,
        Nombre = p.Nombre,
        Ubicacion = p.Ubicacion,
        Orden = p.Orden,
        TiempoDesdeOrigen = p.TiempoDesdeOrigen
    };
}
