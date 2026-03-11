using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Domain.Entidades.Transporte;
using SGAITLA.Domain.Base;
using SGAITLA.Domain.Entidades.Configuracion;


namespace SGAITLA.Domain.Entidades.Transporte;

public class Autobus : AuditEntity
{
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }

    public EstadoAutobus EstadoAutobus { get; set; } = EstadoAutobus.Disponible;

    public ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}

    // definir fecha de creacion
