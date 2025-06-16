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
using Application = System.Windows.Forms.Application;
using MySqlX.XDevAPI.Relational;

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
        int timr;
        int error = 0;
        public includeAdmin(int err = 0)
        {
             error = err;
            InitializeComponent();

        }

        private void includeAdmin_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }
        void load()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(Connection.connect());
                connection.Open();
                string find = "USE agent;SHOW tables;";
                MySqlCommand com = new MySqlCommand(find, connection);
                 com.ExecuteReader();
                connection.Close();
                button4.Enabled = true;
                button5.Enabled = true;
                button2.Enabled = true;
            }
            catch
            {
                button4.Enabled = false;
                button5.Enabled = false;
                button2.Enabled = false;
            }
            
        }
        private void includeAdmin_Load(object sender, EventArgs e)
        {
            if (error == 0)
            {
                load();
            }
            else
            {
                button4.Enabled = false;
                button5.Enabled = false;
                button2.Enabled = false;
            }
            
           
            currentValue = Convert.ToInt32(ConfigurationManager.AppSettings["time"].ToString());


            timr = currentValue ;
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
           
            
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Создать резервную копию?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );
            string db = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(Connection.connect()))
                    {
                        string exePath = "";
                        string baseDir = "";
                        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".sql";
                        string docPath = "";
                        
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                            cmd.Connection = conn;
                            conn.Open();
                            try
                            {
                                exePath = Assembly.GetEntryAssembly().Location;
                                baseDir = Path.GetDirectoryName(exePath);

                                docPath = Path.Combine(baseDir, "backup", "Ручное резервное копирование", "Backup_" + fileName);
                                mb.ExportToFile(docPath);
                            }
                            catch
                            {
                                exePath = Assembly.GetEntryAssembly().Location;
                                baseDir = Path.GetDirectoryName(exePath);
                                baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
                                docPath = Path.Combine(baseDir, "backup", "Ручное резервное копирование", "Backup_" + fileName);
                                mb.ExportToFile(docPath);
                            }
                                
                                conn.Close();
                            }
                        }

                    
                        MessageBox.Show($"Файл сохранен по пути {docPath}", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                    // Проверка расширения файла
                    if (Path.GetExtension(selectedFile).ToLower() != ".sql")
                    {
                        MessageBox.Show("Пожалуйста, выберите файл с расширением .sql", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        string readText = File.ReadAllText(selectedFile);

                        using (MySqlConnection con = new MySqlConnection(Connection.connect()))
                        {
                            con.Open();

                            using (MySqlCommand cmd = new MySqlCommand(readText, con))
                            {
                                 int results = cmd.ExecuteNonQuery();

                                if (results > 0)
                                {
                                    MessageBox.Show("Данные успешно восстановлены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Выполнение SQL прошло без изменений", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Данные не востановлен", "Ошибка восстановления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Данные не востановлен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Востановить струтуру бд?. После выполнения будут удалены все данные!!!. Сделайте перед эти резервную копию.",
                "Предупреждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
            if (result == DialogResult.Yes)
            {
                try
                {
                    string pathError = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string filePath = Path.Combine(pathError, "copy", "structure.sql");

                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show($"Файл не найден:\n{filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string cons = $"server={server}; uid={user}; pwd={pwd}";

                    string readText = File.ReadAllText(filePath);

                    using (MySqlConnection con = new MySqlConnection(cons))
                    {
                        con.Open();

                        using (MySqlCommand cmd = new MySqlCommand(readText, con))
                        {
                            int resulst = cmd.ExecuteNonQuery();

                            if (resulst > 0)
                            {
                                MessageBox.Show("Структура успешно восстановлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Выполнение прошло без изменений", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    load();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Ошибка MySQL:\n{ex.Message}", "Ошибка восстановления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exit_Click_2(object sender, EventArgs e)
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

        private void includeAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }

        private void textBox2_TextChanged_2(object sender, EventArgs e)
        {
            button3.Enabled = true;
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            double time = Convert.ToInt32(textBox2.Text);

            UpdateAppSettings("time", $"{time}");

            currentValue = Convert.ToInt32(ConfigurationManager.AppSettings["time"].ToString());
            
            MessageBox.Show($"Время бездействия изменено","Уведомление",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.Show();
            this.Close();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
