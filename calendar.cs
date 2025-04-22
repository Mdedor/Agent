using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VBIDE;

namespace Agent
{
    public partial class calendar : Form
    {
        public calendar()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void calendar_Load(object sender, EventArgs e)
        {
            int clock = 8;
            int q = clock;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            for(int i = 0; i < 7; i++)
            {

                for(int j = 0; j < 8; j++)
                {
                    Label labels = new Label
                    {
                        Text = $"{clock}:00-{q = clock + 1}:00",
                        BackColor = Color.Green,
                        Size = new System.Drawing.Size(70, 30),
                        Name = $"label_{i}_{j}"
                    };
                    
                    labels.MouseDown += ( senders, ee) =>
                    {

                        MessageBox.Show($"{labels.Name}");
                    };
                    clock += j;
                    tableLayoutPanel1.Controls.Add(labels,i,j);
                    clock = 8;
                    
                }
                
            }
        }
        

            private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
