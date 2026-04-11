using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class RegistroUsoService : IRegistroUsoService
{
    private readonly IUnitOfWork _unitOfWork;

    public RegistroUsoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<RegistroUsoDto>>> GetAllAsync()
    {
        var registros = await _unitOfWork.RegistrosUso.GetAllAsync();
        var dtos = registros.Select(r => new RegistroUsoDto
        {
            Id = r.Id,
            PersonaId = r.PersonaId,
            PersonaNombre = r.Persona != null ? $"{r.Persona.Nombre} {r.Persona.Apellido}" : string.Empty,
            ViajeId = r.ViajeId,
            FechaHora = r.FechaHora,
            AccesoPermitido = r.AccesoPermitido,
            MotivoRechazo = r.MotivoRechazo,
            TipoRegistroNombre = r.Tipo?.Nombre ?? string.Empty
        }).ToList().AsReadOnly();

        return OperationResult<IReadOnlyList<RegistroUsoDto>>.Ok(dtos);
    }

    public async Task<OperationResult<IReadOnlyList<RegistroUsoDto>>> GetByViajeAsync(int viajeId)
    {
        var registros = await _unitOfWork.RegistrosUso.GetByViajeAsync(viajeId);
        var dtos = registros.Select(r => new RegistroUsoDto
        {
            Id = r.Id,
            PersonaId = r.PersonaId,
            PersonaNombre = r.Persona != null ? $"{r.Persona.Nombre} {r.Persona.Apellido}" : string.Empty,
            ViajeId = r.ViajeId,
            FechaHora = r.FechaHora,
            AccesoPermitido = r.AccesoPermitido,
            MotivoRechazo = r.MotivoRechazo,
            TipoRegistroNombre = r.Tipo?.Nombre ?? string.Empty
        }).ToList().AsReadOnly();

        return OperationResult<IReadOnlyList<RegistroUsoDto>>.Ok(dtos);
    }

    public async Task<OperationResult<IReadOnlyList<RegistroUsoDto>>> GetByPersonaAsync(int personaId)
    {
        var registros = await _unitOfWork.RegistrosUso.GetByPersonaAsync(personaId);
        var dtos = registros.Select(r => new RegistroUsoDto
        {
            Id = r.Id,
            PersonaId = r.PersonaId,
            PersonaNombre = r.Persona != null ? $"{r.Persona.Nombre} {r.Persona.Apellido}" : string.Empty,
            ViajeId = r.ViajeId,
            FechaHora = r.FechaHora,
            AccesoPermitido = r.AccesoPermitido,
            MotivoRechazo = r.MotivoRechazo,
            TipoRegistroNombre = r.Tipo?.Nombre ?? string.Empty
        }).ToList().AsReadOnly();

        return OperationResult<IReadOnlyList<RegistroUsoDto>>.Ok(dtos);
    }
}
