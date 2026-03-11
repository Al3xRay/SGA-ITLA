using SGAITLA.Application.Dtos.Transporte;
using SGAITLA.Application.Interfaces;
using SGA.Domain.Base;
using SGAITLA.Domain.Entidades.Configuracion;
using SGAITLA.Domain.Entidades.Transporte;
using SGAITLA.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Application.Dtos.Operaciones;
using SGAITLA.Domain.Base;
using SGAITLA.Domain.Entidades.Operaciones;
using SGAITLA.Domain.Entidades.Personas;



namespace SGAITLA.Application.Servicios;

public class AutobusService : IAutobusService
{
    private readonly IBaseRepository<Autobus> _repository;

    public AutobusService(IBaseRepository<Autobus> repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<List<AutobusDto>>> GetAll()
    {
        try
        {
            var autobuses = await _repository.GetAllAsync();
            var dtos = autobuses.Select(a => new AutobusDto
            {
                Id = a.Id,
                Placa = a.Placa,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Capacidad = a.Capacidad,
                Estado = a.EstadoAutobus,  
                Activo = a.Activo
            }).ToList();

            return OperationResult<List<AutobusDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return OperationResult<List<AutobusDto>>.Fail($"Error al obtener autobuses: {ex.Message}");
        }
    }

    public async Task<OperationResult<AutobusDto>> GetById(int id)
    {
        try
        {
            var autobus = await _repository.GetByIdAsync(id);
            if (autobus == null)
                return OperationResult<AutobusDto>.Fail("Autobús no encontrado");

            var dto = new AutobusDto
            {
                Id = autobus.Id,
                Placa = autobus.Placa,
                Marca = autobus.Marca,
                Modelo = autobus.Modelo,
                Capacidad = autobus.Capacidad,
                Estado = autobus.EstadoAutobus,  
                Activo = autobus.Activo
            };

            return OperationResult<AutobusDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return OperationResult<AutobusDto>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Save(SaveAutobusDto dto)
    {
        try
        {
            // Validar placa única
            var existe = await _repository.FindAsync(a => a.Placa == dto.Placa);
            if (existe.Any())
                return OperationResult<int>.Fail("Ya existe un autobús con esa placa");

            var autobus = new Autobus
            {
                Placa = dto.Placa,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Capacidad = dto.Capacidad,
                EstadoAutobus = dto.Estado,  
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            await _repository.AddAsync(autobus);
            return OperationResult<int>.Ok(autobus.Id, "Autobús guardado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al guardar: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Update(UpdateAutobusDto dto)
    {
        try
        {
            var autobus = await _repository.GetByIdAsync(dto.Id);
            if (autobus == null)
                return OperationResult<int>.Fail("Autobús no encontrado");

            // Validar placa única 
            if (autobus.Placa != dto.Placa)
            {
                var existe = await _repository.FindAsync(a => a.Placa == dto.Placa && a.Id != dto.Id);
                if (existe.Any())
                    return OperationResult<int>.Fail("Ya existe otro autobús con esa placa");
            }

            autobus.Placa = dto.Placa;
            autobus.Marca = dto.Marca;
            autobus.Modelo = dto.Modelo;
            autobus.Capacidad = dto.Capacidad;
            autobus.EstadoAutobus = dto.Estado;  
            autobus.Activo = dto.Activo;
            autobus.FechaModificacion = DateTime.Now;

            await _repository.UpdateAsync(autobus);
            return OperationResult<int>.Ok(autobus.Id, "Autobús actualizado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al actualizar: {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> Remove(RemoveAutobusDto dto)
    {
        try
        {
            var autobus = await _repository.GetByIdAsync(dto.Id);
            if (autobus == null)
                return OperationResult<bool>.Fail("Autobús no encontrado");

            // Borrado 
            autobus.Activo = false;
            autobus.FechaModificacion = DateTime.Now;  
            await _repository.UpdateAsync(autobus);

            return OperationResult<bool>.Ok(true, "Autobús eliminado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error al eliminar: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<AutobusDto>>> GetDisponibles()
    {
        try
        {
            var autobuses = await _repository.FindAsync(a =>
                a.EstadoAutobus == EstadoAutobus.Disponible && a.Activo);  
            //  Comparación con enum

            var dtos = autobuses.Select(a => new AutobusDto
            {
                Id = a.Id,
                Placa = a.Placa,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Capacidad = a.Capacidad,
                Estado = a.EstadoAutobus,  
                Activo = a.Activo
            }).ToList();

            return OperationResult<List<AutobusDto>>.Ok(dtos, $"{dtos.Count} autobuses disponibles");
        }
        catch (Exception ex)
        {
            return OperationResult<List<AutobusDto>>.Fail($"Error: {ex.Message}");
        }
    }
}
