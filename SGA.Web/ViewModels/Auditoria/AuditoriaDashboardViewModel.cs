using SGA.Web.Models.Operaciones;
using System.Collections.Generic;

namespace SGA.Web.ViewModels.Auditoria
{
    public class AuditoriaDashboardViewModel
    {
        public List<AuditoriaViajeDto> Viajes { get; set; } = new();
        public List<AuditoriaEmisionDto> Emisiones { get; set; } = new();
    }
}
