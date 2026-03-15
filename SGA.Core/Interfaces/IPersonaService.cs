using SGA.Application.Base;
using SGA.Application.Dtos.Personas;

namespace SGA.Application.Interfaces;

public interface IPersonaService : IBaseService<PersonaDto, SavePersonaDto, UpdatePersonaDto>
{
}
