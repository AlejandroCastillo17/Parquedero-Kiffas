using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.BD
{
    public class MensualidadBD
    {
        public static bool GuardarMensualidad(Mensualidad mensualidad)
        {
            try
            {
                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();
                    string query = "INSERT INTO Mensualidades (Placa, Tipo, Propietario, Telefono, FechaInicio, FechaFin, EstadoPago, Costo) VALUES (@Placa, @Tipo, @Propietario, @Telefono, @FechaInicio, @FechaFin, @EstadoPago, @Costo)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Placa", mensualidad.Placa);
                        cmd.Parameters.AddWithValue("@Tipo", mensualidad.Tipo);
                        cmd.Parameters.AddWithValue("@Propietario", mensualidad.Propietario);
                        cmd.Parameters.AddWithValue("@Telefono", mensualidad.Telefono);
                        cmd.Parameters.AddWithValue("@FechaInicio", mensualidad.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", mensualidad.FechaFin);
                        cmd.Parameters.AddWithValue("@EstadoPago", mensualidad.EstadoPago);
                        cmd.Parameters.AddWithValue("@Costo", mensualidad.costo);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la mensualidad: " + ex.Message);
                return false;
            }
        }

        public static List<Mensualidad> ObtenerMensualidades()
        {
            List<Mensualidad> ListaMensualidades = new List<Mensualidad>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT * FROM Mensualidades ";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Mensualidad mensualidad = new Mensualidad
                            {
                                Id = reader.GetInt32("Id"),
                                Placa = reader.GetString("Placa"),
                                Tipo = reader.GetString("Tipo"),
                                Propietario = reader.GetString("Propietario"),
                                Telefono = reader.GetString("Telefono"),
                                FechaInicio = reader.GetDateTime("FechaInicio"),
                                FechaFin = reader.GetDateTime("FechaFin"),
                                EstadoPago = reader.GetString("EstadoPago"),
                                costo = reader.GetDecimal("Costo")
                            };
                            ListaMensualidades.Add(mensualidad);
                        }
                    }
                }
            }
            return ListaMensualidades;
        }

        public static void EliminarMensualidad(int idMensualidad)
        {

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "DELETE FROM Mensualidades WHERE id = @id";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@id", idMensualidad);
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Mensualidad eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la mensualidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la mensualidad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static bool RenovarMensualidad(int idMensualidad, DateTime nuevaInicio, DateTime nuevaFin)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "UPDATE Mensualidades SET FechaInicio = @inicio, FechaFin = @fin, EstadoPago = 'Pagado' WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@inicio", nuevaInicio);
                    cmd.Parameters.AddWithValue("@fin", nuevaFin);
                    cmd.Parameters.AddWithValue("@id", idMensualidad);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
