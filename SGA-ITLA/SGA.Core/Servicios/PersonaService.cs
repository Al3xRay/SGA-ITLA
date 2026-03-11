using SGA.Application.Dtos.Personas;
using SGAITLA.Application.Dtos.Personas;
using SGA.Application.Interfaces;
using SGA.Domain.Base;
using SGAITLA.Domain.Entidades.Configuracion;
using SGAITLA.Domain.Entidades.Personas;
using SGAITLA.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGAITLA.Application.Interfaces;
using SGAITLA.Domain.Base;


namespace SGAITLA.Application.Servicios;

public class PersonaService : IPersonaService
{
    private readonly IBaseRepository<Persona> _personaRepository;
    private readonly IBaseRepository<Estudiante> _estudianteRepository;
    private readonly IBaseRepository<Conductor> _conductorRepository;
    private readonly IBaseRepository<TipoPersona> _tipoPersonaRepository;

    public PersonaService(
        IBaseRepository<Persona> personaRepository,
        IBaseRepository<Estudiante> estudianteRepository,
        IBaseRepository<Conductor> conductorRepository,
        IBaseRepository<TipoPersona> tipoPersonaRepository)
    {
        _personaRepository = personaRepository;
        _estudianteRepository = estudianteRepository;
        _conductorRepository = conductorRepository;
        _tipoPersonaRepository = tipoPersonaRepository;
    }

