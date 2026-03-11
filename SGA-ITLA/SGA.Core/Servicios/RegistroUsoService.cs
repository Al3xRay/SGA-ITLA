using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Entidades.Transporte;
using SGAITLA.Application.Dtos.Operaciones;
using SGAITLA.Application.Interfaces;
using SGAITLA.Domain.Entidades.Personas;
using SGAITLA.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;
using SGAITLA.Domain.Entidades.Operaciones;


namespace SGAITLA.Application.Servicios;

public class RegistroUsoService : IRegistroUsoService
{
    private readonly IBaseRepository<RegistroUso> _registroRepository;
    private readonly IBaseRepository<Persona> _personaRepository;
    private readonly IBaseRepository<Viaje> _viajeRepository;

    public RegistroUsoService(
        IBaseRepository<RegistroUso> registroRepository,
        IBaseRepository<Persona> personaRepository,
        IBaseRepository<Viaje> viajeRepository)
    {
        _registroRepository = registroRepository;
        _personaRepository = personaRepository;
        _viajeRepository = viajeRepository;
    }

    public async Task<OperationResult<List<RegistroUsoDto>>> GetAll()
    {
        try
        {
            var registros = await _registroRepository.GetAllAsync();
            var dtos = registros.Select(r => new RegistroUsoDto
            {
                Id = r.Id,
                PersonaId = r.PersonaId,
                PersonaNombre = $"{r.Persona?.Nombre} {r.Persona?.Apellido}",
                ViajeId = r.ViajeId,
                AutorizacionId = r.AutorizacionId,
                FechaHora = r.FechaHora,
                AccesoPermitido = r.AccesoPermitido,
                TipoRegistroId = r.TipoRegistroId,
                TipoRegistroNombre = r.TipoRegistro?.Nombre ?? "Desconocido"
            }).ToList();

            return OperationResult<List<RegistroUsoDto>>.Ok(dtos, "Registros obtenidos");
        }
        catch (Exception ex)
        {
            return OperationResult<List<RegistroUsoDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<RegistroUsoDto>> GetById(int id)
    {
        try
        {
            var registro = await _registroRepository.GetByIdAsync(id);
            if (registro == null)
                return OperationResult<RegistroUsoDto>.Fail("Registro no encontrado");

            var dto = new RegistroUsoDto
            {
                Id = registro.Id,
                PersonaId = registro.PersonaId,
                PersonaNombre = $"{registro.Persona?.Nombre} {registro.Persona?.Apellido}",
                ViajeId = registro.ViajeId,
                AutorizacionId = registro.AutorizacionId,
                FechaHora = registro.FechaHora,
                AccesoPermitido = registro.AccesoPermitido,
                TipoRegistroId = registro.TipoRegistroId,
                TipoRegistroNombre = registro.TipoRegistro?.Nombre ?? "Desconocido"
            };

            return OperationResult<RegistroUsoDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return OperationResult<RegistroUsoDto>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<RegistroUsoDto>>> GetByPersonaId(int personaId)
    {
        try
        {
            var registros = await _registroRepository.FindAsync(r => r.PersonaId == personaId);
            var dtos = registros.Select(r => new RegistroUsoDto
            {
                Id = r.Id,
                PersonaId = r.PersonaId,
                ViajeId = r.ViajeId,
                AutorizacionId = r.AutorizacionId,
                FechaHora = r.FechaHora,
                AccesoPermitido = r.AccesoPermitido,
                TipoRegistroId = r.TipoRegistroId
            }).ToList();

            return OperationResult<List<RegistroUsoDto>>.Ok(dtos, $"{dtos.Count} registros encontrados");
        }
        catch (Exception ex)
        {
            return OperationResult<List<RegistroUsoDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<RegistroUsoDto>>> GetByViajeId(int viajeId)
    {
        try
        {
            var registros = await _registroRepository.FindAsync(r => r.ViajeId == viajeId);
            var dtos = registros.Select(r => new RegistroUsoDto
            {
                Id = r.Id,
                PersonaId = r.PersonaId,
                ViajeId = r.ViajeId,
                AutorizacionId = r.AutorizacionId,
                FechaHora = r.FechaHora,
                AccesoPermitido = r.AccesoPermitido,
                TipoRegistroId = r.TipoRegistroId
            }).ToList();

            return OperationResult<List<RegistroUsoDto>>.Ok(dtos, $"{dtos.Count} registros encontrados");
        }
        catch (Exception ex)
        {
            return OperationResult<List<RegistroUsoDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<RegistroUsoDto>>> GetByFecha(DateTime fecha)
    {
        try
        {
            var registros = await _registroRepository.FindAsync(r =>
                r.FechaHora.Date == fecha.Date);
            var dtos = registros.Select(r => new RegistroUsoDto
            {
                Id = r.Id,
                PersonaId = r.PersonaId,
                ViajeId = r.ViajeId,
                AutorizacionId = r.AutorizacionId,
                FechaHora = r.FechaHora,
                AccesoPermitido = r.AccesoPermitido,
                TipoRegistroId = r.TipoRegistroId
            }).ToList();

            return OperationResult<List<RegistroUsoDto>>.Ok(dtos, $"{dtos.Count} registros encontrados");
        }
        catch (Exception ex)
        {
            return OperationResult<List<RegistroUsoDto>>.Fail($"Error: {ex.Message}");
        }
    }
}
