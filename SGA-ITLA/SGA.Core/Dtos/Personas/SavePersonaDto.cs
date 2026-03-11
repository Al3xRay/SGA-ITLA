using SGA.Application.Dtos;
using SGAITLA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Personas;

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

