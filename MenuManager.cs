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
    public partial class MenuManager : Form
    {

        public MenuManager()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Auntification auntification = new Auntification();
            auntification.Show();
            this.Close();
        }

        private void buttonAplicant_Click(object sender, EventArgs e)
        {
            SeeVacancy seeVacancy = new SeeVacancy();
            seeVacancy.Show();
            this.Close();
        }

        private void MenuManager_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            seeDirection seeDirection = new seeDirection();
            seeDirection.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeeResume seeResume = new SeeResume();
            seeResume.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MenuManager_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
