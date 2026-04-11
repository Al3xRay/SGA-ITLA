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

        Persona persona = dto.TipoPersonaId switch
        {
            2 => new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                DocumentoIdentidad = dto.DocumentoIdentidad,
                TipoPersonaId = dto.TipoPersonaId,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                FechaNacimiento = dto.FechaNacimiento,
                Contrasena = dto.Contrasena,
                CodigoEmpleado = dto.DocumentoIdentidad,
                Departamento = string.Empty,
                Cargo = string.Empty,
                FechaContratacion = DateTime.UtcNow
            },
            _ => new Estudiante
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                DocumentoIdentidad = dto.DocumentoIdentidad,
                TipoPersonaId = dto.TipoPersonaId,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                FechaNacimiento = dto.FechaNacimiento,
                Contrasena = dto.Contrasena,
                Matricula = dto.DocumentoIdentidad,
                Carrera = string.Empty,
                FechaIngreso = DateTime.UtcNow
            }
        };

        if (persona is Empleado emp) emp.Contrasena = dto.Contrasena;

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
        
        if (!string.IsNullOrWhiteSpace(dto.Contrasena))
        {
            if (persona is Empleado emp) emp.Contrasena = dto.Contrasena;
            else if (persona is Estudiante est) est.Contrasena = dto.Contrasena;
        }

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

    public async Task<OperationResult<IReadOnlyList<PersonaDto>>> BuscarAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return OperationResult<IReadOnlyList<PersonaDto>>.Ok(new List<PersonaDto>());

        var queryUpper = query.ToUpper();
        var personas = await _unitOfWork.Personas.GetAllAsync();
        
        var filtradas = personas.Where(p => 
            (p.Nombre != null && p.Nombre.ToUpper().Contains(queryUpper)) || 
            (p.Apellido != null && p.Apellido.ToUpper().Contains(queryUpper)) || 
            (p.DocumentoIdentidad != null && p.DocumentoIdentidad.ToUpper().Contains(queryUpper))
        );

        var dtos = filtradas.Select(MapToDto).ToList().AsReadOnly();
        return OperationResult<IReadOnlyList<PersonaDto>>.Ok(dtos);
    }

    public async Task<bool> ValidarCredencialesAsync(string documento, string contrasena)
    {
        var persona = await _unitOfWork.Personas.GetByDocumentoAsync(documento);
        if (persona == null) return false;
        return persona.Contrasena == contrasena;
    }

    public async Task<PersonaDto?> GetByDocumentoAsync(string documento)
    {
        var persona = await _unitOfWork.Personas.GetByDocumentoAsync(documento);
        return persona == null ? null : MapToDto(persona);
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
        TipoPersonaId = p.TipoPersonaId,
        TipoPersonaNombre = p.Tipo?.Nombre ?? string.Empty,
        Activo = p.Activo,
        FechaCreacion = p.FechaCreacion
    };
}
