using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Agent
{
    public partial class Setting : Form
    {
        int status = 0;
        string server = ConfigurationManager.ConnectionStrings["server"].ConnectionString;
        string user = ConfigurationManager.ConnectionStrings["user"].ConnectionString;
        string password = ConfigurationManager.ConnectionStrings["pwd"].ConnectionString;
        string db = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
        public Setting()
        {
            InitializeComponent();
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
        private void button1_Click(object sender, EventArgs e)
        {
            port.move = 1;
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            ConnectionStringsSection connSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connSection.ConnectionStrings["server"].ConnectionString = textBox1.Text;
            connSection.ConnectionStrings["user"].ConnectionString = textBox3.Text;
            connSection.ConnectionStrings["pwd"].ConnectionString = textBox4.Text;
            connSection.ConnectionStrings["database"].ConnectionString = textBox2.Text;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            server = ConfigurationManager.ConnectionStrings["server"].ConnectionString;
            user = ConfigurationManager.ConnectionStrings["user"].ConnectionString;
            password = ConfigurationManager.ConnectionStrings["pwd"].ConnectionString;
            db = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            //UpdateAppSettings("server", $"{textBox1.Text}");
            //UpdateAppSettings("user", $"{textBox3.Text}");
            //UpdateAppSettings("pwd", $"{textBox4.Text}");
            //UpdateAppSettings("database", $"{textBox2.Text}");
            
            try
            {
                string cons = $"server={server};user={user};pwd={password};database={db};";
                MySqlConnection connection = new MySqlConnection(cons);
                connection.Open();
                connection.Close();
                MessageBox.Show("Настройка подключения прошла успешано. Соединение установлено.","Уведомление",MessageBoxButtons.OK,MessageBoxIcon.Information);
                status = 0;
                includeAdmin includeAdmin = new includeAdmin();
                includeAdmin.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                status = 1;
                MessageBox.Show("Соединение не установнело. Попробуйте снова","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            textBox1.Text = server;
            textBox3.Text = user;
            textBox4.Text = password;
            textBox2.Text = db;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (status == 1)
            {
                includeAdmin includeAdmin = new includeAdmin(1);
                includeAdmin.Show();
                this.Close();
            }
            else
            {
                includeAdmin includeAdmin = new includeAdmin();
                includeAdmin.Show();
                this.Close();
            }

        }

        private void Setting_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }
    }
}
