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
using Parqueadero_Kiffas.Logica;
using Parqueadero_Kiffas.BD;
using System.Drawing.Printing;

namespace Parqueadero_Kiffas.Interfaz
{
    public partial class RegistrarVehiculo : Form
    {
        public RegistrarVehiculo()
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

        private void RegistrarVehiculo_Load(object sender, EventArgs e)
        {
            cmbTipo.Items.AddRange(new string[] { "Carro", "Moto", "Camioneta" });
            cmbTipo.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            Vehiculo vehiculo = new Vehiculo
            {
                Placa = txtPlaca.Text,
                Tipo = cmbTipo.SelectedItem?.ToString(),
                Propietario = txtPropietario.Text,
                Telefono = txtNumPro.Text,
                HoraEntrada = DateTime.Now,
                HoraSalida = null,
                TotalCobro = null,
                Fecha = DateTime.Now.Date
            };

            if (VehiculosBD.GuardarVehiculo(vehiculo))
            {
                MessageBox.Show("Servicio registrado con éxito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Imprimir(vehiculo);
            }
            else
            {
                MessageBox.Show("Error al registrar el servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtPlaca.Clear();
            txtPropietario.Clear();
            txtNumPro.Clear();
        }

        private string textoFactura = "";

        private void Imprimir(Vehiculo vehiculo)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*** PARQUEADERO KIFFA ***");
            sb.AppendLine($"Calle 42 sur - Carrera 81");
            sb.AppendLine("--------------------------------------------");
            sb.AppendLine($"Placa: {vehiculo.Placa}");
            sb.AppendLine($"Tipo de vehiculo: {vehiculo.Tipo}");
            sb.AppendLine($"Propietario: {vehiculo.Propietario}");
            sb.AppendLine($"Telefono: {vehiculo.Telefono}");
            sb.AppendLine($"Hora de ingreso: {vehiculo.HoraEntrada}");
            sb.AppendLine($"Fecha: {vehiculo.Fecha}");
            sb.AppendLine("--------------------------------------------");
            sb.AppendLine("Con esta factura registra su salida");
            sb.AppendLine("Gracias por su servicio");
            textoFactura = sb.ToString();

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(ImprimirFactura);

            try
            {
                pd.Print(); // Enviar a impresora predeterminada
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message);
            }

        }

        private void ImprimirFactura(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Font fuente = new System.Drawing.Font("Consolas", 9, FontStyle.Regular);
            e.Graphics.DrawString(textoFactura, fuente, Brushes.Black, new RectangleF(0, 0, 280, 10000));
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text) ||
                string.IsNullOrWhiteSpace(cmbTipo.SelectedItem?.ToString()) ||
                string.IsNullOrWhiteSpace(txtPropietario.Text) ||
                string.IsNullOrWhiteSpace(txtNumPro.Text))
            {
                MessageBox.Show("Los campos del vehiculo están vacíos, verifique por favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPlaca.Clear();
            txtPropietario.Clear();
            txtNumPro.Clear();
        }

        private void btnV_Click(object sender, EventArgs e)
        {
            IrMenu();
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

        //********************************************************************************************
        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}
