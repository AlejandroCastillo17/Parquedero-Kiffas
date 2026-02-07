using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Parqueadero_Kiffas.BD;
using Parqueadero_Kiffas.Logica;

namespace Parqueadero_Kiffas.Interfaz
{
    public partial class Mensualidades : Form
    {
        public Mensualidades()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;

            btnV.MouseEnter += BtnV_MouseEnter;
            btnV.MouseLeave += BtnV_MouseLeave;

            btnAM.MouseEnter += BtnAM_MouseEnter;
            btnAM.MouseLeave += BtnAM_MouseLeave;
        }

        private void Mensualidades_Load(object sender, EventArgs e)
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
            DG.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // 👉 Estilos visuales de celdas
            DG.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 11, FontStyle.Regular),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.White,
                SelectionForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // 👉 Estilo de los encabezados
            DG.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Gold,
                ForeColor = Color.Black,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // 👉 Altura de filas
            DG.RowTemplate.Height = 50;

            // 👉 Crear columnas manualmente
            DG.ColumnCount = 9;
            DG.Columns[0].Name = "Id";
            DG.Columns[0].Visible = false;

            DG.Columns[1].Name = "Placa";
            DG.Columns[1].Width = 110;

            DG.Columns[2].Name = "Tipo";
            DG.Columns[2].Width = 100;

            DG.Columns[3].Name = "Propietario";
            DG.Columns[3].Width = 150;

            DG.Columns[4].Name = "Telefono";
            DG.Columns[4].Width = 130;

            DG.Columns[5].Name = "F Inicio";
            DG.Columns[5].Width = 130;

            DG.Columns[6].Name = "F Fin";
            DG.Columns[6].Width = 130;

            DG.Columns[7].Name = "Estado de pago";
            DG.Columns[7].Width = 130;

            DG.Columns[8].Name = "Costo";
            DG.Columns[8].Visible = false;

            DG.Columns["Estado de pago"].Width = 110;

            // 👉 Botón Renovar
            DataGridViewButtonColumn btnRenovar = new DataGridViewButtonColumn
            {
                Name = "Renovar",
                HeaderText = "",
                Text = "↻ Renovar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Popup,
                Width = 100
            };
            btnRenovar.DefaultCellStyle.BackColor = Color.Black;
            btnRenovar.DefaultCellStyle.ForeColor = Color.White;
            DG.Columns.Add(btnRenovar);

            // 👉 Botón Eliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "",
                Text = "❌",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Popup,
                Width = 30
            };
            btnEliminar.DefaultCellStyle.BackColor = Color.Firebrick;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;
            DG.Columns.Add(btnEliminar);

            // 👉 Evento para mantener color del botón eliminar
            DG.CellPainting += (s, e) =>
            {
                if (e.ColumnIndex == DG.Columns["Eliminar"].Index && e.RowIndex >= 0)
                {
                    e.CellStyle.BackColor = Color.Firebrick;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.SelectionBackColor = Color.Firebrick;
                    e.CellStyle.SelectionForeColor = Color.White;
                }
            };

            // 👉 Evento click para Eliminar/Renovar
            DG.CellClick += (s, e) =>
            {
                if (e.ColumnIndex == DG.Columns["Eliminar"].Index && e.RowIndex >= 0)
                {
                    int idmp = Convert.ToInt32(DG.Rows[e.RowIndex].Cells["Id"].Value);
                    DialogResult confirm = MessageBox.Show("¿Seguro que deseas eliminar esta mensualidad?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        MensualidadBD.EliminarMensualidad(idmp);
                        DG.Rows.RemoveAt(e.RowIndex);
                        txtPlaca.Clear();
                        CargarMensualidades();
                    }
                }
                else if (e.ColumnIndex == DG.Columns["Renovar"].Index && e.RowIndex >= 0)
                {
                    int idMensualidad = Convert.ToInt32(DG.Rows[e.RowIndex].Cells["Id"].Value);
                    string placa = DG.Rows[e.RowIndex].Cells["Placa"].Value.ToString();
                    string tipo = DG.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                    string propietario = DG.Rows[e.RowIndex].Cells["Propietario"].Value.ToString();
                    string telefono = DG.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                    decimal costo = Convert.ToDecimal(DG.Rows[e.RowIndex].Cells["Costo"].Value);
                    DateTime fechaFin = Convert.ToDateTime(DG.Rows[e.RowIndex].Cells["F Fin"].Value);

                    // Sumar 30 días
                    DateTime fechaFinNueva = fechaFin.AddDays(30);

                    Ingresos ingreso = new Ingresos
                    {
                        Placa = placa,
                        TipoServicio = "Mensualidad",
                        Costo = costo,
                        Fecha = DateTime.Now
                    };

                    DialogResult confirm = MessageBox.Show("¿Deseas renovar esta mensualidad por otro mes?",
                                                           "Confirmar renovación",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        DateTime nuevaFechaInicio = DateTime.Today;
                        DateTime nuevaFechaFin = nuevaFechaInicio.AddMonths(1);

                        if (MensualidadBD.RenovarMensualidad(idMensualidad, nuevaFechaInicio, nuevaFechaFin) &&
                            IngresosBD.GuardarIngreso(ingreso))
                        {
                            MessageBox.Show("Mensualidad renovada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Imprimir(placa, tipo, propietario, telefono, nuevaFechaFin);
                        }
                        else
                        {
                            MessageBox.Show("Error al renovar la mensualidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        CargarMensualidades();
                    }
                }
            };

            // 👉 Cargar los datos
            CargarMensualidades();
        }



        List<Mensualidad> listaOriginal = new List<Mensualidad>();

        private void CargarMensualidades()
        {
            listaOriginal = MensualidadBD.ObtenerMensualidades();
            MostrarMensualidades(listaOriginal);
        }

        private void MostrarMensualidades(List<Mensualidad> mensualidades)
        {
            DG.Rows.Clear();

            foreach (var m in mensualidades)
            {
                string estadoReal = (m.FechaFin < DateTime.Today) ? "Pendiente" : "Pagado";

                int rowIndex = DG.Rows.Add(m.Id, m.Placa, m.Tipo, m.Propietario, m.Telefono,
                                           m.FechaInicio.ToShortDateString(),
                                           m.FechaFin.ToShortDateString(),
                                           estadoReal, m.costo);

                var celdaEstado = DG.Rows[rowIndex].Cells["Estado de pago"];
                if (estadoReal == "Pagado")
                {
                    celdaEstado.Style.BackColor = Color.LightGreen;
                    celdaEstado.Style.ForeColor = Color.Black;
                }
                else
                {
                    celdaEstado.Style.BackColor = Color.IndianRed;
                    celdaEstado.Style.ForeColor = Color.White;
                }

                // Si está vencido, mostrar el botón "Renovar"
                var celdaRenovar = DG.Rows[rowIndex].Cells["Renovar"];
                celdaRenovar.ReadOnly = true; // Evitar edición
                celdaRenovar.Style.BackColor = (estadoReal == "Pendiente") ? Color.DarkOrange : Color.LightGray;
                celdaRenovar.Style.ForeColor = Color.White;


                if (estadoReal == "Pagado")
                {
                    // Ocultar el botón renovador si aún está vigente
                    celdaRenovar.Value = "";
                }
            }
        }

        private string textoFactura = "";

        private void Imprimir(string placa, string tipo, string propietario, string telefono, DateTime fechafin )
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*** PARQUEADERO KIFFA ***");
            sb.AppendLine($"Calle 42 sur - Carrera 81");
            sb.AppendLine("--------------------------------------------");
            sb.AppendLine($"Mensualidad Renovada");
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

        private void txtPlaca_TextChanged_1(object sender, EventArgs e)
        {
            string filtro = txtPlaca.Text.Trim().ToLower();

            var filtrados = listaOriginal
                .Where(m => !string.IsNullOrEmpty(m.Placa) && m.Placa.ToLower().Contains(filtro))
                .ToList();

            MostrarMensualidades(filtrados);
        }

        private void btnV_Click_1(object sender, EventArgs e)
        {
            IrMenu();
            txtPlaca.Clear();
        }

        private void btnAM_Click(object sender, EventArgs e)
        {
            IrAM();
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

        // btn IrAM
        private void BtnAM_MouseEnter(object sender, EventArgs e)
        {
            btnAM.Size = new Size(185, 73);
            btnAM.FillColor = Color.Black;
            btnAM.ForeColor = Color.Gold;
        }

        private void BtnAM_MouseLeave(object sender, EventArgs e)
        {
            btnAM.Size = new Size(179, 71);
            btnAM.FillColor = Color.Gold;
            btnAM.ForeColor = Color.Black;
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

        private void IrAM()
        {
            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);

            this.Hide();

            AgregarMensualidad AM = new AgregarMensualidad();
            AM.Show();

            transicion.ShowSync(AM);
        }
    }
}
