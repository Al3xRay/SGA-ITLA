using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class IncidenciaService : IIncidenciaService
{
    private readonly IUnitOfWork _unitOfWork;

    public IncidenciaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<IncidenciaDto>>> GetAllAsync()
    {
        var incidencias = await _unitOfWork.Incidencias.GetAllAsync();
        var dtos = incidencias.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<IncidenciaDto>>.Ok(dtos);
    }

    public async Task<OperationResult<IncidenciaDto>> GetByIdAsync(int id)
    {
        var incidencia = await _unitOfWork.Incidencias.GetByIdAsync(id);
        if (incidencia == null)
            return OperationResult<IncidenciaDto>.Fail("Incidencia no encontrada.");

        return OperationResult<IncidenciaDto>.Ok(MapToDto(incidencia));
    }

    public async Task<OperationResult> SaveAsync(SaveIncidenciaDto dto)
    {
        var incidencia = new Incidencia
        {
            ViajeId = dto.ViajeId,
            TipoIncidenciaId = dto.TipoIncidenciaId,
            Descripcion = dto.Descripcion,
            FechaReporte = DateTime.UtcNow,
            EsGrave = dto.EsGrave,
            EvidenciaUrl = dto.EvidenciaUrl,
            EstadoIncidenciaId = 1, // Reportada
            ReportadoPorConductorId = dto.ReportadoPorConductorId
        };

        await _unitOfWork.Incidencias.AddAsync(incidencia);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Incidencia reportada exitosamente.");
    }

    public async Task<OperationResult<IReadOnlyList<IncidenciaDto>>> GetByViajeAsync(int viajeId)
    {
        var incidencias = await _unitOfWork.Incidencias.GetByViajeAsync(viajeId);
        var dtos = incidencias.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<IncidenciaDto>>.Ok(dtos);
    }

    public async Task<OperationResult> ResolverAsync(int id, string resolucion)
    {
        var incidencia = await _unitOfWork.Incidencias.GetByIdAsync(id);
        if (incidencia == null)
            return OperationResult.Fail("Incidencia no encontrada.");

        incidencia.Resolucion = resolucion;
        incidencia.FechaResolucion = DateTime.UtcNow;
        incidencia.EstadoIncidenciaId = 2; // Resuelta

        _unitOfWork.Incidencias.Update(incidencia);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Incidencia resuelta exitosamente.");
    }

    private static IncidenciaDto MapToDto(Incidencia i) => new()
    {
        Id = i.Id,
        ViajeId = i.ViajeId,
        TipoIncidenciaNombre = i.Tipo?.Nombre ?? string.Empty,
        Descripcion = i.Descripcion,
        FechaReporte = i.FechaReporte,
        EsGrave = i.EsGrave,
        EvidenciaUrl = i.EvidenciaUrl,
        EstadoIncidenciaNombre = i.Estado?.Nombre ?? string.Empty,
        Resolucion = i.Resolucion,
        FechaResolucion = i.FechaResolucion,
        ReportadoPorNombre = i.ReportadoPor != null ? $"{i.ReportadoPor.Nombre} {i.ReportadoPor.Apellido}" : string.Empty
    };
}
