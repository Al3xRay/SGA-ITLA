using SGA.Application.Dtos.Personas;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGA.Domain.Entidades.Personas;
using SGA.Domain.Repository;

namespace SGA.Application.Servicios;

public class PersonaService : IPersonaService
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IReadOnlyList<PersonaDto>>> GetAllAsync()
    {
        var personas = await _unitOfWork.Personas.GetAllAsync();
        var dtos = personas.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<PersonaDto>>.Ok(dtos);
    }

    public async Task<OperationResult<PersonaDto>> GetByIdAsync(int id)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null)
            return OperationResult<PersonaDto>.Fail("Persona no encontrada.");

        return OperationResult<PersonaDto>.Ok(MapToDto(persona));
    }

    public async Task<OperationResult> SaveAsync(SavePersonaDto dto)
    {
        var existente = await _unitOfWork.Personas.GetByDocumentoAsync(dto.DocumentoIdentidad);
        if (existente != null)
            return OperationResult.Fail("Ya existe una persona con ese documento de identidad.");

        var persona = new Estudiante
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            DocumentoIdentidad = dto.DocumentoIdentidad,
            TipoPersonaId = dto.TipoPersonaId,
            Telefono = dto.Telefono,
            Direccion = dto.Direccion,
            FechaNacimiento = dto.FechaNacimiento,
            Matricula = string.Empty,
            Carrera = string.Empty,
            FechaIngreso = DateTime.UtcNow
        };

        await _unitOfWork.Personas.AddAsync(persona);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Persona creada exitosamente.");
    }

    public async Task<OperationResult> UpdateAsync(int id, UpdatePersonaDto dto)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null)
            return OperationResult.Fail("Persona no encontrada.");

        persona.Nombre = dto.Nombre;
        persona.Apellido = dto.Apellido;
        persona.Telefono = dto.Telefono;
        persona.Direccion = dto.Direccion;

        _unitOfWork.Personas.Update(persona);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Persona actualizada exitosamente.");
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null)
            return OperationResult.Fail("Persona no encontrada.");

        _unitOfWork.Personas.Delete(persona);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult.Ok("Persona eliminada exitosamente.");
    }

    private static PersonaDto MapToDto(Persona p) => new()
    {
        Id = p.Id,
        Nombre = p.Nombre,
        Apellido = p.Apellido,
        DocumentoIdentidad = p.DocumentoIdentidad,
        Telefono = p.Telefono,
        Direccion = p.Direccion,
        FechaNacimiento = p.FechaNacimiento,
        TipoPersonaNombre = p.Tipo?.Nombre ?? string.Empty,
        Activo = p.Activo,
        FechaCreacion = p.FechaCreacion
    };
}
