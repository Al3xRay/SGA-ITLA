using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class AutorizacionService : IAutorizacionService
{
    private readonly IUnitOfWork _unitOfWork;

    public AutorizacionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<AutorizacionDto>>> GetAllAsync()
    {
        var autorizaciones = await _unitOfWork.Autorizaciones.GetAllAsync();
        var dtos = autorizaciones.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<AutorizacionDto>>.Ok(dtos);
    }

    public async Task<OperationResult<AutorizacionDto>> GetByIdAsync(int id)
    {
        var autorizacion = await _unitOfWork.Autorizaciones.GetByIdAsync(id);
        if (autorizacion == null)
            return OperationResult<AutorizacionDto>.Fail("Autorización no encontrada.");

        return OperationResult<AutorizacionDto>.Ok(MapToDto(autorizacion));
    }

    public async Task<OperationResult> SaveAsync(SaveAutorizacionDto dto)
    {
        if (!await _unitOfWork.Personas.ExistsAsync(dto.PersonaId))
            return OperationResult.Fail("La persona especificada no existe.");

        if (dto.FechaVencimiento <= DateTime.UtcNow)
            return OperationResult.Fail("La fecha de vencimiento debe ser futura.");

        var autorizacion = new Autorizacion
        {
            PersonaId = dto.PersonaId,
            TipoAutorizacionId = dto.TipoAutorizacionId,
            Saldo = dto.Saldo,
            ViajesRestantes = dto.ViajesRestantes,
            FechaEmision = DateTime.UtcNow,
            FechaVencimiento = dto.FechaVencimiento
        };

        await _unitOfWork.Autorizaciones.AddAsync(autorizacion);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Autorización emitida exitosamente.");
    }

    public async Task<OperationResult> UpdateAsync(int id, UpdateAutorizacionDto dto)
    {
        var autorizacion = await _unitOfWork.Autorizaciones.GetByIdAsync(id);
        if (autorizacion == null)
            return OperationResult.Fail("Autorización no encontrada.");

        if (dto.Saldo.HasValue) autorizacion.Saldo = dto.Saldo.Value;
        if (dto.ViajesRestantes.HasValue) autorizacion.ViajesRestantes = dto.ViajesRestantes.Value;
        if (dto.FechaVencimiento.HasValue) autorizacion.FechaVencimiento = dto.FechaVencimiento.Value;

        _unitOfWork.Autorizaciones.Update(autorizacion);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Autorización actualizada exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var autorizacion = await _unitOfWork.Autorizaciones.GetByIdAsync(id);
        if (autorizacion == null)
            return OperationResult.Fail("Autorización no encontrada.");

        _unitOfWork.Autorizaciones.Delete(autorizacion);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Autorización eliminada exitosamente.");
    }

    public async Task<OperationResult<IReadOnlyList<AutorizacionDto>>> GetByPersonaAsync(int personaId)
    {
        var autorizaciones = await _unitOfWork.Autorizaciones.GetVigentesAsync(personaId);
        var dtos = autorizaciones.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<AutorizacionDto>>.Ok(dtos);
    }

    private static AutorizacionDto MapToDto(Autorizacion a) => new()
    {
        Id = a.Id,
        PersonaId = a.PersonaId,
        PersonaNombre = a.Persona != null ? $"{a.Persona.Nombre} {a.Persona.Apellido}" : string.Empty,
        TipoAutorizacionNombre = a.Tipo?.Nombre ?? string.Empty,
        Saldo = a.Saldo,
        ViajesRestantes = a.ViajesRestantes,
        FechaEmision = a.FechaEmision,
        FechaVencimiento = a.FechaVencimiento,
        Activo = a.Activo,
        FechaCreacion = a.FechaCreacion
    };
}
