using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Parqueadero_Kiffas.Interfaz;

namespace Parqueadero_Kiffas
{
    public partial class Bienvenida : Form
    {
        public Bienvenida()
        {
            InitializeComponent();
            btn.MouseEnter += Button1_MouseEnter;
            btn.MouseLeave += Button1_MouseLeave;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            btn.Size = new Size(288, 56);
            btn.FillColor = Color.Gold;
            btn.ForeColor = Color.Black;
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            btn.Size = new Size(282, 54);
            btn.FillColor = Color.Black;
            btn.ForeColor = Color.White;
        }

        private void btn_Click_1(object sender, EventArgs e)
        {
            Transicion();
            /*if (Conexion.ProbarConexion() == true)
            {
                MessageBox.Show("Hay conexion", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay conexion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void Transicion()
        {

            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);


            this.Hide();

            Interfaz.Menu menu = new Interfaz.Menu();
            menu.Show();

            transicion.ShowSync(menu);
        }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Bienvenida_Load(object sender, EventArgs e)
        {

        }
    }
}
