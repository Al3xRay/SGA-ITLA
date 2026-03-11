using SGA.Application.Dtos;

namespace SGA.Application.Dtos.Personas;

    public class SavePersonaDto : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public int TipoPersonaId { get; set; }

  
        public string? Matricula { get; set; }
        public string? Carrera { get; set; }

     
        public string? LicenciaConducir { get; set; }
        public DateTime? FechaVencimientoLicencia { get; set; }
    }

