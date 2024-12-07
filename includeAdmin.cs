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
        int currentValue;
        int currentValue2;
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
            currentValue = Convert.ToInt32(ConfigurationManager.AppSettings["minut"].ToString());
            currentValue2 = Convert.ToInt32(ConfigurationManager.AppSettings["secund"].ToString());

            int timr = currentValue * 60 + currentValue2;
            textBox2.Text = timr.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataImport dataImport = new dataImport();
            dataImport.Show();
            this.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Востановить бд?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
            if (result == DialogResult.Yes)
            {
                filePath = "copy\\Резервная_копия (1).sql";
                string cons = $"server={server}; uid={user}; pwd={pwd}";
                string readText = File.ReadAllText(filePath);
                MySqlConnection con = new MySqlConnection(cons);
                con.Open();

                MySqlCommand cmd = new MySqlCommand(readText, con);
                int resulst = cmd.ExecuteNonQuery();

                con.Close();
                load();
            }

               
        
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
                button3.Enabled = true;
        }
        public static void UpdateAppSettings(string key, string value)
        {
            // Получаем конфигурацию приложения
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Проверяем, существует ли ключ
            if (config.AppSettings.Settings[key] != null)
            {
                // Если существует, обновляем значение
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                // Если не существует, добавляем новый ключ
                config.AppSettings.Settings.Add(key, value);
            }

            // Сохраняем изменения
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            double time = Convert.ToInt32(textBox2.Text);
            double min = Math.Round(time/60);

            double second = time - min * 60;
            UpdateAppSettings("minut", $"{min}");
            UpdateAppSettings("secund", $"{second}");

            MessageBox.Show("Время изменено");
            
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            Auntification auntification = new Auntification();
            auntification.Show();
            this.Close();
        }
    }
}
