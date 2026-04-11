using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class RutaService : IRutaService
{
    private readonly IUnitOfWork _unitOfWork;

    public RutaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<RutaDto>>> GetAllAsync()
    {
        var rutas = await _unitOfWork.Rutas.GetAllAsync();
        var dtos = rutas.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<RutaDto>>.Ok(dtos);
    }

    public async Task<OperationResult<RutaDto>> GetByIdAsync(int id)
    {
        var ruta = await _unitOfWork.Rutas.GetByIdAsync(id);
        if (ruta == null)
            return OperationResult<RutaDto>.Fail("Ruta no encontrada.");

        return OperationResult<RutaDto>.Ok(MapToDto(ruta));
    }

    public async Task<OperationResult> SaveAsync(SaveRutaDto dto)
    {
        var ruta = new Ruta
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Origen = dto.Origen,
            Destino = dto.Destino,
            DuracionEstimada = dto.DuracionEstimada,
            Paradas = dto.Paradas?.Select(p => new Parada
            {
                Nombre = p.Nombre,
                Ubicacion = p.Ubicacion ?? string.Empty,
                Orden = p.Orden,
                TiempoDesdeOrigen = p.TiempoDesdeOrigen
            }).ToList() ?? new List<Parada>()
        };

        await _unitOfWork.Rutas.AddAsync(ruta);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Ruta creada exitosamente.");
    }

    public async Task<OperationResult> UpdateAsync(int id, UpdateRutaDto dto)
    {
        var ruta = await _unitOfWork.Rutas.GetByIdAsync(id);
        if (ruta == null)
            return OperationResult.Fail("Ruta no encontrada.");

        ruta.Nombre = dto.Nombre;
        ruta.Descripcion = dto.Descripcion;
        ruta.Origen = dto.Origen;
        ruta.Destino = dto.Destino;
        ruta.DuracionEstimada = dto.DuracionEstimada;

        _unitOfWork.Rutas.Update(ruta);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Ruta actualizada exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var ruta = await _unitOfWork.Rutas.GetByIdAsync(id);
        if (ruta == null)
            return OperationResult.Fail("Ruta no encontrada.");

        _unitOfWork.Rutas.Delete(ruta);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Ruta eliminada exitosamente.");
    }

    private static RutaDto MapToDto(Ruta r) => new()
    {
        Id = r.Id,
        Nombre = r.Nombre,
        Descripcion = r.Descripcion,
        Origen = r.Origen,
        Destino = r.Destino,
        DuracionEstimada = r.DuracionEstimada,
        Activo = r.Activo,
        FechaCreacion = r.FechaCreacion,
        Paradas = r.Paradas?.Select(p => new ParadaDto
        {
            Id = p.Id,
            RutaId = p.RutaId,
            Nombre = p.Nombre,
            Ubicacion = p.Ubicacion,
            Orden = p.Orden,
            TiempoDesdeOrigen = p.TiempoDesdeOrigen
        }).ToList() ?? new()
    };
}
