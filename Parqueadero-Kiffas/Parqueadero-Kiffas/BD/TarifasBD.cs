using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.BD
{
    public class TarifasBD
    {
        public static bool GuardarTarifa(Tarifas tarifa)
        {
            try
            {
                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();
                    string query = "UPDATE ConfiguracionTarifas SET TarifaPorHora = @TarifaPorHora, TarifaPorDia = @TarifaPorDia, TarifaMensual = @TarifaMensual, TarifaLavada = @TarifaLavada WHERE TipoVehiculo = @TipoVehiculo";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@TarifaPorHora", tarifa.TarifaPorHora);
                        cmd.Parameters.AddWithValue("@TarifaPorDia", tarifa.TarifaPorDia);
                        cmd.Parameters.AddWithValue("@TarifaMensual", tarifa.TarifaMensual);
                        cmd.Parameters.AddWithValue("@TarifaLavada", tarifa.TarifaLavada);
                        cmd.Parameters.AddWithValue("@TipoVehiculo", tarifa.TipoVehiculo);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la tarifa: " + ex.Message);
                return false;
            }
        }

        public static decimal ObtenerCostoHora(string tipoVehiculo)
        {
            decimal costoHora = 0;

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT TarifaPorHora FROM ConfiguracionTarifas WHERE TipoVehiculo = @TipoVehiculo";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@TipoVehiculo", tipoVehiculo);

                    object result = cmd.ExecuteScalar(); 

                    if (result != null && decimal.TryParse(result.ToString(), out decimal tarifa))
                    {
                        costoHora = tarifa;
                    }
                }
            }

            return costoHora;
        }

        public static decimal ObtenerCostoDia(string tipoVehiculo)
        {
            decimal costoDia = 0;

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT TarifaPorDia FROM ConfiguracionTarifas WHERE TipoVehiculo = @TipoVehiculo";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@TipoVehiculo", tipoVehiculo);

                    object result = cmd.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal tarifa))
                    {
                        costoDia = tarifa;
                    }
                }
            }

            return costoDia;
        }

        public static decimal ObtenerCostoMensualidad(string tipoVehiculo)
        {
            decimal costoMen = 0;

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT TarifaMensual FROM ConfiguracionTarifas WHERE TipoVehiculo = @TipoVehiculo";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@TipoVehiculo", tipoVehiculo);

                    object result = cmd.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal tarifa))
                    {
                        costoMen = tarifa;
                    }
                }
            }

            return costoMen;
        }

        public static decimal ObtenerCostoLavada(string tipoVehiculo)
        {
            decimal costo = 0;

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT TarifaLavada FROM ConfiguracionTarifas WHERE TipoVehiculo = @TipoVehiculo";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@TipoVehiculo", tipoVehiculo);

                    object result = cmd.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal tarifa))
                    {
                        costo = tarifa;
                    }
                }
            }

            return costo;
        }
    }
}
