using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace Agent
{
    public partial class includeAdmin : Form
    {
        static string server = ConfigurationManager.ConnectionStrings["server"].ConnectionString.ToString();
        static string db = ConfigurationManager.ConnectionStrings["database"].ConnectionString.ToString();
        static string user = ConfigurationManager.ConnectionStrings["user"].ConnectionString.ToString();
        static string pwd = ConfigurationManager.ConnectionStrings["pwd"].ConnectionString.ToString();
        string filePath;
        public includeAdmin()
        {
            InitializeComponent();
        }

        private void includeAdmin_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
        void load()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(Connection.connect());
                connection.Open();

                string find = "USE agent;SHOW tables;";
                MySqlCommand com = new MySqlCommand(find, connection);
                MySqlDataReader reader = com.ExecuteReader();
                connection.Close();
                button4.Enabled = true;
            }
            catch
            {
                button4.Enabled = false;
            }
            
        }
        private void includeAdmin_Load(object sender, EventArgs e)
        {
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataImport dataImport = new dataImport();
            dataImport.Show();
            this.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Auntification auntification = new Auntification();
            auntification.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            filePath = "copy\\Резервная_копия (1).sql";
            string cons = $"server={server}; uid={user}; pwd={pwd}";
            string readText = File.ReadAllText(filePath);
            MySqlConnection con = new MySqlConnection(cons);
            con.Open();

            MySqlCommand cmd = new MySqlCommand(readText, con);
            int result = cmd.ExecuteNonQuery();

            con.Close();
            load();
        }
    }
}
