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
        try
        {
            var viaje = await _unitOfWork.Viajes.GetByIdAsync(dto.ViajeId);
            if (viaje == null)
                return OperationResult.Fail($"No existe un viaje con ID {dto.ViajeId}.");


            var conductor = await _unitOfWork.Conductores.GetByIdAsync(
                dto.ReportadoPorConductorId);
            if (conductor == null)
                return OperationResult.Fail($"No existe un conductor con ID {dto.ReportadoPorConductorId}.");

            var incidencia = new Incidencia
            {
                ViajeId = dto.ViajeId,
                TipoIncidenciaId = dto.TipoIncidenciaId,
                Descripcion = dto.Descripcion,
                FechaReporte = DateTime.UtcNow,
                EsGrave = dto.EsGrave,
                EvidenciaUrl = dto.EvidenciaUrl,
                EstadoIncidenciaId = 1,
                ReportadoPorConductorId = dto.ReportadoPorConductorId
            };

            await _unitOfWork.Incidencias.AddAsync(incidencia);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Ok("Incidencia reportada exitosamente.");
        }
        catch (Exception ex)
        {
            var mensaje = ex.InnerException?.Message ?? ex.Message;

            if (mensaje.Contains("FK_Incidencias_ReportadoPor"))
                return OperationResult.Fail("El conductor especificado no existe en la base de datos.");

            if (mensaje.Contains("FK_Incidencias") || mensaje.Contains("ViajeId"))
                return OperationResult.Fail("El viaje especificado no existe en la base de datos.");

            return OperationResult.Fail($"Error al guardar la incidencia: {mensaje}");
        }
    }

    public async Task<OperationResult<IReadOnlyList<IncidenciaDto>>> GetByViajeAsync(
        int viajeId)
    {
        var incidencias = await _unitOfWork.Incidencias.GetByViajeAsync(viajeId);
        var dtos = incidencias.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<IncidenciaDto>>.Ok(dtos);
    }

    public async Task<OperationResult> ResolverAsync(int id, string resolucion)
    {
        try
        {
            var incidencia = await _unitOfWork.Incidencias.GetByIdAsync(id);
            if (incidencia == null)
                return OperationResult.Fail("Incidencia no encontrada.");

            incidencia.Resolucion = resolucion;
            incidencia.FechaResolucion = DateTime.UtcNow;
            incidencia.EstadoIncidenciaId = 2;

            _unitOfWork.Incidencias.Update(incidencia);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Ok("Incidencia resuelta exitosamente.");
        }
        catch (Exception ex)
        {
            var mensaje = ex.InnerException?.Message ?? ex.Message;
            return OperationResult.Fail($"Error al resolver la incidencia: {mensaje}");
        }
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