using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parqueadero_Kiffas.Logica
{
    public class Tarifas
    {
        public string TipoVehiculo { get; set; } 
        public decimal TarifaPorHora { get; set; }
        public decimal TarifaPorDia { get; set; }
        public decimal TarifaMensual { get; set; }
        public decimal TarifaLavada { get; set; }
    }
}
