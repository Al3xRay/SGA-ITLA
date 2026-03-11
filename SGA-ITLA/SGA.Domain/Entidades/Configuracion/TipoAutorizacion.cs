using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Domain.Entidades.Configuracion;


public class TipoAutorizacion
{
    
    public const int TarjetaRecargable = 1;
    public const int TicketMensual = 2;
    public const int PaseGratuito = 3;

    
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}
