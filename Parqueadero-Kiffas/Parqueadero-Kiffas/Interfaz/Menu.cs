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

namespace Parqueadero_Kiffas.Interfaz
{
    public partial class Menu : Form
    {
        public Menu()
        {

            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, 100);
            this.MaximizeBox = false;

            btnIV.MouseEnter += BtnIV_MouseEnter;
            btnIV.MouseLeave += BtnIV_MouseLeave;

            btnCS.MouseEnter += BtnCS_MouseEnter;
            btnCS.MouseLeave += BtnCS_MouseLeave;

            btnM.MouseEnter += BtnM_MouseEnter;
            btnM.MouseLeave += BtnM_MouseLeave;

            btnSL.MouseEnter += BtnSL_MouseEnter;
            btnSL.MouseLeave += BtnSL_MouseLeave;

            btnCT.MouseEnter += BtnCT_MouseEnter;
            btnCT.MouseLeave += BtnCT_MouseLeave;

            btnI.MouseEnter += BtnI_MouseEnter;
            btnI.MouseLeave += BtnI_MouseLeave;
        }

        //****************************************************************************************

        // Logica

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void btnIV_Click(object sender, EventArgs e)
        {
            IrRV();
        }

        private void btnCS_Click(object sender, EventArgs e)
        {
            IrCS();
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            IrMensualidades();
        }

        private void btnSL_Click(object sender, EventArgs e)
        {
            IrSL();
        }

        private void btnCT_Click_1(object sender, EventArgs e)
        {
            IrCT();
        }

        private void btnI_Click(object sender, EventArgs e)
        {
            IrInformes();
        }

        //***************************************************************************************

        // Diseño y animaciones

        // Ingreso de vehiculos
        private void BtnIV_MouseEnter(object sender, EventArgs e)
        {
            btnIV.Size = new Size(335, 47);
            btnIV.FillColor = Color.Black;
            btnIV.ForeColor = Color.Gold;
        }

        private void BtnIV_MouseLeave(object sender, EventArgs e)
        {
            btnIV.Size = new Size(330, 45);
            btnIV.FillColor = Color.Gold;
            btnIV.ForeColor = Color.Black;
        }

        // Control de salidas
        private void BtnCS_MouseEnter(object sender, EventArgs e)
        {
            btnCS.Size = new Size(335, 47);
            btnCS.FillColor = Color.Black;
            btnCS.ForeColor = Color.Gold;
        }

        private void BtnCS_MouseLeave(object sender, EventArgs e)
        {
            btnCS.Size = new Size(330, 45);
            btnCS.FillColor = Color.Gold;
            btnCS.ForeColor = Color.Black;
        }

        // Mensualidades
        private void BtnM_MouseEnter(object sender, EventArgs e)
        {
            btnM.Size = new Size(335, 47);
            btnM.FillColor = Color.Black;
            btnM.ForeColor = Color.Gold;
        }

        private void BtnM_MouseLeave(object sender, EventArgs e)
        {
            btnM.Size = new Size(330, 45);
            btnM.FillColor = Color.Gold;
            btnM.ForeColor = Color.Black;
        }

        // Lavadas
        private void BtnSL_MouseEnter(object sender, EventArgs e)
        {
            btnSL.Size = new Size(335, 47);
            btnSL.FillColor = Color.Black;
            btnSL.ForeColor = Color.Gold;
        }

        private void BtnSL_MouseLeave(object sender, EventArgs e)
        {
            btnSL.Size = new Size(330, 45);
            btnSL.FillColor = Color.Gold;
            btnSL.ForeColor = Color.Black;
        }

        // Tarifas
        private void BtnCT_MouseEnter(object sender, EventArgs e)
        {
            btnCT.Size = new Size(164, 63);
            btnCT.FillColor = Color.Gold;
            btnCT.ForeColor = Color.Black;
        }

        private void BtnCT_MouseLeave(object sender, EventArgs e)
        {
            btnCT.Size = new Size(158, 61);
            btnCT.FillColor = Color.Black;
            btnCT.ForeColor = Color.Gold;
        }

        // Informes
        private void BtnI_MouseEnter(object sender, EventArgs e)
        {
            btnI.Size = new Size(335, 47);
            btnI.FillColor = Color.Black;
            btnI.ForeColor = Color.Gold;
        }

        private void BtnI_MouseLeave(object sender, EventArgs e)
        {
            btnI.Size = new Size(330, 45);
            btnI.FillColor = Color.Gold;
            btnI.ForeColor = Color.Black;
        }

        //*******************************************************************************************

        // transiciones

        private void IrRV()
        {
            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);

            this.Hide();

            RegistrarVehiculo RG = new RegistrarVehiculo();
            RG.Show();

            transicion.ShowSync(RG);
        }

        private void IrCS()
        {

            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);


            this.Hide();

            ControlSalidas CS = new ControlSalidas();
            CS.Show();

            transicion.ShowSync(CS);
        }

        private void IrMensualidades()
        {
            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);

            this.Hide();

            Mensualidades M = new Mensualidades();
            M.Show();

            transicion.ShowSync(M);
        }

        private void IrSL()
        {
            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);

            this.Hide();

            Lavadas l= new Lavadas();
            l.Show();

            transicion.ShowSync(l);
        }
        private void IrCT()
        {
            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);

            this.Hide();

            CTarifas tarifa = new CTarifas();
            tarifa.Show();

            transicion.ShowSync(tarifa);
        }

        private void IrInformes()
        {

            var transicion = new Guna2Transition();
            transicion.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            transicion.ShowSync(this);


            this.Hide();


            Informes infor = new Informes();
            infor.Show();

            transicion.ShowSync(infor);
        }
    }
}
