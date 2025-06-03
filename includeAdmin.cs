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
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Word;

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
            //currentValue = Convert.ToInt32(ConfigurationManager.AppSettings["minut"].ToString());
            //currentValue2 = Convert.ToInt32(ConfigurationManager.AppSettings["secund"].ToString());

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
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Connection.connect()))
            {
                conn.Open();

                // Получаем список таблиц
                List<string> tables = new List<string>();
                using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES", conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }

                // Получаем путь к исполняемому файлу
                string exePath = Assembly.GetEntryAssembly().Location;
                // Переходим на несколько уровней вверх (например, из binDebug\netX.Y в корень проекта)
                string baseDir = Path.GetDirectoryName(exePath); // binDebug\netX.Y
                baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..")); // Поднимаемся на 3 уровня вверх
                                                                             // Добавляем относительный путь к документу
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".sql";
                string docPath = Path.Combine(baseDir, "backup", "Ручное резервное копирование", "Отчет_" +fileName );
                // Создаем SQL-дамп
                using (StreamWriter writer = new StreamWriter(docPath))
                {
                    writer.WriteLine($"CREATE DATABASE  IF NOT EXISTS `agent`;");
                    writer.WriteLine($"USE `agent`;");
                    writer.WriteLine(@"/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;");
                    foreach (string table in tables)
                    {
                       

                        writer.WriteLine($"DROP TABLE IF EXISTS `{table}`;");
                        
                        

                        //Получаем структуру таблицы
                        using (MySqlCommand cmd = new MySqlCommand($"SHOW CREATE TABLE `{table}`", conn))
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                writer.WriteLine(reader.GetString(1) + ";");
                            }
                        }

                        // Получаем данные таблицы
                        writer.WriteLine($"\n-- Data for table `{table}`\n");

                        using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `{table}`", conn))
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StringBuilder insert = new StringBuilder($"INSERT INTO `{table}` VALUES (");
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (i > 0) insert.Append(", ");

                                    if (reader.IsDBNull(i))
                                    {
                                        insert.Append("NULL");
                                    }
                                    else
                                    {
                                        Type fieldType = reader.GetFieldType(i);
                                        if (fieldType == typeof(DateTime))
                                        {
                                            DateTime dateValue = reader.GetDateTime(i);
                                            insert.Append($"'{dateValue.ToString("yyyy-MM-dd")}'");
                                        }
                                        else
                                        {
                                            insert.Append($"'{MySqlHelper.EscapeString(reader.GetString(i))}'");
                                        }
                                    }
                                }

                                insert.Append(");");
                                writer.WriteLine(insert.ToString());
                            }
                        }
                    }
                    MessageBox.Show($"Файл сохранен по пути {docPath}", "Уведобление",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Восстановить данные из резервной копии БД?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Yes)
            {
                // Получаем путь к исполняемому файлу
                string exePath = Assembly.GetEntryAssembly().Location;
                // Поднимаемся на несколько уровней вверх (например, из binDebug\netX.Y в корень проекта)
                string baseDir = Path.GetDirectoryName(exePath); // binDebug\netX.Y
                baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..")); // Поднимаемся на 2 уровня вверх
                                                                            // Добавляем относительный путь к папке с резервными копиями
                string backupDir = Path.Combine(baseDir, "backup");

                // Создаем и настраиваем диалоговое окно выбора папки
                //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                //folderBrowserDialog.Description = "Выберите папку с резервными копиями";
                //folderBrowserDialog.SelectedPath = backupDir; // Начальная директория (необязательно)

                // Если пользователь выбрал папку
                

                // Теперь настраиваем OpenFileDialog, чтобы он открывался в выбранной папке
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите файл резервной копии";
                openFileDialog.InitialDirectory = backupDir; // Устанавливаем выбранную папку

                // Показываем диалог выбора файла
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    MessageBox.Show($"Выбран файл: {selectedFile}");
                }
                
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Востановить струтуру бд?",
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

        private void exit_Click_2(object sender, EventArgs e)
        {
            Auntification auntification = new Auntification();
            auntification.Show();
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            dataImport dataImport = new dataImport();
            dataImport.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataImport dataImport = new dataImport(1);
            dataImport.Show();
            this.Close();
        }
    }
}
