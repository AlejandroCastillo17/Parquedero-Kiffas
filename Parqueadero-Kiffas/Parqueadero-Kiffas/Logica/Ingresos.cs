using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parqueadero_Kiffas.Logica
{
    public class Ingresos
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string TipoServicio { get; set; }
        public decimal Costo { get; set; }
        public DateTime Fecha { get; set; }
    }
}
