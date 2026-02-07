using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.BD
{
    public class Conexion
    {
        private static string cadenaConexion = "server=localhost; database=Parqueadero; user=root; password=Alejo123-;";
        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }

    }
}
