using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void AdminE_Load(object sender, EventArgs e)
        {
            
        }
        
        private void AdminE_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            port.move = 1;
            AddE add = new AddE(0);
            add.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            port.move = 1;
            SeeEmp seeEmp = new SeeEmp();
            seeEmp.Show();
            this.Close();
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            port.move = 1;
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

        private void AdminE_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }
    }
}
