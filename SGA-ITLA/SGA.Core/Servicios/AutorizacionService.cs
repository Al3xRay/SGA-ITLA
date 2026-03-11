using SGA.Application.Dtos.Operaciones;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Entidades.Configuracion;
using SGAITLA.Application.Dtos.Operaciones;
using SGAITLA.Domain.Entidades.Operaciones;
using SGAITLA.Domain.Entidades.Personas;
using SGAITLA.Domain.Entidades.Transporte;
using SGAITLA.Domain.Repositorios;
using SGAITLA.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Domain.Base;
using SGAITLA.Domain.Entidades.Configuracion;


namespace SGAITLA.Application.Servicios;

public class AutorizacionService : IAutorizacionService
{
    private readonly IBaseRepository<Autorizacion> _autorizacionRepository;
    private readonly IBaseRepository<Persona> _personaRepository;
    private readonly IBaseRepository<Viaje> _viajeRepository;
    private readonly IBaseRepository<RegistroUso> _registroRepository;

    public AutorizacionService(
        IBaseRepository<Autorizacion> autorizacionRepository,
        IBaseRepository<Persona> personaRepository,
        IBaseRepository<Viaje> viajeRepository,
        IBaseRepository<RegistroUso> registroRepository)
    {
        _autorizacionRepository = autorizacionRepository;
        _personaRepository = personaRepository;
        _viajeRepository = viajeRepository;
        _registroRepository = registroRepository;
    }

