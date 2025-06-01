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
    public partial class AdminC : Form
    {
        public AdminC()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            port.move = 1;
            MenuManager menuManager = new MenuManager();
            menuManager.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            port.move = 1;
            SeeCompany seeCompany = new SeeCompany();
            seeCompany.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            port.move = 1;
            SeeCompany seeCompany = new SeeCompany(1);
            seeCompany.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            port.move = 1;
            AddC addC = new AddC();
            addC.Show();
            this.Close();
        }

        private void AdminC_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            port.move = 1;
            SeeVacancyNew seeVacancyNew = new SeeVacancyNew();
            seeVacancyNew.Show();
            this.Close();
        }

        private void AdminC_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
