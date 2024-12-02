using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agent
{
    public partial class AdminE : Form
    {
        int empIds;
        public AdminE()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            MenuAdmin menuAdmin = new MenuAdmin();
            menuAdmin.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddE add = new AddE(0);
            add.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeeEmp seeEmp = new SeeEmp();
            seeEmp.Show();
            this.Close();
        }

        private void AdminE_Load(object sender, EventArgs e)
        {

        }

        private void AdminE_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
