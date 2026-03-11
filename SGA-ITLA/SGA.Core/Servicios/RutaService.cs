using SGA.Application.Dtos.Transporte;
using SGA.Domain.Base;
using SGAITLA.Application.Dtos.Transporte;
using SGAITLA.Application.Interfaces;
using SGAITLA.Domain.Entidades.Transporte;
using SGAITLA.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;


namespace SGAITLA.Application.Servicios;

public class RutaService : IRutaService
{
    private readonly IBaseRepository<Ruta> _rutaRepository;

    public RutaService(IBaseRepository<Ruta> rutaRepository)
    {
        _rutaRepository = rutaRepository;
    }

    public async Task<OperationResult<List<RutaDto>>> GetAll()
    {
        try
        {
            var rutas = await _rutaRepository.GetAllAsync();
            var dtos = rutas.Select(r => new RutaDto
            {
                Id = r.Id,
                Codigo = r.Codigo,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin,
                Activo = r.Activo
            }).ToList();

            return OperationResult<List<RutaDto>>.Ok(dtos, "Rutas obtenidas");
        }
        catch (Exception ex)
        {
            return OperationResult<List<RutaDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<RutaDto>> GetById(int id)
    {
        try
        {
            var ruta = await _rutaRepository.GetByIdAsync(id);
            if (ruta == null)
                return OperationResult<RutaDto>.Fail("Ruta no encontrada");

            var dto = new RutaDto
            {
                Id = ruta.Id,
                Codigo = ruta.Codigo,
                Nombre = ruta.Nombre,
                Descripcion = ruta.Descripcion,
                HoraInicio = ruta.HoraInicio,
                HoraFin = ruta.HoraFin,
                Activo = ruta.Activo
            };

            return OperationResult<RutaDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return OperationResult<RutaDto>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Save(SaveRutaDto dto)
    {
        try
        {
            var existe = await _rutaRepository.FindAsync(r => r.Codigo == dto.Codigo);
            if (existe.Any())
                return OperationResult<int>.Fail("Ya existe una ruta con ese código");

            var ruta = new Ruta
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                FechaCreacion = DateTime.UtcNow,
                Activo = true
            };

            await _rutaRepository.AddAsync(ruta);
            return OperationResult<int>.Ok(ruta.Id, "Ruta guardada correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al guardar: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Update(UpdateRutaDto dto)
    {
        try
        {
            var ruta = await _rutaRepository.GetByIdAsync(dto.Id);
            if (ruta == null)
                return OperationResult<int>.Fail("Ruta no encontrada");

            if (ruta.Codigo != dto.Codigo)
            {
                var existe = await _rutaRepository.FindAsync(r => r.Codigo == dto.Codigo && r.Id != dto.Id);
                if (existe.Any())
                    return OperationResult<int>.Fail("Ya existe otra ruta con ese código");
            }

            ruta.Codigo = dto.Codigo;
            ruta.Nombre = dto.Nombre;
            ruta.Descripcion = dto.Descripcion;
            ruta.HoraInicio = dto.HoraInicio;
            ruta.HoraFin = dto.HoraFin;
            ruta.Activo = dto.Activo;
            ruta.FechaModificacion = DateTime.UtcNow;

            await _rutaRepository.UpdateAsync(ruta);
            return OperationResult<int>.Ok(ruta.Id, "Ruta actualizada correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al actualizar: {ex.Message}");
        }
    }

    // ← ESTE MÉTODO FALTABA
    public async Task<OperationResult<bool>> Remove(RemoveRutaDto dto)
    {
        try
        {
            var ruta = await _rutaRepository.GetByIdAsync(dto.Id);
            if (ruta == null)
                return OperationResult<bool>.Fail("Ruta no encontrada");

            ruta.Activo = false;
            ruta.FechaModificacion = DateTime.UtcNow;
            await _rutaRepository.UpdateAsync(ruta);

            return OperationResult<bool>.Ok(true, "Ruta eliminada correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error al eliminar: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<RutaDto>>> GetActivas()
    {
        try
        {
            var rutas = await _rutaRepository.FindAsync(r => r.Activo);
            var dtos = rutas.Select(r => new RutaDto
            {
                Id = r.Id,
                Codigo = r.Codigo,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin,
                Activo = r.Activo
            }).ToList();

            return OperationResult<List<RutaDto>>.Ok(dtos, $"{dtos.Count} rutas activas");
        }
        catch (Exception ex)
        {
            return OperationResult<List<RutaDto>>.Fail($"Error: {ex.Message}");
        }
    }
}
