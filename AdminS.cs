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
    public partial class AdminS : Form
    {
        int empIds;
        public AdminS()
        {
            InitializeComponent();
        }

        private void alminS_Load(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            port.move = 1;
            MenuManager menuManager = new MenuManager();
            menuManager.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            port.move = 1;
            AddS addS = new AddS(0);
            addS.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            port.move = 1;
            SeeS seeS = new SeeS();
            seeS.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            port.move = 1;
            SeeS seeS = new SeeS(true);
            seeS.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            port.move = 1;
            SeeResume see = new SeeResume();
            see.Show();
            this.Close();
        }

        private void AdminS_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
