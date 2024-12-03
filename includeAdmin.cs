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
    public partial class includeAdmin : Form
    {
        public includeAdmin()
        {
            InitializeComponent();
        }

        private void includeAdmin_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }

        private void includeAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
