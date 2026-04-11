using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class AutobusService : IAutobusService
{
    private readonly IUnitOfWork _unitOfWork;

    public AutobusService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<AutobusDto>>> GetAllAsync()
    {
        var autobuses = await _unitOfWork.Autobuses.GetAllAsync();
        var dtos = autobuses.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<AutobusDto>>.Ok(dtos);
    }

    public async Task<OperationResult<AutobusDto>> GetByIdAsync(int id)
    {
        var autobus = await _unitOfWork.Autobuses.GetByIdAsync(id);
        if (autobus == null)
            return OperationResult<AutobusDto>.Fail("Autobús no encontrado.");

        return OperationResult<AutobusDto>.Ok(MapToDto(autobus));
    }

    public async Task<OperationResult> SaveAsync(SaveAutobusDto dto)
    {
        var existente = await _unitOfWork.Autobuses.GetByPlacaAsync(dto.Placa);
        if (existente != null)
            return OperationResult.Fail("Ya existe un autobús con esa placa.");

        if (dto.Capacidad <= 0)
            return OperationResult.Fail("La capacidad debe ser mayor a 0.");

        var autobus = new Autobus
        {
            Placa = dto.Placa,
            Marca = dto.Marca,
            Modelo = dto.Modelo,
            Capacidad = dto.Capacidad,
            EstadoAutobusId = 1
        };

        await _unitOfWork.Autobuses.AddAsync(autobus);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Autobús creado exitosamente.");
    }

    public async Task<OperationResult> UpdateAsync(int id, UpdateAutobusDto dto)
    {
        var autobus = await _unitOfWork.Autobuses.GetByIdAsync(id);
        if (autobus == null)
            return OperationResult.Fail("Autobús no encontrado.");

        if (dto.Capacidad <= 0)
            return OperationResult.Fail("La capacidad debe ser mayor a 0.");

        autobus.Marca = dto.Marca;
        autobus.Modelo = dto.Modelo;
        autobus.Capacidad = dto.Capacidad;
        autobus.EstadoAutobusId = dto.EstadoAutobusId;

        _unitOfWork.Autobuses.Update(autobus);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Autobús actualizado exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var autobus = await _unitOfWork.Autobuses.GetByIdAsync(id);
        if (autobus == null)
            return OperationResult.Fail("Autobús no encontrado.");

        _unitOfWork.Autobuses.Delete(autobus);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Autobús eliminado exitosamente.");
    }

    public async Task<OperationResult<IReadOnlyList<AutobusDto>>> GetDisponiblesAsync()
    {
        var autobuses = await _unitOfWork.Autobuses.GetDisponiblesAsync(1);
        var dtos = autobuses.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<AutobusDto>>.Ok(dtos);
    }

    private static AutobusDto MapToDto(Autobus a) => new()
    {
        Id = a.Id,
        Placa = a.Placa,
        Marca = a.Marca,
        Modelo = a.Modelo,
        Capacidad = a.Capacidad,
        EstadoAutobusNombre = a.Estado?.Nombre ?? string.Empty,
        Activo = a.Activo,
        FechaCreacion = a.FechaCreacion
    };
}
