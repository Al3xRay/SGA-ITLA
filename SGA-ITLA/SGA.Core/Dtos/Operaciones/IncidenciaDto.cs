using SGA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAITLA.Application.Dtos.Operaciones;

public class IncidenciaDto : DtoBase
{
    public int Id { get; set; }
    public int ViajeId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaReporte { get; set; }
    public bool Resuelta { get; set; }
}