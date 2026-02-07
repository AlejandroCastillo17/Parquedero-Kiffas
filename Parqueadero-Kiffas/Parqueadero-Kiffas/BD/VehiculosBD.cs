using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Parqueadero_Kiffas.BD;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.BD
{
    public class VehiculosBD
    {
        public static bool GuardarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();
                    string query = "INSERT INTO Vehiculos (Placa, Tipo, Propietario, Telefono, HoraEntrada, HoraSalida, TotalCobro) VALUES (@Placa, @Tipo, @Propietario, @Telefono, @HoraEntrada, @HoraSalida, @TotalCobro)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Placa", vehiculo.Placa);
                        cmd.Parameters.AddWithValue("@Tipo", vehiculo.Tipo);
                        cmd.Parameters.AddWithValue("@Propietario", vehiculo.Propietario);
                        cmd.Parameters.AddWithValue("@Telefono", vehiculo.Telefono);
                        cmd.Parameters.AddWithValue("@HoraEntrada", vehiculo.HoraEntrada);
                        cmd.Parameters.AddWithValue("@HoraSalida", vehiculo.HoraSalida);
                        cmd.Parameters.AddWithValue("@TotalCobro", vehiculo.TotalCobro);

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

        public static List<Vehiculo> ObtenerVehiculos()
        {
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT * FROM Vehiculos WHERE HoraSalida IS NULL";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vehiculo vehiculo = new Vehiculo
                            {
                                IdVehiculo = reader.GetInt32("IdVehiculo"),
                                Placa = reader.GetString("Placa"),
                                Tipo = reader.GetString("Tipo"),
                                Propietario = reader.GetString("Propietario"),
                                Telefono = reader.GetString("Telefono"),
                                HoraEntrada = reader.GetDateTime("HoraEntrada"),
                                HoraSalida = reader.IsDBNull(reader.GetOrdinal("HoraSalida"))
                                             ? (DateTime?)null : reader.GetDateTime("HoraSalida"),
                                TotalCobro = reader.IsDBNull(reader.GetOrdinal("TotalCobro"))
                                             ? (decimal?)null : reader.GetDecimal("TotalCobro")
                            };
                            listaVehiculos.Add(vehiculo);
                        }
                    }
                }
            }
            return listaVehiculos;
        }

        public static bool ActualizarVehiculo(int id, DateTime horaSalida, decimal total)
        {
            using (var conn = Conexion.ObtenerConexion())
            {
                conn.Open();
                string sql = "UPDATE Vehiculos SET HoraSalida = @HoraSalida, TotalCobro = @TotalCobro WHERE IdVehiculo = @Id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HoraSalida", horaSalida);
                    cmd.Parameters.AddWithValue("@TotalCobro", total);
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


    }
}
