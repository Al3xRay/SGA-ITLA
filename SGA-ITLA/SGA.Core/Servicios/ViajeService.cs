using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Configuracion;
using SGA.Domain.Entidades.Transporte;
using SGAITLA.Application.Dtos.Transporte;
using SGAITLA.Application.Interfaces;
using SGAITLA.Domain.Entidades.Personas;
using SGAITLA.Domain.Entidades.Transporte;
using SGAITLA.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;


namespace SGAITLA.Application.Servicios;

public class ViajeService : IViajeService
{
    private readonly IBaseRepository<Viaje> _viajeRepository;
    private readonly IBaseRepository<Ruta> _rutaRepository;
    private readonly IBaseRepository<Autobus> _autobusRepository;
    private readonly IBaseRepository<Conductor> _conductorRepository;

    public ViajeService(
        IBaseRepository<Viaje> viajeRepository,
        IBaseRepository<Ruta> rutaRepository,
        IBaseRepository<Autobus> autobusRepository,
        IBaseRepository<Conductor> conductorRepository)
    {
        _viajeRepository = viajeRepository;
        _rutaRepository = rutaRepository;
        _autobusRepository = autobusRepository;
        _conductorRepository = conductorRepository;
    }

    public async Task<OperationResult<List<ViajeDto>>> GetAll()
    {
        try
        {
            var viajes = await _viajeRepository.GetAllAsync();
            var dtos = viajes.Select(v => new ViajeDto
            {
                Id = v.Id,
                RutaId = v.RutaId,
                RutaNombre = v.Ruta?.Nombre ?? "Desconocida",
                AutobusId = v.AutobusId,
                AutobusPlaca = v.Autobus?.Placa ?? "Desconocida",
                ConductorId = v.ConductorId,
                ConductorNombre = $"{v.Conductor?.Nombre} {v.Conductor?.Apellido}",
                FechaSalida = v.FechaSalida,
                FechaLlegadaEstimada = v.FechaLlegadaEstimada,
                FechaLlegadaReal = v.FechaLlegadaReal,
                EstadoViajeId = v.EstadoViajeId,
                EstadoViajeNombre = v.Estado?.Nombre ?? "Desconocido", 
                OcupacionActual = v.OcupacionActual,
                CapacidadAutobus = v.Autobus?.Capacidad ?? 0,
                Activo = v.Activo
            }).ToList();

            return OperationResult<List<ViajeDto>>.Ok(dtos, "Viajes obtenidos");
        }
        catch (Exception ex)
        {
            return OperationResult<List<ViajeDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<ViajeDto>> GetById(int id)
    {
        try
        {
            var viaje = await _viajeRepository.GetByIdAsync(id);
            if (viaje == null)
                return OperationResult<ViajeDto>.Fail("Viaje no encontrado");

            var dto = new ViajeDto
            {
                Id = viaje.Id,
                RutaId = viaje.RutaId,
                RutaNombre = viaje.Ruta?.Nombre ?? "Desconocida",
                AutobusId = viaje.AutobusId,
                AutobusPlaca = viaje.Autobus?.Placa ?? "Desconocida",
                ConductorId = viaje.ConductorId,
                ConductorNombre = $"{viaje.Conductor?.Nombre} {viaje.Conductor?.Apellido}",
                FechaSalida = viaje.FechaSalida,
                FechaLlegadaEstimada = viaje.FechaLlegadaEstimada,
                FechaLlegadaReal = viaje.FechaLlegadaReal,
                EstadoViajeId = viaje.EstadoViajeId,
                EstadoViajeNombre = viaje.Estado?.Nombre ?? "Desconocido",
                OcupacionActual = viaje.OcupacionActual,
                CapacidadAutobus = viaje.Autobus?.Capacidad ?? 0,
                Activo = viaje.Activo
            };

            return OperationResult<ViajeDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return OperationResult<ViajeDto>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Save(SaveViajeDto dto)
    {
        try
        {
            var ruta = await _rutaRepository.GetByIdAsync(dto.RutaId);
            if (ruta == null)
                return OperationResult<int>.Fail("Ruta no encontrada");

            var autobus = await _autobusRepository.GetByIdAsync(dto.AutobusId);
            if (autobus == null)
                return OperationResult<int>.Fail("Autobús no encontrado");

            var conductor = await _conductorRepository.GetByIdAsync(dto.ConductorId);
            if (conductor == null)
                return OperationResult<int>.Fail("Conductor no encontrado");

            var viaje = new Viaje
            {
                RutaId = dto.RutaId,
                AutobusId = dto.AutobusId,
                ConductorId = dto.ConductorId,
                FechaSalida = dto.FechaSalida,
                FechaLlegadaEstimada = dto.FechaLlegadaEstimada,
                EstadoViajeId = 1, // Programado
                OcupacionActual = 0,
                FechaCreacion = DateTime.UtcNow,
                Activo = true
            };

            await _viajeRepository.AddAsync(viaje);
            return OperationResult<int>.Ok(viaje.Id, "Viaje guardado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al guardar: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Update(UpdateViajeDto dto)
    {
        try
        {
            var viaje = await _viajeRepository.GetByIdAsync(dto.Id);
            if (viaje == null)
                return OperationResult<int>.Fail("Viaje no encontrado");

            viaje.RutaId = dto.RutaId;
            viaje.AutobusId = dto.AutobusId;
            viaje.ConductorId = dto.ConductorId;
            viaje.EstadoViajeId = dto.EstadoViajeId;
            viaje.FechaLlegadaReal = dto.FechaLlegadaReal;
            viaje.OcupacionActual = dto.OcupacionActual;
            viaje.Activo = dto.Activo;
            viaje.FechaModificacion = DateTime.UtcNow;

            await _viajeRepository.UpdateAsync(viaje);
            return OperationResult<int>.Ok(viaje.Id, "Viaje actualizado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al actualizar: {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> Remove(RemoveViajeDto dto)
    {
        try
        {
            var viaje = await _viajeRepository.GetByIdAsync(dto.Id);
            if (viaje == null)
                return OperationResult<bool>.Fail("Viaje no encontrado");

            viaje.Activo = false;
            viaje.FechaModificacion = DateTime.UtcNow;
            await _viajeRepository.UpdateAsync(viaje);

            return OperationResult<bool>.Ok(true, "Viaje eliminado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error al eliminar: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<ViajeDto>>> GetActivos()
    {
        try
        {
            var viajes = await _viajeRepository.FindAsync(v => v.Activo);
            var dtos = viajes.Select(v => new ViajeDto
            {
                Id = v.Id,
                RutaId = v.RutaId,
                RutaNombre = v.Ruta?.Nombre ?? "Desconocida",
                AutobusId = v.AutobusId,
                AutobusPlaca = v.Autobus?.Placa ?? "Desconocida",
                ConductorId = v.ConductorId,
                ConductorNombre = $"{v.Conductor?.Nombre} {v.Conductor?.Apellido}",
                FechaSalida = v.FechaSalida,
                FechaLlegadaEstimada = v.FechaLlegadaEstimada,
                FechaLlegadaReal = v.FechaLlegadaReal,
                EstadoViajeId = v.EstadoViajeId,
                EstadoViajeNombre = v.Estado?.Nombre ?? "Desconocido",
                OcupacionActual = v.OcupacionActual,
                CapacidadAutobus = v.Autobus?.Capacidad ?? 0,
                Activo = v.Activo
            }).ToList();

            return OperationResult<List<ViajeDto>>.Ok(dtos, $"{dtos.Count} viajes activos");
        }
        catch (Exception ex)
        {
            return OperationResult<List<ViajeDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<ViajeDto>>> GetByRuta(int rutaId)
    {
        try
        {
            var viajes = await _viajeRepository.FindAsync(v => v.RutaId == rutaId);
            var dtos = viajes.Select(v => new ViajeDto
            {
                Id = v.Id,
                RutaId = v.RutaId,
                RutaNombre = v.Ruta?.Nombre ?? "Desconocida",
                AutobusId = v.AutobusId,
                AutobusPlaca = v.Autobus?.Placa ?? "Desconocida",
                ConductorId = v.ConductorId,
                ConductorNombre = $"{v.Conductor?.Nombre} {v.Conductor?.Apellido}",
                FechaSalida = v.FechaSalida,
                FechaLlegadaEstimada = v.FechaLlegadaEstimada,
                FechaLlegadaReal = v.FechaLlegadaReal,
                EstadoViajeId = v.EstadoViajeId,
                EstadoViajeNombre = v.Estado?.Nombre ?? "Desconocido", 
                OcupacionActual = v.OcupacionActual,
                CapacidadAutobus = v.Autobus?.Capacidad ?? 0,
                Activo = v.Activo
            }).ToList();

            return OperationResult<List<ViajeDto>>.Ok(dtos, $"{dtos.Count} viajes encontrados");
        }
        catch (Exception ex)
        {
            return OperationResult<List<ViajeDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<ViajeDto>>> GetByConductor(int conductorId)
    {
        try
        {
            var viajes = await _viajeRepository.FindAsync(v => v.ConductorId == conductorId);
            var dtos = viajes.Select(v => new ViajeDto
            {
                Id = v.Id,
                RutaId = v.RutaId,
                RutaNombre = v.Ruta?.Nombre ?? "Desconocida",
                AutobusId = v.AutobusId,
                AutobusPlaca = v.Autobus?.Placa ?? "Desconocida",
                ConductorId = v.ConductorId,
                ConductorNombre = $"{v.Conductor?.Nombre} {v.Conductor?.Apellido}",
                FechaSalida = v.FechaSalida,
                FechaLlegadaEstimada = v.FechaLlegadaEstimada,
                FechaLlegadaReal = v.FechaLlegadaReal,
                EstadoViajeId = v.EstadoViajeId,
                EstadoViajeNombre = v.Estado?.Nombre ?? "Desconocido", 
                OcupacionActual = v.OcupacionActual,
                CapacidadAutobus = v.Autobus?.Capacidad ?? 0,
                Activo = v.Activo
            }).ToList();

            return OperationResult<List<ViajeDto>>.Ok(dtos, $"{dtos.Count} viajes encontrados");
        }
        catch (Exception ex)
        {
            return OperationResult<List<ViajeDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> IniciarViaje(int viajeId)
    {
        try
        {
            var viaje = await _viajeRepository.GetByIdAsync(viajeId);
            if (viaje == null)
                return OperationResult<bool>.Fail("Viaje no encontrado");

            viaje.EstadoViajeId = EstadoViaje.EnCurso;
            viaje.HoraInicioReal = DateTime.UtcNow;
            viaje.FechaModificacion = DateTime.UtcNow;

            await _viajeRepository.UpdateAsync(viaje);
            return OperationResult<bool>.Ok(true, "Viaje iniciado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> FinalizarViaje(int viajeId)
    {
        try
        {
            var viaje = await _viajeRepository.GetByIdAsync(viajeId);
            if (viaje == null)
                return OperationResult<bool>.Fail("Viaje no encontrado");

            viaje.EstadoViajeId = 2; // Completado
            viaje.HoraFinReal = DateTime.UtcNow;
            viaje.FechaModificacion = DateTime.UtcNow;

            await _viajeRepository.UpdateAsync(viaje);
            return OperationResult<bool>.Ok(true, "Viaje finalizado correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error: {ex.Message}");
        }
    }
}