    public async Task<OperationResult<List<AutorizacionDto>>> GetAll()
    {
        try
        {
            var autorizaciones = await _autorizacionRepository.GetAllAsync();
            var dtos = autorizaciones.Select(a => new AutorizacionDto
            {
                Id = a.Id,
                PersonaId = a.PersonaId,
                PersonaNombre = $"{a.Persona?.Nombre} {a.Persona?.Apellido}",
                TipoAutorizacion = a.Tipo?.Nombre ?? "Desconocido",
                Saldo = a.Saldo,
                ViajesRestantes = a.ViajesRestantes,
                FechaVencimiento = a.FechaVencimiento,
                Activo = a.Activo
            }).ToList();

            return OperationResult<List<AutorizacionDto>>.Ok(dtos, "Autorizaciones obtenidas");
        }
        catch (Exception ex)
        {
            return OperationResult<List<AutorizacionDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<AutorizacionDto>> GetById(int id)
    {
        try
        {
            var autorizacion = await _autorizacionRepository.GetByIdAsync(id);
            if (autorizacion == null)
                return OperationResult<AutorizacionDto>.Fail("Autorización no encontrada");

            var dto = new AutorizacionDto
            {
                Id = autorizacion.Id,
                PersonaId = autorizacion.PersonaId,
                PersonaNombre = $"{autorizacion.Persona?.Nombre} {autorizacion.Persona?.Apellido}",
                TipoAutorizacion = autorizacion.Tipo?.Nombre ?? "Desconocido",
                Saldo = autorizacion.Saldo,
                ViajesRestantes = autorizacion.ViajesRestantes,
                FechaVencimiento = autorizacion.FechaVencimiento,
                Activo = autorizacion.Activo
            };

            return OperationResult<AutorizacionDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return OperationResult<AutorizacionDto>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Save(SaveAutorizacionDto dto)
    {
        try
        {
            var persona = await _personaRepository.GetByIdAsync(dto.PersonaId);
            if (persona == null)
                return OperationResult<int>.Fail("Persona no encontrada");

            var autorizacion = new Autorizacion
            {
                PersonaId = dto.PersonaId,
                TipoAutorizacionId = dto.TipoAutorizacionId,
                Saldo = dto.TipoAutorizacionId == TipoAutorizacion.TarjetaRecargable ? dto.SaldoInicial : 0,
                ViajesRestantes = dto.TipoAutorizacionId == TipoAutorizacion.TicketMensual ? dto.ViajesIniciales : null,
                FechaEmision = DateTime.Now,
                FechaVencimiento = dto.FechaVencimiento,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            await _autorizacionRepository.AddAsync(autorizacion);
            return OperationResult<int>.Ok(autorizacion.Id, "Autorización creada correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Update(UpdateAutorizacionDto dto)
    {
        try
        {
            var autorizacion = await _autorizacionRepository.GetByIdAsync(dto.Id);
            if (autorizacion == null)
                return OperationResult<int>.Fail("Autorización no encontrada");

            autorizacion.FechaVencimiento = dto.FechaVencimiento;
            autorizacion.Activo = dto.Activo;
            autorizacion.FechaModificacion = DateTime.Now;

            await _autorizacionRepository.UpdateAsync(autorizacion);
            return OperationResult<int>.Ok(autorizacion.Id, "Autorización actualizada");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> Remove(RemoveAutorizacionDto dto)
    {
        try
        {
            var autorizacion = await _autorizacionRepository.GetByIdAsync(dto.Id);
            if (autorizacion == null)
                return OperationResult<bool>.Fail("Autorización no encontrada");

            autorizacion.Activo = false;
            autorizacion.FechaModificacion = DateTime.Now;
            await _autorizacionRepository.UpdateAsync(autorizacion);

            return OperationResult<bool>.Ok(true, "Autorización eliminada");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<AutorizacionDto>>> GetByPersonaId(int personaId)
    {
        try
        {
            var autorizaciones = await _autorizacionRepository.FindAsync(a =>
                a.PersonaId == personaId && a.Activo);

            var dtos = autorizaciones.Select(a => new AutorizacionDto
            {
                Id = a.Id,
                PersonaId = a.PersonaId,
                PersonaNombre = $"{a.Persona?.Nombre} {a.Persona?.Apellido}",
                TipoAutorizacion = a.Tipo?.Nombre ?? "Desconocido",
                Saldo = a.Saldo,
                ViajesRestantes = a.ViajesRestantes,
                FechaVencimiento = a.FechaVencimiento,
                Activo = a.Activo
            }).ToList();

            return OperationResult<List<AutorizacionDto>>.Ok(dtos, $"{dtos.Count} autorizaciones encontradas");
        }
        catch (Exception ex)
        {
            return OperationResult<List<AutorizacionDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<decimal>> Recargar(int autorizacionId, decimal monto, int changeUser)
    {
        try
        {
            var autorizacion = await _autorizacionRepository.GetByIdAsync(autorizacionId);
            if (autorizacion == null)
                return OperationResult<decimal>.Fail("Autorización no encontrada");

            if (autorizacion.TipoAutorizacionId != TipoAutorizacion.TarjetaRecargable)
                return OperationResult<decimal>.Fail("Solo tarjetas recargables pueden recargarse");

            if (monto <= 0)
                return OperationResult<decimal>.Fail("El monto debe ser mayor a cero");

            autorizacion.Saldo += monto;
            autorizacion.FechaModificacion = DateTime.Now;

            await _autorizacionRepository.UpdateAsync(autorizacion);
            return OperationResult<decimal>.Ok(autorizacion.Saldo, $"Recarga exitosa. Nuevo saldo: {autorizacion.Saldo}");
        }
        catch (Exception ex)
        {
            return OperationResult<decimal>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> ValidarAcceso(int personaId, int viajeId)
    {
        try
        {
            // Validar viaje
            var viaje = await _viajeRepository.GetByIdAsync(viajeId);
            if (viaje == null)
                return OperationResult<bool>.Fail("Viaje no encontrado");

            if (viaje.EstadoViajeId != EstadoViaje.EnCurso)
                return OperationResult<bool>.Fail("El viaje no está en curso");

            // Validar capacidad
            if (viaje.OcupacionActual >= viaje.Autobus?.Capacidad)
                return OperationResult<bool>.Fail("Autobús lleno");

            // Buscar autorización válida
            var autorizaciones = await _autorizacionRepository.FindAsync(a =>
                a.PersonaId == personaId && a.Activo && a.FechaVencimiento > DateTime.Now);

            var autorizacionValida = autorizaciones.FirstOrDefault(a => EsValida(a));

            if (autorizacionValida == null)
                return OperationResult<bool>.Fail("Sin autorización válida");

            // Verificar duplicado
            var abordajes = await _registroRepository.FindAsync(r =>
                r.PersonaId == personaId && r.ViajeId == viajeId && r.AccesoPermitido);

            if (abordajes.Any())
                return OperationResult<bool>.Fail("Ya abordó este viaje");

            // Consumir y registrar
            await ConsumirAutorizacion(autorizacionValida);

            viaje.OcupacionActual++;
            viaje.FechaModificacion = DateTime.Now;
            await _viajeRepository.UpdateAsync(viaje);

            // Registrar uso
            var registro = new RegistroUso
            {
                PersonaId = personaId,
                ViajeId = viajeId,
                AutorizacionId = autorizacionValida.Id,
                FechaHora = DateTime.Now,
                AccesoPermitido = true,
                TipoRegistroId = TipoRegistro.Abordaje
            };
            await _registroRepository.AddAsync(registro);

            return OperationResult<bool>.Ok(true, "Acceso permitido");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error: {ex.Message}");
        }
    }

    private bool EsValida(Autorizacion a)
    {
        if (!a.Activo) return false;
        if (a.FechaVencimiento <= DateTime.Now) return false;
        if (a.TipoAutorizacionId == TipoAutorizacion.TarjetaRecargable && a.Saldo <= 0) return false;
        if (a.TipoAutorizacionId == TipoAutorizacion.TicketMensual && a.ViajesRestantes.HasValue && a.ViajesRestantes <= 0) return false;
        return true;
    }

    private async Task ConsumirAutorizacion(Autorizacion a)
    {
        if (a.TipoAutorizacionId == TipoAutorizacion.TarjetaRecargable)
            a.Saldo -= 1;
        else if (a.TipoAutorizacionId == TipoAutorizacion.TicketMensual && a.ViajesRestantes.HasValue)
            a.ViajesRestantes--;

        a.FechaModificacion = DateTime.Now;
        await _autorizacionRepository.UpdateAsync(a);
    }
}