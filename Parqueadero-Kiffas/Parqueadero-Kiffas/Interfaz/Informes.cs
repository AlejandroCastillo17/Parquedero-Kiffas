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
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using Parqueadero_Kiffas.BD;

namespace Parqueadero_Kiffas.Interfaz
{
    public partial class Informes : Form
    {
        public Informes()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;

            btnV.MouseEnter += BtnV_MouseEnter;
            btnV.MouseLeave += BtnV_MouseLeave;

            btnPDF.MouseEnter += BtnPDF_MouseEnter;
            btnPDF.MouseLeave += BtnPDF_MouseLeave;
        }

        //****************************************************************************************

        // Logica

        private void Informes_Load(object sender, EventArgs e)
        {
            // Establecer rango predeterminado (últimos 7 días)
            DTPinicio.Value = DateTime.Now.AddDays(-7);
            DTPfin.Value = DateTime.Now;

            // Cargar el informe con la fecha predeterminada
            CargarInforme();
            CalcularVentaTotal();
            diseñoDG();
        }

        private void CargarInforme()
        {
            try
            {
                if (DGVinforme == null)
                {
                    MessageBox.Show("El DataGridView no está inicializado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime fechaInicio = DTPinicio.Value.Date;
                DateTime fechaFin = DTPfin.Value.Date.AddDays(1).AddSeconds(-1);

                string query = @"
                    SELECT 
                        Placa,
                        TipoServicio,
                        Costo AS Ingreso,
                        DATE_FORMAT(Fecha, '%Y-%m-%d %H:%i:%s') AS Fecha
                    FROM Ingresos
                    WHERE Fecha BETWEEN @fechaInicio AND @fechaFin
                    ORDER BY Fecha DESC;
                ";

                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        DGVinforme.DataSource = dt;
                    }
                }

                if (DGVinforme.Columns.Count == 0)
                {
                    MessageBox.Show("No se encontraron columnas en el DataGridView.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el informe: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CalcularVentaTotal()
        {
            try
            {
                if (DGVinforme.Rows.Count == 0) return; // Evita error si no hay datos

                decimal totalVentas = 0;

                foreach (DataGridViewRow row in DGVinforme.Rows)
                {
                    if (row.Cells["Ingreso"].Value != DBNull.Value)
                    {
                        decimal precio = Convert.ToDecimal(row.Cells["Ingreso"].Value);

                        totalVentas += precio;
                    }
                }

                lblVenta.Text = "Venta Total: $" + totalVentas.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular la venta total: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void diseñoDG()
        {
            // ===== CONFIGURACIÓN GENERAL =====
            DGVinforme.EnableHeadersVisualStyles = false;
            DGVinforme.ReadOnly = true;
            DGVinforme.AllowUserToAddRows = false;
            DGVinforme.AllowUserToDeleteRows = false;
            DGVinforme.AllowUserToResizeColumns = false;
            DGVinforme.AllowUserToResizeRows = false;
            DGVinforme.RowHeadersVisible = false;
            DGVinforme.MultiSelect = false;
            DGVinforme.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DGVinforme.CurrentCell = null;
            DGVinforme.ColumnHeadersVisible = true;
            DGVinforme.ColumnHeadersHeight = 40;
            DGVinforme.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DGVinforme.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // ===== ESTILO DE ENCABEZADOS =====
            DGVinforme.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Gold,
                ForeColor = Color.Black,
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = Color.FromArgb(255, 128, 0)
            };

            // ===== ESTILO DE CELDAS =====
            DGVinforme.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Regular),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = Color.White,
                SelectionForeColor = Color.Black
            };

            // ===== BORDES Y REJILLA =====
            DGVinforme.GridColor = Color.Black;
            DGVinforme.BorderStyle = BorderStyle.FixedSingle;
            DGVinforme.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // ===== CONFIGURACIÓN DE COLUMNAS =====
            if (DGVinforme.Columns.Count >= 4)
            {
                // 0: Placa
                DGVinforme.Columns[0].Width = 100;
                DGVinforme.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVinforme.Columns[0].Name = "Placa";
                DGVinforme.Columns[0].HeaderText = "Placa";

                // 1: TipoServicio
                DGVinforme.Columns[1].Width = 140;
                DGVinforme.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVinforme.Columns[1].Name = "TipoServicio";
                DGVinforme.Columns[1].HeaderText = "Tipo de Servicio";

                // 2: Ingreso
                DGVinforme.Columns[2].Width = 110;
                DGVinforme.Columns[2].DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                };
                DGVinforme.Columns[2].Name = "Ingreso"; 
                DGVinforme.Columns[2].HeaderText = "Ingreso";

                // 3: Fecha
                DGVinforme.Columns[3].Width = 160;
                DGVinforme.Columns[3].DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm:ss",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                };
                DGVinforme.Columns[3].Name = "Fecha";
                DGVinforme.Columns[3].HeaderText = "Fecha";
            }
        }



        private void DTPinicio_ValueChanged(object sender, EventArgs e)
        {
            CargarInforme();
            CalcularVentaTotal();
        }

        private void DTPfin_ValueChanged(object sender, EventArgs e)
        {
            CargarInforme();
            CalcularVentaTotal();
        }

        private void ExportarAPDF(DataGridView dgv, string titulo, string ganancia)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Guardar reporte"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.Document documento = new iTextSharp.text.Document(PageSize.A4);
                PdfWriter.GetInstance(documento, new FileStream(saveFileDialog.FileName, FileMode.Create));
                documento.Open();

                // ❗ Especificar el namespace correcto para evitar conflicto
                iTextSharp.text.Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                iTextSharp.text.Font fuenteFecha = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
                iTextSharp.text.Font fuenteTabla = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                iTextSharp.text.Font fuenteBalance = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.RED);

                // Título centrado
                Paragraph tituloParrafo = new Paragraph(titulo, fuenteTitulo)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                documento.Add(new Paragraph("\n"));
                documento.Add(new Paragraph("\n"));
                documento.Add(new Paragraph("\n"));
                documento.Add(new Paragraph("\n"));
                documento.Add(new Paragraph("\n"));
                documento.Add(tituloParrafo);

                // Fecha alineada a la derecha
                string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                Paragraph fechaParrafo = new Paragraph("Fecha: " + fechaActual, fuenteFecha)
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                documento.Add(fechaParrafo);

                // Espacio antes de la tabla
                documento.Add(new Paragraph("\n"));

                // Crear la tabla con el número de columnas del DataGridView
                PdfPTable tabla = new PdfPTable(dgv.Columns.Count);
                tabla.WidthPercentage = 100;

                // Agregar encabezados con color de fondo
                foreach (DataGridViewColumn columna in dgv.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(columna.HeaderText, fuenteTabla))
                    {
                        BackgroundColor = BaseColor.ORANGE,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    tabla.AddCell(celda);
                }

                // Agregar filas con datos
                foreach (DataGridViewRow fila in dgv.Rows)
                {
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        tabla.AddCell(new Phrase(celda.Value?.ToString() ?? "", fuenteTabla));
                    }
                }

                documento.Add(tabla);

                // Espacio antes del balance
                documento.Add(new Paragraph("\n"));

                // Balance de utilidad centrado y en negrita
                Paragraph balanceParrafo = new Paragraph(ganancia, fuenteBalance)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                documento.Add(balanceParrafo);

                documento.Close();

                MessageBox.Show("Exportado con éxito.", "Exportar a PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ExportarAPDF(DGVinforme, "Informe de utilidades", lblVenta.Text);
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

        // btn PDF
        private void BtnPDF_MouseEnter(object sender, EventArgs e)
        {
            btnPDF.Size = new Size(166, 73);
            btnPDF.FillColor = Color.Black;
            btnPDF.ForeColor = Color.Gold;
        }

        private void BtnPDF_MouseLeave(object sender, EventArgs e)
        {
            btnPDF.Size = new Size(160, 71);
            btnPDF.FillColor = Color.Gold;
            btnPDF.ForeColor = Color.Black;
        }

        //********************************************************************************************

        // Transiciones 

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
