using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
namespace Agent
{

    public partial class MenuAdmin : Form
    {

        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {
            
        }
        
        private void buttonAplicant_Click(object sender, EventArgs e)
        {
            port.move = 1;
            AdminS adminS = new AdminS();
            adminS.Show();
            this.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Auntification auntification = new Auntification();
            auntification.Show();
            this.Close();
        }

        private void buttonCompany_Click(object sender, EventArgs e)
        {
            port.move = 1;
            AdminC adminC = new AdminC();
            adminC.Show();
            this.Close();
        }

        private void buttonEmployee_Click(object sender, EventArgs e)
        {
            port.move = 1;
            AdminE adminE = new AdminE();
            adminE.Show();
            this.Close();
        }

        private void MenuAdmin_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }
    }
}
