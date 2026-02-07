using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parqueadero_Kiffas.Logica
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; } 
        public string Propietario { get; set; }
        public string Telefono { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public decimal? TotalCobro { get; set; }
        public DateTime Fecha { get; set; }
    }
}
