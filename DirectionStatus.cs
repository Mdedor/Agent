using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Agent
{
    public partial class DirectionStatus : Form
    {
        int dirs;
        int applicant;
        int vacancy;
        int emp;
        string e = null;
        public DirectionStatus(int dir)
        {
            dirs = dir;
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void enter_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(Connection.con);
            connection.Open();
            string find = $"SELECT * FROM direction WHERE id = {dirs};";
            MySqlCommand com = new MySqlCommand(find, connection);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                applicant = Convert.ToInt32(reader[1].ToString());
                vacancy = Convert.ToInt32(reader[2].ToString());
                emp = Convert.ToInt32(reader[3].ToString());
                
            }
            connection.Close();
            
            port.status = comboBox1.SelectedItem.ToString();
            func.direction($@"UPDATE direction 
                           SET direction_status = '{port.status}'
                           WHERE id = {dirs};");
            if(port.status == "Принято")
            {
                func.direction($@"UPDATE applicant 
                           SET applicant_delete_status = 3
                           WHERE applicant_id = '{applicant}';");
                func.direction($@"UPDATE vacancy 
                           SET vacancy_delete_status = 2
                           WHERE id = '{vacancy}';");
            }else if(port.status == "Ожидание")
            {
                func.direction($@"UPDATE applicant 
                           SET applicant_delete_status = '4'
                           WHERE applicant_id = '{applicant}';");
                func.direction($@"UPDATE vacancy 
                           SET vacancy_delete_status = '4'
                           WHERE id = '{vacancy}';");
            }
            else if(port.status == "Отклонено")
            {
                func.direction($@"UPDATE applicant 
                           SET applicant_delete_status = '4'
                           WHERE applicant_id = '{applicant}';");
                func.direction($@"UPDATE vacancy 
                           SET vacancy_delete_status = '4'
                           WHERE id = '{vacancy}';");
            } 
            this.Close();
        }

        private void DirectionStatus_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Ожидание");
            comboBox1.Items.Add("Принято");
            comboBox1.Items.Add("Отклонено");
            if (port.haveStatus == "Ожидание")
                comboBox1.SelectedIndex = 0;
            else if (port.haveStatus == "Принято")
                comboBox1.SelectedIndex = 1;
            else if (port.haveStatus == "Отклонено")
                comboBox1.SelectedIndex = 2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            enter.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
