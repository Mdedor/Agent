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
            //StartTimer();
        }
    //    private async void StartTimer()
    //    {
    //        int currentValue = Convert.ToInt32(ConfigurationManager.AppSettings["minut"].ToString());
    //        int currentValue2 = Convert.ToInt32(ConfigurationManager.AppSettings["secund"].ToString());
    //        TimeSpan ts = new TimeSpan(0, currentValue, currentValue2);
    //        while (ts > TimeSpan.Zero)
    //        {
    //            await Task.Delay(1000);
    //             ts -= TimeSpan.FromSeconds(1);

    //            if (port.move == 1)
    //            {
    //                ts = new TimeSpan(0, 0, 40);
    //                port.move = 0;
    //            }
    //        }
    //        Auntification auntification = new Auntification();
    //        CloseAllAndOpenNew(auntification);
           
        
        

    //}
        public static void CloseAllAndOpenNew(Form newForm)
        {
            var forms = Application.OpenForms.Cast<Form>().ToList();
            foreach (Form form in forms)
            {
                newForm = forms[0];
                if (form == newForm)
                    continue;
                else
                    form.Close();
            }

            // Открыть новую форму
            newForm.Show();
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
            func.FormPaint(this);
        }
    }
}
