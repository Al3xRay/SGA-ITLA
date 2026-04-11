using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class ViajeService : IViajeService
{
    private readonly IUnitOfWork _unitOfWork;

    public ViajeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<ViajeDto>>> GetAllAsync()
    {
        var viajes = await _unitOfWork.Viajes.GetAllAsync();
        var dtos = viajes.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<ViajeDto>>.Ok(dtos);
    }

    public async Task<OperationResult<ViajeDto>> GetByIdAsync(int id)
    {
        var viaje = await _unitOfWork.Viajes.GetByIdAsync(id);
        if (viaje == null)
            return OperationResult<ViajeDto>.Fail("Viaje no encontrado.");

        return OperationResult<ViajeDto>.Ok(MapToDto(viaje));
    }

    public async Task<OperationResult> SaveAsync(SaveViajeDto dto)
    {
        if (!await _unitOfWork.Rutas.ExistsAsync(dto.RutaId))
            return OperationResult.Fail("La ruta especificada no existe.");

        if (!await _unitOfWork.Autobuses.ExistsAsync(dto.AutobusId))
            return OperationResult.Fail("El autobús especificado no existe.");

        if (!await _unitOfWork.Conductores.ExistsAsync(dto.ConductorId))
            return OperationResult.Fail("El conductor especificado no existe.");

        var viajesConductor = await _unitOfWork.Viajes.GetByConductorYFechaAsync(dto.ConductorId, dto.FechaProgramada);

        if (viajesConductor.Any(v => v.EstadoViajeId == 1 || v.EstadoViajeId == 2))
            return OperationResult.Fail("El conductor ya tiene un viaje activo asignado para esa fecha.");

        var viaje = new Viaje
        {
            RutaId = dto.RutaId,
            AutobusId = dto.AutobusId,
            ConductorId = dto.ConductorId,
            HorarioId = dto.HorarioId,
            FechaProgramada = dto.FechaProgramada,
            EstadoViajeId = 1,
            OcupacionActual = 0
        };

        await _unitOfWork.Viajes.AddAsync(viaje);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Viaje planificado exitosamente.");
    }

    public async Task<OperationResult> UpdateAsync(int id, UpdateViajeDto dto)
    {
        var viaje = await _unitOfWork.Viajes.GetByIdAsync(id);
        if (viaje == null)
            return OperationResult.Fail("Viaje no encontrado.");

        viaje.EstadoViajeId = dto.EstadoViajeId;
        viaje.HoraInicioReal = dto.HoraInicioReal;
        viaje.HoraFinReal = dto.HoraFinReal;
        viaje.Observaciones = dto.Observaciones;

        _unitOfWork.Viajes.Update(viaje);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Viaje actualizado exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var viaje = await _unitOfWork.Viajes.GetByIdAsync(id);
        if (viaje == null)
            return OperationResult.Fail("Viaje no encontrado.");

        _unitOfWork.Viajes.Delete(viaje);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Viaje eliminado exitosamente.");
    }

    public async Task<OperationResult> IniciarViajeAsync(int viajeId)
    {
        var viaje = await _unitOfWork.Viajes.GetByIdAsync(viajeId);
        if (viaje == null)
            return OperationResult.Fail("Viaje no encontrado.");

        if (viaje.HoraInicioReal != null)
            return OperationResult.Fail("El viaje ya fue iniciado.");

        viaje.HoraInicioReal = DateTime.UtcNow;
        viaje.EstadoViajeId = 2;

        _unitOfWork.Viajes.Update(viaje);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Viaje iniciado exitosamente.");
    }

    public async Task<OperationResult> FinalizarViajeAsync(int viajeId)
    {
        var viaje = await _unitOfWork.Viajes.GetByIdAsync(viajeId);
        if (viaje == null)
            return OperationResult.Fail("Viaje no encontrado.");

        if (viaje.HoraInicioReal == null)
            return OperationResult.Fail("El viaje no ha sido iniciado.");

        if (viaje.HoraFinReal != null)
            return OperationResult.Fail("El viaje ya fue finalizado.");

        viaje.HoraFinReal = DateTime.UtcNow;
        viaje.EstadoViajeId = 3; 

        _unitOfWork.Viajes.Update(viaje);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Viaje finalizado exitosamente.");
    }

    private static ViajeDto MapToDto(Viaje v) => new()
    {
        Id = v.Id,
        RutaId = v.RutaId,
        RutaNombre = v.Ruta?.Nombre ?? string.Empty,
        AutobusId = v.AutobusId,
        AutobusPlaca = v.Autobus?.Placa ?? string.Empty,
        ConductorId = v.ConductorId,
        ConductorNombre = v.Conductor != null ? $"{v.Conductor.Nombre} {v.Conductor.Apellido}" : string.Empty,
        FechaProgramada = v.FechaProgramada,
        HoraInicioReal = v.HoraInicioReal,
        HoraFinReal = v.HoraFinReal,
        OcupacionActual = v.OcupacionActual,
        Capacidad = v.Autobus?.Capacidad ?? 0,
        EstadoViajeNombre = v.Estado?.Nombre ?? string.Empty,
        Observaciones = v.Observaciones,
        Activo = v.Activo,
        FechaCreacion = v.FechaCreacion
    };
}
