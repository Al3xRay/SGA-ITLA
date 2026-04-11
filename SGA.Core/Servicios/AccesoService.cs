using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

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
            var viaje = await _unitOfWork.Viajes.GetByIdAsync(dto.ViajeId);
            if (viaje == null)
            {
                await _unitOfWork.RollbackAsync();
                return OperationResult<ResultadoAccesoDto>.Fail("El viaje especificado no existe.");
            }

            if (viaje.HoraInicioReal == null || viaje.HoraFinReal != null)
            {
                await _unitOfWork.RollbackAsync();
                return OperationResult<ResultadoAccesoDto>.Fail("El viaje no está en curso.");
            }
            var persona = await _unitOfWork.Personas.GetByIdAsync(dto.PersonaId);
            if (persona == null)
            {
                await _unitOfWork.RollbackAsync();
                return OperationResult<ResultadoAccesoDto>.Fail("La persona no existe en el sistema.");
            }

            var autorizacion = await _unitOfWork.Autorizaciones.GetActivaByPersonaAsync(dto.PersonaId);
            if (autorizacion == null)
                return await DenegarAcceso(dto, "La persona no tiene una autorización activa.");

            if (autorizacion.FechaVencimiento < DateTime.UtcNow)
                return await DenegarAcceso(dto, "La autorización ha vencido.");

            bool sinViajes = autorizacion.ViajesRestantes.HasValue && autorizacion.ViajesRestantes.Value <= 0;
            // Para pases de saldo, el mínimo requerido es RD$ 25.00
            bool sinSaldo = autorizacion.TipoAutorizacionId == 2 && autorizacion.Saldo < 25m;

            if (sinViajes && autorizacion.TipoAutorizacionId != 2)
                return await DenegarAcceso(dto, "No tiene viajes disponibles en su autorización.");
            
            if (sinSaldo)
                return await DenegarAcceso(dto, "Saldo insuficiente (mínimo RD$ 25.00 requerido).");

            var autobus = await _unitOfWork.Autobuses.GetByIdAsync(viaje.AutobusId);
            if (autobus != null && viaje.OcupacionActual >= autobus.Capacidad)
                return await DenegarAcceso(dto, "El autobús ha alcanzado su capacidad máxima.");

            var registro = new RegistroUso
            {
                PersonaId = dto.PersonaId,
                ViajeId = dto.ViajeId,
                AutorizacionId = autorizacion.Id,
                FechaHora = DateTime.UtcNow,
                AccesoPermitido = true,
                TipoRegistroId = 1,
                ValidadoPorConductorId = dto.ConductorId
            };

            await _unitOfWork.RegistrosUso.AddAsync(registro);

            // Deducción de saldo y recálculo de viajes en tiempo real
            if (autorizacion.TipoAutorizacionId == 2) // Tipo Saldo/Tarjeta Recargable
            {
                autorizacion.Saldo -= 25m;
                // Los viajes restantes se calculan siempre en base al saldo disponible
                autorizacion.ViajesRestantes = (int)Math.Floor(autorizacion.Saldo / 25m);
            }
            else if (autorizacion.ViajesRestantes.HasValue)
            {
                // Para otros tipos (mensuales con límites), se mantiene la resta unitaria
                autorizacion.ViajesRestantes--;
            }

            _unitOfWork.Autorizaciones.Update(autorizacion);

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
        var registro = new RegistroUso
        {
            PersonaId = dto.PersonaId,
            ViajeId = dto.ViajeId,
            FechaHora = DateTime.UtcNow,
            AccesoPermitido = false,
            MotivoRechazo = motivo,
            TipoRegistroId = 2,
            ValidadoPorConductorId = dto.ConductorId
        };

        await _unitOfWork.RegistrosUso.AddAsync(registro);
        await _unitOfWork.CommitAsync();

        return OperationResult<ResultadoAccesoDto>.Ok(new ResultadoAccesoDto
        {
            AccesoPermitido = false,
            MotivoRechazo = motivo,
            RegistroUsoId = registro.Id
        });
    }
}
