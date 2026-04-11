using SGA.Application.Dtos.Operaciones;
using SGA.Application.Dtos.Transporte;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Entidades.Transporte;
using SGA.Domain.Repository;
using SGA.Domain.Entidades.Personas;

namespace SGA.Application.Servicios;

public class AuditoriaService : IAuditoriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuditoriaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<AuditoriaGeneralDto>> GetEstadisticasGeneralesAsync()
    {
        var viajes = await _unitOfWork.Viajes.GetAllAsync();
        var incidencias = await _unitOfWork.Incidencias.GetAllAsync();
        var pasajerosTotales = await _unitOfWork.RegistrosUso.GetAllAsync();
        var recaudado = await _unitOfWork.TransaccionesFinanciera.GetIngresosTotalesAsync();
        var autobuses = await _unitOfWork.Autobuses.GetAllAsync();
        var rutas = await _unitOfWork.Rutas.GetAllAsync();

        var dtos = new AuditoriaGeneralDto
        {
            ViajesRealizados = viajes.Count,
            IncidentesReportados = incidencias.Count,
            PasajerosTotales = pasajerosTotales.Count(x => x.AccesoPermitido),
            RecaudadoPorTicketsSueltos = recaudado,
            
            AutobusesActivos = autobuses.Count(x => x.Activo),
            RutasTotales = rutas.Count(x => x.Activo),
            ViajesProgramados = viajes.Count(x => x.EstadoViajeId == 1 && x.Activo),
            IncidenciasPendientes = incidencias.Count(x => x.EstadoIncidenciaId == 1)
        };

        return OperationResult<AuditoriaGeneralDto>.Ok(dtos);
    }

    public async Task<OperationResult<IReadOnlyList<AuditoriaViajeDto>>> GetHistorialViajesCompletoAsync()
    {
        var viajes = await _unitOfWork.Viajes.GetAllAsync();
        var registros = await _unitOfWork.RegistrosUso.GetAllAsync();
        var dtos = new List<AuditoriaViajeDto>();

        foreach (var v in viajes)
        {
            var registrosDelViaje = registros.Where(r => r.ViajeId == v.Id).ToList();
            var pasajerosDto = registrosDelViaje.Select(MapRegistroToAuditoriaTicket).ToList().AsReadOnly();
            
            dtos.Add(new AuditoriaViajeDto
            {
                Viaje = MapViajeToDto(v),
                Pasajeros = pasajerosDto
            });
        }

        return OperationResult<IReadOnlyList<AuditoriaViajeDto>>.Ok(dtos);
    }

    public async Task<OperationResult<IReadOnlyList<AuditoriaTicketDto>>> GetHistorialConsumosGeneralesAsync()
    {
        var registros = await _unitOfWork.RegistrosUso.GetAllAsync();
        var dtos = registros.Select(MapRegistroToAuditoriaTicket).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<AuditoriaTicketDto>>.Ok(dtos);
    }

    private static AuditoriaTicketDto MapRegistroToAuditoriaTicket(RegistroUso r)
    {
        var matricula = string.Empty;
        if (r.Persona is Estudiante e)
            matricula = e.Matricula;
        else if (r.Persona is Empleado emp)
            matricula = emp.CodigoEmpleado;
        else
            matricula = r.Persona?.DocumentoIdentidad ?? string.Empty;

        return new AuditoriaTicketDto
        {
            RegistroId = r.Id,
            PersonaNombre = r.Persona?.Nombre + " " + r.Persona?.Apellido,
            PersonaDocumento = r.Persona?.DocumentoIdentidad ?? string.Empty,
            PersonaMatricula = matricula,
            TipoViaje = r.Autorizacion?.Tipo?.Nombre ?? "Suelto/Desconocido",
            MontoDescontado = (r.Autorizacion != null && r.Autorizacion.TipoAutorizacionId == 2) ? 25m : 0m,
            FechaUso = r.FechaHora,
            AccesoPermitido = r.AccesoPermitido,
            MotivoRechazo = r.MotivoRechazo ?? string.Empty
        };
    }

    private static ViajeDto MapViajeToDto(Viaje v) => new()
    {
        Id = v.Id,
        RutaNombre = v.Ruta?.Nombre ?? string.Empty,
        AutobusPlaca = v.Autobus?.Placa ?? string.Empty,
        ConductorNombre = v.Conductor?.Nombre + " " + v.Conductor?.Apellido,
        EstadoViajeNombre = v.Estado?.Nombre ?? string.Empty,
        FechaProgramada = v.FechaProgramada,
        HoraInicioReal = v.HoraInicioReal,
        HoraFinReal = v.HoraFinReal,
        OcupacionActual = v.OcupacionActual,
        Observaciones = v.Observaciones,
        TieneIncidencias = v.Incidencias != null && v.Incidencias.Any(i => i.EstadoIncidenciaId != 2)
    };

    public async Task<OperationResult<IReadOnlyList<AuditoriaEmisionDto>>> GetHistorialEmisionesCompletoAsync()
    {
        var autorizaciones = await _unitOfWork.Autorizaciones.GetAllAsync();
        var registrosUso = await _unitOfWork.RegistrosUso.GetAllAsync();

        var dtos = autorizaciones.Select(a => {
            var consumos = registrosUso.Where(r => r.AutorizacionId == a.Id).Select(MapRegistroToAuditoriaTicket).ToList().AsReadOnly();
            
            var matricula = string.Empty;
            if (a.Persona is Estudiante e)
                matricula = e.Matricula;
            else if (a.Persona is Empleado emp)
                matricula = emp.CodigoEmpleado;
            else
                matricula = a.Persona?.DocumentoIdentidad ?? string.Empty;

            return new AuditoriaEmisionDto
            {
                EmisionId = a.Id,
                EmisorNombre = a.EmitidoPor != null ? $"{a.EmitidoPor.Nombre} {a.EmitidoPor.Apellido}" : "Sistema/Pago Online",
                TitularNombre = a.Persona != null ? $"{a.Persona.Nombre} {a.Persona.Apellido}" : string.Empty,
                TitularDocumento = a.Persona?.DocumentoIdentidad ?? string.Empty,
                TitularMatricula = matricula,
                TipoAutorizacion = a.Tipo?.Nombre ?? string.Empty,
                MontoCobrado = a.MontoCobrado,
                FechaEmision = a.FechaEmision,
                FechaVencimiento = a.FechaVencimiento,
                Consumos = consumos
            };
        }).ToList().AsReadOnly();

        return OperationResult<IReadOnlyList<AuditoriaEmisionDto>>.Ok(dtos);
    }
}
