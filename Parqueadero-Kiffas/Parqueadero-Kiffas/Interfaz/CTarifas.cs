using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using Parqueadero_Kiffas.BD;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.Interfaz
{
    public partial class CTarifas : Form
    {
        public CTarifas()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;

            btnV.MouseEnter += BtnV_MouseEnter;
            btnV.MouseLeave += BtnV_MouseLeave;

            btnGuardar.MouseEnter += BtnGuardar_MouseEnter;
            btnGuardar.MouseLeave += BtnGuardar_MouseLeave;
        }


        //****************************************************************************************

        // Logica

        private void CTarifas_Load(object sender, EventArgs e)
        {
            cmbTipo.Items.AddRange(new string[] { "Carro", "Camioneta", "Moto" });
            cmbTipo.SelectedIndex = 0;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = cmbTipo.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(tipo)) return;

            txtHora.Text = "";
            txtDia.Text = "";
            txtMensualidad.Text = "";
            txtLavada.Text = "";


            // 👉 Cargar las tarifas desde la base de datos
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT TarifaPorHora, TarifaPorDia, TarifaMensual, TarifaLavada FROM ConfiguracionTarifas WHERE TipoVehiculo = @tipo";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@tipo", tipo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtHora.Text = reader.GetDecimal("TarifaPorHora").ToString();
                            txtDia.Text = reader.GetDecimal("TarifaPorDia").ToString();
                            txtMensualidad.Text = reader.GetDecimal("TarifaMensual").ToString();
                            txtLavada.Text = reader.GetDecimal("TarifaLavada").ToString();
                        }
                    }
                }
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            string tipo = cmbTipo.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(tipo))
            {
                MessageBox.Show("Debe seleccionar un tipo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal tarifaHora = 0;
            decimal tarifaDia = 0;
            decimal tarifaMensual = 0;
            decimal tarifaLavada = 0;

            try
            {
                tarifaMensual = Convert.ToDecimal(txtMensualidad.Text);
                tarifaHora = Convert.ToDecimal(txtHora.Text);
                tarifaDia = Convert.ToDecimal(txtDia.Text);
                tarifaLavada = Convert.ToDecimal(txtLavada.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Verifique que los valores numéricos sean válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Tarifas tarifa = new Tarifas
            {
                TipoVehiculo = tipo,
                TarifaPorHora = tarifaHora,
                TarifaPorDia = tarifaDia,
                TarifaMensual = tarifaMensual,
                TarifaLavada = tarifaLavada
            };

            bool guardado = TarifasBD.GuardarTarifa(tarifa);
            if (guardado)
            {
                MessageBox.Show("Tarifas actualizadas con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al actualizar las tarifas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidarCampos()
        {
            if (cmbTipo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de vehiculo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtHora.Text) ||
                string.IsNullOrWhiteSpace(txtDia.Text) ||
                string.IsNullOrWhiteSpace(txtMensualidad.Text) ||
                string.IsNullOrWhiteSpace(txtLavada.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtHora.Text, out _))
            {
                MessageBox.Show("El precio por hora debe ser un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtDia.Text, out _))
            {
                MessageBox.Show("El precio por dia debe ser un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtMensualidad.Text, out _))
            {
                MessageBox.Show("El precio por mensualidad debe ser un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtLavada.Text, out _))
            {
                MessageBox.Show("El precio por lavada debe ser un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }


        private void btnV_Click(object sender, EventArgs e)
        {
            IrMenu();
        }

        private void txtHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla si no es un número
            }
        }

        private void txtDia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla si no es un número
            }
        }

        private void txtMensualidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla si no es un número
            }
        }

        private void txtLavada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla si no es un número
            }
        }

        //***************************************************************************************

        // Diseño y animaciones

        // btn volver
        private void BtnV_MouseEnter(object sender, EventArgs e)
        {
            btnV.Size = new Size(64, 64);
        }

        private void BtnV_MouseLeave(object sender, EventArgs e)
        {
            btnV.Size = new Size(60, 60);
        }

        // btn Guardar
        private void BtnGuardar_MouseEnter(object sender, EventArgs e)
        {
            btnGuardar.Size = new Size(166, 73);
            btnGuardar.FillColor = Color.Black;
            btnGuardar.ForeColor = Color.Gold;
        }

        private void BtnGuardar_MouseLeave(object sender, EventArgs e)
        {
            btnGuardar.Size = new Size(160, 71);
            btnGuardar.FillColor = Color.Gold;
            btnGuardar.ForeColor = Color.Black;
        }


        //*******************************************************************************************

        // transiciones

        private void IrMenu()
        {
            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);

            this.Hide();

            Menu menu = new Menu();
            menu.Show();

            transicion.ShowSync(menu);
        }

        private void lbld_Click(object sender, EventArgs e)
        {

        }

        //********************************************************************************************
    }
}
