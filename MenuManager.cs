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

       

        private void MenuManager_Load(object sender, EventArgs e)
        {
            
        }

        private void MenuManager_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }

        private void MenuManager_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            port.empIds = 0;
            Auntification auntification = new Auntification();
            //auntification.Show();
            //this.Close();

            var forms = Application.OpenForms.Cast<Form>().ToList();
            foreach (Form form in forms)
            {

                if (form.Name == auntification.Name && form.Text == auntification.Text)
                {
                    form.Show();

                    continue;

                }

                else
                {
                    form.Close();
                }

            }
        }

        private void buttonAplicant_Click_1(object sender, EventArgs e)
        {
            port.move = 1;
            AdminS adminS = new AdminS();
            adminS.Show();
            this.Close();
        }

        private void buttonCompany_Click_1(object sender, EventArgs e)
        {
            port.move = 1;
            AdminC adminC = new AdminC();
            adminC.Show();
            this.Close();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            seeDirection seeDirection = new seeDirection();
            seeDirection.Show();
            this.Close();
        }
    }
}
