using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

/// <summary>
/// Servicio crítico — Validación de Acceso al Transporte.
/// Ejecuta dentro de una transacción para garantizar consistencia.
/// </summary>
public class AccesoService : IAccesoService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccesoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ResultadoAccesoDto>> ValidarAccesoAsync(ValidarAccesoDto dto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // 1. Verificar que el viaje exista y esté 'En Curso'
            var viaje = await _unitOfWork.Viajes.GetByIdAsync(dto.ViajeId);
            if (viaje == null)
                return await DenegarAcceso(dto, "El viaje especificado no existe.");

            if (viaje.HoraInicioReal == null || viaje.HoraFinReal != null)
                return await DenegarAcceso(dto, "El viaje no está en curso.");

            // 2. Verificar que la persona exista
            var persona = await _unitOfWork.Personas.GetByIdAsync(dto.PersonaId);
            if (persona == null)
                return await DenegarAcceso(dto, "La persona no existe en el sistema.");

            // 3. Verificar autorización activa
            var autorizacion = await _unitOfWork.Autorizaciones.GetActivaByPersonaAsync(dto.PersonaId);
            if (autorizacion == null)
                return await DenegarAcceso(dto, "La persona no tiene una autorización activa.");

            if (autorizacion.FechaVencimiento < DateTime.UtcNow)
                return await DenegarAcceso(dto, "La autorización ha vencido.");

            // 4. Verificar saldo/viajes disponibles
            if (autorizacion.ViajesRestantes.HasValue && autorizacion.ViajesRestantes.Value <= 0)
                return await DenegarAcceso(dto, "No tiene viajes disponibles en su autorización.");

            if (autorizacion.Saldo <= 0)
                return await DenegarAcceso(dto, "Saldo insuficiente en la autorización.");

            // 5. Verificar capacidad del autobús
            var autobus = await _unitOfWork.Autobuses.GetByIdAsync(viaje.AutobusId);
            if (autobus != null && viaje.OcupacionActual >= autobus.Capacidad)
                return await DenegarAcceso(dto, "El autobús ha alcanzado su capacidad máxima.");

            // 6. Todas las validaciones pasaron — registrar acceso permitido
            var registro = new RegistroUso
            {
                PersonaId = dto.PersonaId,
                ViajeId = dto.ViajeId,
                AutorizacionId = autorizacion.Id,
                FechaHora = DateTime.UtcNow,
                AccesoPermitido = true,
                TipoRegistroId = 1, // Abordaje
                ValidadoPorConductorId = dto.ConductorId
            };

            await _unitOfWork.RegistrosUso.AddAsync(registro);

            // Debitar autorización
            if (autorizacion.ViajesRestantes.HasValue)
                autorizacion.ViajesRestantes--;

            _unitOfWork.Autorizaciones.Update(autorizacion);

            // Incrementar ocupación
            viaje.OcupacionActual++;
            _unitOfWork.Viajes.Update(viaje);

            await _unitOfWork.CommitAsync();

            return OperationResult<ResultadoAccesoDto>.Ok(new ResultadoAccesoDto
            {
                AccesoPermitido = true,
                PersonaNombre = $"{persona.Nombre} {persona.Apellido}",
                RegistroUsoId = registro.Id
            });
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private async Task<OperationResult<ResultadoAccesoDto>> DenegarAcceso(ValidarAccesoDto dto, string motivo)
    {
        // Registrar intento rechazado
        var registro = new RegistroUso
        {
            PersonaId = dto.PersonaId,
            ViajeId = dto.ViajeId,
            FechaHora = DateTime.UtcNow,
            AccesoPermitido = false,
            MotivoRechazo = motivo,
            TipoRegistroId = 1,
            ValidadoPorConductorId = dto.ConductorId
        };

        await _unitOfWork.RegistrosUso.AddAsync(registro);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<ResultadoAccesoDto>.Ok(new ResultadoAccesoDto
        {
            AccesoPermitido = false,
            MotivoRechazo = motivo,
            RegistroUsoId = registro.Id
        });
    }
}