    public async Task<OperationResult<List<PersonaDto>>> GetAll()
    {
        try
        {
            var personas = await _personaRepository.GetAllAsync();
            var dtos = personas.Select(p => new PersonaDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                DocumentoIdentidad = p.DocumentoIdentidad,
                Telefono = p.Telefono,
                TipoPersona = p.Tipo?.Nombre ?? "Desconocido",
                Activo = p.Activo
            }).ToList();

            return OperationResult<List<PersonaDto>>.Ok(dtos, "Personas obtenidas correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<List<PersonaDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<PersonaDto>> GetById(int id)
    {
        try
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
                return OperationResult<PersonaDto>.Fail("Persona no encontrada");

            var dto = new PersonaDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                DocumentoIdentidad = persona.DocumentoIdentidad,
                Telefono = persona.Telefono,
                TipoPersona = persona.Tipo?.Nombre ?? "Desconocido",
                Activo = persona.Activo
            };

            return OperationResult<PersonaDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return OperationResult<PersonaDto>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Save(SavePersonaDto dto)
    {
        try
        {
            // Validar documento único
            var existe = await _personaRepository.FindAsync(p => p.DocumentoIdentidad == dto.DocumentoIdentidad);
            if (existe.Any())
                return OperationResult<int>.Fail("Ya existe una persona con ese documento");

            Persona persona;

            switch (dto.TipoPersonaId)
            {
                case 1: // Estudiante
                    persona = new Estudiante
                    {
                        Nombre = dto.Nombre,
                        Apellido = dto.Apellido,
                        DocumentoIdentidad = dto.DocumentoIdentidad,
                        Telefono = dto.Telefono,
                        Direccion = dto.Direccion,
                        TipoPersonaId = dto.TipoPersonaId,
                        FechaCreacion = DateTime.Now,
                        Activo = true,
                        Matricula = dto.Matricula!,
                        Carrera = dto.Carrera ?? "Sin carrera",
                    };
                    break;

                case 3: // Conductor
                    persona = new Conductor
                    {
                        Nombre = dto.Nombre,
                        Apellido = dto.Apellido,
                        DocumentoIdentidad = dto.DocumentoIdentidad,
                        Telefono = dto.Telefono,
                        Direccion = dto.Direccion,
                        TipoPersonaId = dto.TipoPersonaId,
                        FechaCreacion = DateTime.Now,
                        Activo = true,
                        LicenciaConducir = dto.LicenciaConducir!
                    };
                    break;

                default:
                    return OperationResult<int>.Fail("Tipo de persona inválido");
            }

            await _personaRepository.AddAsync(persona);

            // Crear registro específico según tipo
            switch (dto.TipoPersonaId)
            {
                case 1: // Estudiante
                    if (string.IsNullOrEmpty(dto.Matricula))
                        return OperationResult<int>.Fail("Matrícula requerida para estudiante");

                    var estudiante = new Estudiante
                    {
                        Id = persona.Id,  
                        Matricula = dto.Matricula,
                        Carrera = dto.Carrera ?? "Sin carrera",
                        FechaIngreso = DateTime.Now 
                    };
                    await _estudianteRepository.AddAsync(estudiante);
                    break;

                case 3: // Conductor
                    if (string.IsNullOrEmpty(dto.LicenciaConducir))
                        return OperationResult<int>.Fail("Licencia requerida para conductor");

                    var conductor = new Conductor
                    {
                        Id = persona.Id,  
                        LicenciaConducir = dto.LicenciaConducir,
                        FechaVencimientoLicencia = dto.FechaVencimientoLicencia ?? DateTime.Now.AddYears(1),
                        CategoriaLicencia = "2",
                        EstadoConductor = EstadoConductor.Disponible,  
                        // Usar enum
                        FechaContratacion = DateTime.Now 
                    };
                    await _conductorRepository.AddAsync(conductor);
                    break;
            }

            return OperationResult<int>.Ok(persona.Id, "Persona guardada correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al guardar: {ex.Message}");
        }
    }

    public async Task<OperationResult<int>> Update(UpdatePersonaDto dto)
    {
        try
        {
            var persona = await _personaRepository.GetByIdAsync(dto.Id);
            if (persona == null)
                return OperationResult<int>.Fail("Persona no encontrada");

            persona.Nombre = dto.Nombre;
            persona.Apellido = dto.Apellido;
            persona.Telefono = dto.Telefono;
            persona.Direccion = dto.Direccion;
            persona.Activo = dto.Activo;
            persona.FechaModificacion = DateTime.Now; 

            await _personaRepository.UpdateAsync(persona);
            return OperationResult<int>.Ok(persona.Id, "Persona actualizada correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<int>.Fail($"Error al actualizar: {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> Remove(RemovePersonaDto dto)
    {
        try
        {
            var persona = await _personaRepository.GetByIdAsync(dto.Id);
            if (persona == null)
                return OperationResult<bool>.Fail("Persona no encontrada");

            persona.Activo = false;
            persona.FechaModificacion = DateTime.Now;  
            await _personaRepository.UpdateAsync(persona);

            return OperationResult<bool>.Ok(true, "Persona eliminada correctamente");
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.Fail($"Error al eliminar: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<PersonaDto>>> GetEstudiantes()
    {
        try
        {
            var estudiantes = await _estudianteRepository.GetAllAsync();
            var dtos = estudiantes.Select(e => new PersonaDto
            {
                Id = e.Id,  
                Nombre = e.Nombre,  
                Apellido = e.Apellido,
                DocumentoIdentidad = e.DocumentoIdentidad,
                Telefono = e.Telefono,
                Matricula = e.Matricula,  
                TipoPersona = "Estudiante",
                Activo = e.Activo
            }).ToList();

            return OperationResult<List<PersonaDto>>.Ok(dtos, $"{dtos.Count} estudiantes encontrados");
        }
        catch (Exception ex)
        {
            return OperationResult<List<PersonaDto>>.Fail($"Error: {ex.Message}");
        }
    }

    public async Task<OperationResult<List<PersonaDto>>> GetConductoresDisponibles()
    {
        try
        {
            var conductores = await _conductorRepository.FindAsync(c =>
                c.EstadoConductor == EstadoConductor.Disponible && c.Activo);  // ✅ Usar enum

            var dtos = conductores.Select(c => new PersonaDto
            {
                Id = c.Id,  // ✅ Hereda de Persona
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                DocumentoIdentidad = c.DocumentoIdentidad,
                Telefono = c.Telefono,
                LicenciaConducir = c.LicenciaConducir,  // ✅ Agregar si existe en DTO
                TipoPersona = "Conductor",
                Activo = c.Activo
            }).ToList();

            return OperationResult<List<PersonaDto>>.Ok(dtos, $"{dtos.Count} conductores disponibles");
        }
        catch (Exception ex)
        {
            return OperationResult<List<PersonaDto>>.Fail($"Error: {ex.Message}");
        }
    }
}
