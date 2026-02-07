using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.BD
{
    public class IngresosBD
    {
        public static bool GuardarIngreso(Ingresos ingreso)
        {
            try
            {
                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();
                    string query = "INSERT INTO Ingresos (Placa, TipoServicio, Costo, Fecha) VALUES (@Placa, @TipoServicio, @Costo, @Fecha)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Placa", ingreso.Placa);
                        cmd.Parameters.AddWithValue("@TipoServicio", ingreso.TipoServicio);
                        cmd.Parameters.AddWithValue("@Costo", ingreso.Costo);
                        cmd.Parameters.AddWithValue("@Fecha", ingreso.Fecha);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el ingreso: " + ex.Message);
                return false;
            }
        }
    }
}
