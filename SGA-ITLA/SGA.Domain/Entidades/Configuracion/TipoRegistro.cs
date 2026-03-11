using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Domain.Entidades.Configuracion;


public class TipoRegistro
{
    
    public const int Abordaje = 1;
    public const int Descenso = 2;
    public const int Incidencia = 3;

    
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}