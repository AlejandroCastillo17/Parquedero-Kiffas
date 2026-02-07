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
    public partial class ControlSalidas : Form
    {
        public ControlSalidas()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;

            btnV.MouseEnter += BtnV_MouseEnter;
            btnV.MouseLeave += BtnV_MouseLeave;
        }

        private void ControlSalidas_Load(object sender, EventArgs e)
        {


            // 👉 Estilos generales del DataGridView
            DG.ReadOnly = true;
            DG.RowHeadersVisible = false;
            DG.EnableHeadersVisualStyles = false;
            DG.ScrollBars = ScrollBars.Vertical;
            DG.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DG.CurrentCell = null;
            DG.MultiSelect = false;
            DG.ColumnHeadersVisible = true;
            DG.ColumnHeadersHeight = 40;
            DG.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            // 👉 Deshabilitar edición de celdas/filas/columnas
            DG.AllowUserToAddRows = false;
            DG.AllowUserToDeleteRows = false;
            DG.AllowUserToResizeColumns = false;
            DG.AllowUserToResizeRows = false;

            // 👉 Estilo de bordes
            DG.GridColor = Color.Black;
            DG.BorderStyle = BorderStyle.FixedSingle;
            DG.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            DG.AlternatingRowsDefaultCellStyle.BackColor = DG.RowsDefaultCellStyle.BackColor;


            // 👉 Estilos visuales de celdas
            DG.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 16, FontStyle.Bold),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                SelectionBackColor = Color.LightGray,
                SelectionForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // 👉 Estilo de los encabezados
            DG.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 16, FontStyle.Bold),
                BackColor = Color.Gold, // Puedes usar Color.FromArgb(0, 0, 192) para un azul más suave
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // 👉 Altura de filas
            DG.RowTemplate.Height = 70;

            // 👉 Crear columnas manualmente
            DG.ColumnCount = 6;
            DG.Columns[0].Name = "ID"; // Oculta el ID
            DG.Columns[0].Visible = false;

            DG.Columns[1].Name = "Placa";
            DG.Columns[1].Width = 160;

            DG.Columns[2].Name = "Tipo";
            DG.Columns[2].Width = 120;

            DG.Columns[3].Name = "Propietario";
            DG.Columns[3].Width = 180;

            DG.Columns[4].Name = "Telefono";
            DG.Columns[4].Width = 180;

            DG.Columns[5].Name = "Hora de Entrada";
            DG.Columns[5].Width = 290;

            // 👉 Agregar botón de eliminar
            DataGridViewButtonColumn btnRG = new DataGridViewButtonColumn
            {
                Name = "RG",
                HeaderText = "",
                Text = "Salida",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Popup,
                Width = 10
            };
            btnRG.DefaultCellStyle.BackColor = Color.Firebrick;
            btnRG.DefaultCellStyle.ForeColor = Color.White;
            DG.Columns.Add(btnRG);

            // 👉 Evento para mantener color del botón eliminar
            DG.CellPainting += (s, e) =>
            {
                if (e.ColumnIndex == DG.Columns["RG"].Index && e.RowIndex >= 0)
                {
                    e.CellStyle.BackColor = Color.Firebrick;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.SelectionBackColor = Color.Firebrick;
                    e.CellStyle.SelectionForeColor = Color.White;
                }
            };

            // 👉 Evento click para salida
            DG.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == DG.Columns["RG"].Index)
                {
                    try
                    {
                        int id = Convert.ToInt32(DG.Rows[e.RowIndex].Cells["ID"].Value);
                        string placa = DG.Rows[e.RowIndex].Cells["Placa"].Value.ToString();
                        string tipo = DG.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();

                        DateTime horaEntrada = Convert.ToDateTime(DG.Rows[e.RowIndex].Cells["Hora de Entrada"].Value);
                        DateTime horaSalida = DateTime.Now;

                        TimeSpan duracion = horaSalida - horaEntrada;
                        int horas = (int)Math.Ceiling(duracion.TotalHours);
                        int dias = (int)Math.Ceiling(duracion.TotalDays);

                        decimal tarifaHora = TarifasBD.ObtenerCostoHora(tipo);
                        decimal tarifaDia = TarifasBD.ObtenerCostoDia(tipo);

                        if (tarifaHora <= 0 || tarifaDia <= 0)
                        {
                            MessageBox.Show("No se encontró una tarifa válida para el tipo de vehículo: " + tipo,
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        decimal total;
                        string detalleCobro;

                        // Lógica tarifaria:
                        if (duracion.TotalHours <= 10)
                        {
                            total = horas * tarifaHora;
                            detalleCobro = $"{horas} hora(s) x ${tarifaHora} = ${total}";
                        }
                        else if (duracion.TotalHours <= 48)
                        {
                            total = tarifaDia;
                            detalleCobro = $"Tarifa diaria aplicada: 1 día x ${tarifaDia} = ${total}";
                        }
                        else
                        {
                            total = dias * tarifaDia;
                            detalleCobro = $"{dias} día(s) x ${tarifaDia} = ${total}";
                        }

                        DialogResult confirm = MessageBox.Show(
                            $"Tiempo: {duracion.TotalHours:F1} horas\n{detalleCobro}",
                            "Confirmar salida",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (confirm == DialogResult.Yes)
                        {
                            Ingresos ingreso = new Ingresos
                            {
                                Placa = placa,
                                TipoServicio = "Parqueadero",
                                Costo = total,
                                Fecha = DateTime.Now
                            };

                            bool actualizado = VehiculosBD.ActualizarVehiculo(id, horaSalida, total);
                            if (actualizado && IngresosBD.GuardarIngreso(ingreso))
                            {
                                DG.Rows.RemoveAt(e.RowIndex);
                                txtPlaca.Clear();
                                MessageBox.Show("Salida registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error al actualizar la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };



            // 👉 Finalmente, cargar los datos
            CargarVehiculos();
        }

        List<Vehiculo> listaOriginal = new List<Vehiculo>();

        private void CargarVehiculos()
        {
            listaOriginal = VehiculosBD.ObtenerVehiculos();
            MostrarVehiculos(listaOriginal);
        }

        private void MostrarVehiculos(List<Vehiculo> vehiculos)
        {
            DG.Rows.Clear();

            foreach (var v in vehiculos)
            {
                DG.Rows.Add(v.IdVehiculo, v.Placa, v.Tipo, v.Propietario, v.Telefono, v.HoraEntrada);
            }
        }

        private void txtPlaca_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtPlaca.Text.Trim().ToLower();

            var filtrados = listaOriginal
                .Where(v => !string.IsNullOrEmpty(v.Placa) && v.Placa.ToLower().Contains(filtro))
                .ToList();

            MostrarVehiculos(filtrados);
        }


        private void btnV_Click(object sender, EventArgs e)
        {
            IrMenu();
            txtPlaca.Clear();
        }

        //***************************************************************************************

        // Diseño y animaciones

        // Ingreso de vehiculos
        private void BtnV_MouseEnter(object sender, EventArgs e)
        {
            btnV.Size = new Size(64, 64);
        }

        private void BtnV_MouseLeave(object sender, EventArgs e)
        {
            btnV.Size = new Size(60, 60);
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


    }
}
