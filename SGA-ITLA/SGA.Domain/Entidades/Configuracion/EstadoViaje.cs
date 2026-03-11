using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Domain.Entidades.Configuracion;

public class EstadoViaje
{
    
    public const int Programado = 1;
    public const int EnCurso = 2;
    public const int Completado = 3;
    public const int Cancelado = 4;

    
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

