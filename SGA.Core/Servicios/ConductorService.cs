using SGA.Application.Dtos.Personas;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Personas;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class ConductorService : IConductorService
{
    private readonly IUnitOfWork _unitOfWork;

    public ConductorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<ConductorDto>>> GetAllAsync()
    {
        var conductores = await _unitOfWork.Conductores.GetAllAsync();
        var dtos = conductores.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<ConductorDto>>.Ok(dtos);
    }

    public async Task<OperationResult<ConductorDto>> GetByIdAsync(int id)
    {
        var conductor = await _unitOfWork.Conductores.GetByIdAsync(id);
        if (conductor == null)
            return OperationResult<ConductorDto>.Fail("Conductor no encontrado.");

        return OperationResult<ConductorDto>.Ok(MapToDto(conductor));
    }

    public async Task<OperationResult> SaveAsync(SaveConductorDto dto)
    {
        if (dto.FechaVencimientoLicencia <= DateTime.UtcNow)
            return OperationResult.Fail("La licencia de conducir ya está vencida.");

        var conductor = new Conductor
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            DocumentoIdentidad = dto.DocumentoIdentidad,
            TipoPersonaId = dto.TipoPersonaId,
            Telefono = dto.Telefono,
            Direccion = dto.Direccion,
            FechaNacimiento = dto.FechaNacimiento,
            Contrasena = dto.Contrasena,
            LicenciaConducir = dto.LicenciaConducir,
            FechaVencimientoLicencia = dto.FechaVencimientoLicencia,
            CategoriaLicencia = dto.CategoriaLicencia,
            FechaContratacion = dto.FechaContratacion
        };

        await _unitOfWork.Conductores.AddAsync(conductor);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Conductor creado exitosamente.");
    }

    public async Task<OperationResult> UpdateAsync(int id, UpdateConductorDto dto)
    {
        var conductor = await _unitOfWork.Conductores.GetByIdAsync(id);
        if (conductor == null)
            return OperationResult.Fail("Conductor no encontrado.");

        conductor.Nombre = dto.Nombre;
        conductor.Apellido = dto.Apellido;
        conductor.Telefono = dto.Telefono;
        conductor.Direccion = dto.Direccion;
        conductor.DocumentoIdentidad = dto.DocumentoIdentidad;
        conductor.LicenciaConducir = dto.LicenciaConducir;
        conductor.CategoriaLicencia = dto.CategoriaLicencia;
        conductor.FechaVencimientoLicencia = dto.FechaVencimientoLicencia;
        conductor.FechaContratacion = dto.FechaContratacion;

        if (!string.IsNullOrWhiteSpace(dto.Contrasena))
        {
            conductor.Contrasena = dto.Contrasena;
        }

        _unitOfWork.Conductores.Update(conductor);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Conductor actualizado exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var conductor = await _unitOfWork.Conductores.GetByIdAsync(id);
        if (conductor == null)
            return OperationResult.Fail("Conductor no encontrado.");

        _unitOfWork.Conductores.Delete(conductor);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Conductor eliminado exitosamente.");
    }

    public async Task<OperationResult<IReadOnlyList<ConductorDto>>> GetActivosAsync()
    {
        var conductores = await _unitOfWork.Conductores.GetActivosAsync();
        var dtos = conductores.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<ConductorDto>>.Ok(dtos);
    }

    private static ConductorDto MapToDto(Conductor c) => new()
    {
        Id = c.Id,
        Nombre = c.Nombre,
        Apellido = c.Apellido,
        DocumentoIdentidad = c.DocumentoIdentidad,
        Telefono = c.Telefono,
        Direccion = c.Direccion,
        FechaNacimiento = c.FechaNacimiento,
        TipoPersonaNombre = c.Tipo?.Nombre ?? string.Empty,
        LicenciaConducir = c.LicenciaConducir,
        FechaVencimientoLicencia = c.FechaVencimientoLicencia,
        CategoriaLicencia = c.CategoriaLicencia,
        FechaContratacion = c.FechaContratacion,
        Activo = c.Activo,
        FechaCreacion = c.FechaCreacion
    };
}
