using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Parqueadero_Kiffas.BD;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.Interfaz
{
    public partial class AgregarMensualidad : Form
    {
        public AgregarMensualidad()
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

        private void AgregarMensualidad_Load(object sender, EventArgs e)
        {
            cmbTipo.Items.AddRange(new string[] { "Carro", "Camioneta", "Moto", "Miscelaneo" });
            cmbTipo.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string tipo = cmbTipo.SelectedItem?.ToString();
            if (!ValidarCampos())
            {
                return;
            }


            decimal tarifaMensualidad = 0;

            if (cmbTipo.SelectedItem.ToString() == "Miscelaneo")
            {
                if (decimal.TryParse(txtTM.Text, out decimal tarifa))
                {
                    tarifaMensualidad = tarifa; 
                }
                else
                {
                    MessageBox.Show("Ingrese una tarifa válida.");
                    return; 
                }
            }
            else
            {
                tarifaMensualidad = TarifasBD.ObtenerCostoMensualidad(tipo);
            }


            if (tarifaMensualidad <= 0)
            {
                MessageBox.Show("No se encontró tarifa para el tipo de vehículo: " + tipo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Mensualidad mensualidad = new Mensualidad
            {
                Placa = txtPlaca.Text.Trim(),
                Tipo = tipo,
                Propietario = txtPropietario.Text.Trim(),
                Telefono = txtNumPro.Text.Trim(),
                FechaInicio = DateTime.Today,
                FechaFin = DateTime.Today.AddMonths(1),
                EstadoPago = "Pagado",
                costo = tarifaMensualidad
            };

            Ingresos ingreso = new Ingresos
            {
                Placa = txtPlaca.Text.Trim(),
                TipoServicio = "Mensualidad",
                Costo = tarifaMensualidad,
                Fecha = DateTime.Now
            };

            if (MensualidadBD.GuardarMensualidad(mensualidad) && IngresosBD.GuardarIngreso(ingreso))
            {
                MessageBox.Show("Mensualidad registrado con éxito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Imprimir(mensualidad.Placa, mensualidad.Tipo, mensualidad.Propietario, mensualidad.Telefono, mensualidad.FechaFin);
            }
            else
            {
                MessageBox.Show("Error al registrar la mensualidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtPlaca.Clear();
            txtPropietario.Clear();
            txtNumPro.Clear();
            txtTM.Clear();
            cmbTipo.SelectedIndex = 0;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text) ||
                string.IsNullOrWhiteSpace(cmbTipo.SelectedItem?.ToString()) ||
                string.IsNullOrWhiteSpace(txtPropietario.Text) ||
                string.IsNullOrWhiteSpace(txtNumPro.Text))
            {
                MessageBox.Show("Los campos están vacíos, verifique por favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private string textoFactura = "";

        private void Imprimir(string placa, string tipo, string propietario, string telefono, DateTime fechafin)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*** PARQUEADERO KIFFA ***");
            sb.AppendLine($"Calle 42 sur - Carrera 81");
            sb.AppendLine("--------------------------------------------");
            sb.AppendLine($"Nueva Mensualidad");
            sb.AppendLine("--------------------------------------------");
            sb.AppendLine($"Placa: {placa}");
            sb.AppendLine($"Tipo de vehiculo: {tipo}");
            sb.AppendLine($"Propietario: {propietario}");
            sb.AppendLine($"Telefono: {telefono}");
            sb.AppendLine($"Fecha Fin: {fechafin}");
            sb.AppendLine("--------------------------------------------");
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

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedItem.ToString() == "Miscelaneo")
            {
                lbl.Text = "Nombre: ";
                txtPlaca.PlaceholderText = "Nombre para identificar el carrito";
                l.Visible = true;
                txtTM.Visible = true;
            }
            else
            {
                lbl.Text = "Placa: ";
                txtPlaca.PlaceholderText = "Placa del vehiculo";
                l.Visible = false;
                txtTM.Visible = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPlaca.Clear();
            txtPropietario.Clear();
            txtTM.Clear();
            txtNumPro.Clear();
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

            Mensualidades mensualidad = new Mensualidades();
            mensualidad.Show();

            transicion.ShowSync(mensualidad);
        }

        //********************************************************************************************
    }
}
