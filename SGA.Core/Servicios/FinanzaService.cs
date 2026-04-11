using SGA.Application.Dtos.Operaciones;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Operaciones;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class FinanzaService : IFinanzaService
{
    private readonly IUnitOfWork _unitOfWork;

    public FinanzaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<TransaccionFinancieraDto>>> GetAllAsync()
    {
        var transacciones = await _unitOfWork.TransaccionesFinanciera.GetAllAsync();
        var dtos = transacciones.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<TransaccionFinancieraDto>>.Ok(dtos);
    }

    public async Task<OperationResult<decimal>> GetIngresosTotalesAsync()
    {
        var total = await _unitOfWork.TransaccionesFinanciera.GetIngresosTotalesAsync();
        return OperationResult<decimal>.Ok(total);
    }

    public async Task<OperationResult> SaveAsync(SaveTransaccionFinancieraDto dto)
    {
        if (dto.Monto <= 0)
            return OperationResult.Fail("El monto debe ser mayor a cero.");

        var tr = new TransaccionFinanciera
        {
            Concepto = dto.Concepto,
            Monto = dto.Monto,
            Tipo = dto.Tipo,
            Fecha = dto.Fecha,
            MetodoPago = dto.MetodoPago,
            Referencia = dto.Referencia,
            ProcesadoPorId = dto.ProcesadoPorId
        };

        await _unitOfWork.TransaccionesFinanciera.AddAsync(tr);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult.Ok("Transacción registrada exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var tr = await _unitOfWork.TransaccionesFinanciera.GetByIdAsync(id);
        if (tr == null)
            return OperationResult.Fail("Transacción no encontrada.");

        _unitOfWork.TransaccionesFinanciera.Delete(tr);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Transacción anulada.");
    }

    private static TransaccionFinancieraDto MapToDto(TransaccionFinanciera t) => new()
    {
        Id = t.Id,
        Concepto = t.Concepto,
        Monto = t.Monto,
        Tipo = t.Tipo,
        Fecha = t.Fecha,
        MetodoPago = t.MetodoPago,
        Referencia = t.Referencia,
        ProcesadoPorNombre = t.ProcesadoPor?.Nombre,
        FechaCreacion = t.FechaCreacion
    };
}
