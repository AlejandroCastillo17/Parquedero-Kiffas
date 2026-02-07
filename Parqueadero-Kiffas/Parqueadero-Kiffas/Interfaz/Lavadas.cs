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
using Parqueadero_Kiffas.BD;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.Interfaz
{
    public partial class Lavadas : Form
    {
        public Lavadas()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;

            btnV.MouseEnter += BtnV_MouseEnter;
            btnV.MouseLeave += BtnV_MouseLeave;

            btnGuardar.MouseEnter += BtnGuardar_MouseEnter;
            btnGuardar.MouseLeave += BtnGuardar_MouseLeave;

            btnLimpiar.MouseEnter += BtnLimpiar_MouseEnter;
            btnLimpiar.MouseLeave += BtnLimpiar_MouseLeave;
        }


        //****************************************************************************************

        // Logica

        private void Lavadas_Load(object sender, EventArgs e)
        {
            cmbTipo.Items.AddRange(new string[] { "Carro","Camioneta", "Moto" });
            cmbTipo.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string tipo = cmbTipo.SelectedItem?.ToString();
            if (!ValidarCampos())
            {
                return;
            }

            decimal tarifaLavada = TarifasBD.ObtenerCostoLavada(tipo);
            if (tarifaLavada <= 0)
            {
                MessageBox.Show("No se encontró tarifa para el tipo de vehículo: " + tipo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Ingresos ingreso = new Ingresos
            {
                Placa = txtPlaca.Text.Trim(),
                TipoServicio = "Lavada",
                Costo = tarifaLavada,
                Fecha = DateTime.Now
            };

            if (IngresosBD.GuardarIngreso(ingreso))
            {
                MessageBox.Show("Servicio de lavado registrado con éxito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al registrar el servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtPlaca.Clear();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text) ||
                string.IsNullOrWhiteSpace(cmbTipo.SelectedItem?.ToString()))
            {
                MessageBox.Show("Los campos están vacíos, verifique por favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPlaca.Clear();
        }

        private void btnV_Click(object sender, EventArgs e)
        {
            Volver();
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
            btnGuardar.Size = new Size(186, 73);
            btnGuardar.FillColor = Color.Black;
            btnGuardar.ForeColor = Color.Gold;
        }

        private void BtnGuardar_MouseLeave(object sender, EventArgs e)
        {
            btnGuardar.Size = new Size(180, 71);
            btnGuardar.FillColor = Color.Gold;
            btnGuardar.ForeColor = Color.Black;
        }

        // btn Limpiar
        private void BtnLimpiar_MouseEnter(object sender, EventArgs e)
        {
            btnLimpiar.Size = new Size(166, 47);
            btnLimpiar.FillColor = Color.Black;
            btnLimpiar.ForeColor = Color.Gold;
        }

        private void BtnLimpiar_MouseLeave(object sender, EventArgs e)
        {
            btnLimpiar.Size = new Size(160, 45);
            btnLimpiar.FillColor = Color.Gold;
            btnLimpiar.ForeColor = Color.Black;
        }

        //*******************************************************************************************

        // transiciones

        private void Volver()
        {
            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);

            this.Hide();

            Menu menu = new Menu();
            menu.Show();

            transicion.ShowSync(menu);
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //********************************************************************************************
    }
}
