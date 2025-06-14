using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Brush = System.Drawing.Brush;
using Pen = System.Drawing.Pen;

namespace Agent
{
    public partial class Auntification : Form
    {
        string login;
        string password;
        string loginBD;
        string passwordBD;
        int empId;
        bool nextCaptcha;
        int pbWidth;
        int pbHeight;
        int pictureX;
        int buttonX;
        int countEr;
        int status = 1;
        Point Point;
        PictureBox pb = new PictureBox();
        Button updateCaptcha = new Button();
        TextBox textBoxCaptcha = new TextBox();
        Label labelPods = new Label();
        Random Random = new Random();
        string capcha;
        public Auntification()
        {
            InitializeComponent();
        }
        

        // ДОДЕЛАТЬ ТЗ
        // сделать проверку на подключение и проверку на наличиие базы данных !!
        // Востановление базы данных автоматическое и ручное
        // добавить создание документа при добавлении компании
        // документ по прибыли
        // изменить дизайн
        // протестировать каждый комбо бокс
        private void Auntification_Load(object sender, EventArgs e)
        {
            Point = this.Location;
            //try
            //{
            //    string cons = $"server={server};user={user};pwd={passworddd};database={db};";
            //    MySqlConnection connection = new MySqlConnection(cons);
            //    connection.Open();
            //    connection.Close();

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Соединение c базой данных не уставнолено. Вызовите локального администратора для настройки подключения к бд.");
            //    status = 0;
            //    return;
            //}
            //textBoxLogin.Text = "1";
            //textBoxPwd.Text = "1";
            //updateCaptcha.Click += buttonClicl;
            
        }
        void buckup()
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


                string exePath = "";

                string baseDir = "";
                                                                           
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".sql";
                string docPath = "";
                StreamWriter writer;
                try
                {
                    try
                {
                    exePath = Assembly.GetEntryAssembly().Location;
                    baseDir = Path.GetDirectoryName(exePath);

                    docPath = Path.Combine(baseDir, "backup", "Автоматическое резервное копирование", "Backup_auto_" + fileName);
                    writer = new StreamWriter(docPath);
                }
                catch
                {
                    exePath = Assembly.GetEntryAssembly().Location;
                    baseDir = Path.GetDirectoryName(exePath);
                    baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
                    docPath = Path.Combine(baseDir, "backup", "Автоматическое резервное копирование", "Backup_auto_" + fileName);
                    writer = new StreamWriter(docPath);
                }
                string db = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
                // Создаем SQL-дамп
                
                    writer.WriteLine($"CREATE DATABASE  IF NOT EXISTS `{db}`;");
                    writer.WriteLine($"USE `{db}`;");
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
                }
                catch 
                {
                    MessageBox.Show(
                 "Авторматическое создание резевной копии не удалось",
                 "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
                }
                    
                   // MessageBox.Show($"Файл сохранен по пути {docPath}", "Уведобление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                
            }
        }
        void deleteBuckup()
        {

            string exePath = Assembly.GetEntryAssembly().Location;
            // Переходим на несколько уровней вверх (например, из binDebug\netX.Y в корень проекта)
            string baseDir = Path.GetDirectoryName(exePath); // binDebug\netX.Y
            baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..")); // Поднимаемся на 3 уровня вверх
                                                                         // Добавляем относительный путь к документу

            string docPath = Path.Combine(baseDir, "backup", "Автоматическое резервное копирование");
            try
            {
                DirectoryInfo directory = new DirectoryInfo(docPath);

                // Получаем все файлы в папке, сортируем по дате создания (последние сначала)
                FileInfo[] files = directory.GetFiles()
                    .OrderByDescending(f => f.CreationTime)
                    .ToArray();

                int totalFiles = files.Length;


                if (totalFiles > 10)
                {
                    

                    // Пропускаем первые 10 файлов (самые новые), остальные удаляем
                    foreach (FileInfo file in files.Skip(10))
                    {
                        try
                        {

                            file.Delete();
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show($"Ошибка при удалении файла {file.Name}: {ex.Message}");
                        }
                    }

                }
                else
                {
                    //MessageBox.Show("Файлов меньше 10, удаление не требуется.");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void buttonClicl(object sender, EventArgs eventArgs)
        {
            edit();
        }
        private void SetControlsEnabled(Control control, bool enabled)
        {
            foreach (Control c in control.Controls)
            {
                c.Enabled = enabled;
                // Если есть вложенные элементы управления, рекурсивно отключаем/включаем их
                if (c.HasChildren)
                {
                    SetControlsEnabled(c, enabled);
                }
            }
        }
        void sleep()
        {
            SetControlsEnabled(this, false);

            // Ждем 10 секунд
            System.Threading.Thread.Sleep(10000);

            // Включаем все элементы управления обратно
            SetControlsEnabled(this, true);
        }
        void auntification()
        {
            string post = func.search($"SELECT employe_post FROM employe WHERE employe_login = '{login}'");
            empId = Convert.ToInt32(func.search($"SELECT id FROM employe WHERE employe_login = '{login}'"));
            //var formsы = Application.OpenForms.Cast<Form>().ToList();
            if (port.empIds == 0)
            {
                
                port.empIds = empId;
                if (post == "1")
                {
                    AdminE adminE = new AdminE();
                    adminE.Show();
                    this.Hide();
                }
                else if (post == "2")
                {
                    MenuManager manager = new MenuManager();
                    manager.Show();
                    this.Hide();
                }
                else if (post == "3")
                {
                    SeeVacancy seeVacancy = new SeeVacancy();
                    seeVacancy.Show();
                    this.Hide();
                }
                nextCaptcha = false;
                this.Size = new Size(346, 311);
            }
            else
            {
                if (empId == port.empIds) 
                {
                    var forms = Application.OpenForms.Cast<Form>().ToList();
                    foreach (Form form in forms)
                    {

                        if (form.Name == port.block.Name && form.Text == port.block.Text)
                        {
                            form.Show();
                            continue;

                        }

                        
                    }
                    this.Size = new Size(346, 311);
                    nextCaptcha = false;
                    this.Hide();
                }
                else
                {
                    port.empIds = empId;
                    if (post == "1")
                    {
                        AdminE adminE = new AdminE();
                        adminE.Show();
                        this.Hide();
                    }
                    else if (post == "2")
                    {
                        MenuManager manager = new MenuManager();
                        manager.Show();
                        this.Hide();
                    }
                    else if (post == "3")
                    {
                        SeeVacancy seeVacancy = new SeeVacancy();
                        seeVacancy.Show();
                        this.Hide();
                    }
                    this.Size = new Size(346, 311);
                    nextCaptcha = false;
                }
            }

            
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            func.StartTimer();
            port.move = 1;
            string loginAdmin = ConfigurationManager.AppSettings["loginAdmin"].ToString();
            string pwdAdmin = ConfigurationManager.AppSettings["paswordAdmin"].ToString();
            if(string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(textBoxPwd.Text))
            {
                string message = string.IsNullOrEmpty(textBoxLogin.Text) ? "Введите логин" : "Введите пароль";
                MessageBox.Show(message, "Педупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                return; 
            }//добавить сообщение о незаполненности capcha
            
                login = textBoxLogin.Text;
                password = textBoxPwd.Text;
            if (status == 1)
            {
                if (!nextCaptcha)
                {
                    if (login == loginAdmin && password == pwdAdmin)
                    {
                        includeAdmin includeAdmin = new includeAdmin();
                        includeAdmin.Show();
                        this.Hide();
                    }
                    else
                    {
                        passwordBD = func.search($"SELECT employe_pwd FROM employe WHERE employe_login = '{login}'");
                        if (BCrypt.Net.BCrypt.Verify(password, passwordBD))
                        {
                            auntification();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Авторизация не пройдена. Ошибка в логине или пароле.",
                                "Педупреждение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning

                                );
                            textBoxLogin.Text = "";
                            textBoxPwd.Text = "";
                            if (countEr >= 2)
                            {
                                func.FormPaint(this, Color.White);
                                
                                nextCaptcha = true;
                                pbWidth = textBoxLogin.Width;
                                pbHeight = (textBoxPwd.Location.Y - textBoxLogin.Location.Y) + textBoxPwd.Height;
                                pb.Size = new Size(pbWidth, pbHeight);
                                updateCaptcha.Size = new Size(enter.Width, enter.Height);
                                textBoxCaptcha.Size = new Size(pbWidth, pbHeight);
                                pictureX = (this.Width / 2) - (((Point)pb.Size).X / 2) + this.Width;
                                buttonX = (this.Width / 2) - (((Point)updateCaptcha.Size).X / 2) + this.Width;
                                int width = this.Width;
                                this.Width += this.Width;
                                pb.Location = new Point(pictureX, textBoxLogin.Location.Y);
                                textBoxCaptcha.Location = new Point(pictureX, enter.Location.Y);
                                labelPods.Location = new Point(pictureX, checkBox1.Location.Y + 10);

                                textBoxCaptcha.BackColor = Color.FromArgb(255, 204, 153);
                                textBoxCaptcha.Font = new Font("Comic Sans MS", 18);
                                labelPods.Width = pbWidth;
                                labelPods.Text = "Введите текст, изображенный на картинке";
                                updateCaptcha.Location = new Point(buttonX, exit.Location.Y);
                                updateCaptcha.Text = "Обновить";
                                updateCaptcha.Font = new Font("Comic Sans MS", 18, FontStyle.Bold);
                                updateCaptcha.BackColor = Color.FromArgb(204, 102, 0);
                                updateCaptcha.ForeColor = Color.White;
                                updateCaptcha.FlatStyle = FlatStyle.Flat;
                                updateCaptcha.Cursor = new Cursor(Handle);
                                pb.Name = "pictureBox2";
                                this.Controls.Add(pb);
                                this.Controls.Add(textBoxCaptcha);
                                this.Controls.Add(updateCaptcha);
                                this.Controls.Add(labelPods);
                                edit();
                                func.FormPaint(this, Color.FromArgb(213, 213, 213));
                                this.Location = new Point(Point.X- width/2, Point.Y);
                            }
                            else
                            {
                                countEr++;
                            }
                            
                        }
                    }
                }
                else
                {
                    countEr++;
                    if (textBoxCaptcha.Text != "")
                    {
                        try
                        {
                            passwordBD = func.search($"SELECT employe_pwd FROM employe WHERE employe_login = '{login}'");
                            if (BCrypt.Net.BCrypt.Verify(password, passwordBD) && textBoxCaptcha.Text == capcha)
                            {
                                textBoxCaptcha.Text = "";
                                auntification();
                            }
                            else if (BCrypt.Net.BCrypt.Verify(password, passwordBD) && textBoxCaptcha.Text != capcha)
                            {
                                MessageBox.Show(
                                        "Капча не пройдена. Программа заблокируется на 10 секунд",
                                        "Предуапреждение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning

                                        );
                                edit();
                                textBoxCaptcha.Text = "";
                                textBoxLogin.Text = "";
                                textBoxPwd.Text = "";
                                enter.Enabled = false;
                                sleep();
                                enter.Enabled = true;

                            }
                            else if (login == loginAdmin && password == pwdAdmin && textBoxCaptcha.Text == capcha)

                            {
                                textBoxCaptcha.Text = "";
                                includeAdmin includeAdmin = new includeAdmin();
                                includeAdmin.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Авторизация не пройдена. Программа заблокируется на 10 секунд",
                                    "Предупреждение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                    );
                                edit();
                                textBoxCaptcha.Text = "";
                                textBoxLogin.Text = "";
                                textBoxPwd.Text = "";
                                enter.Enabled = false;
                                sleep();
                                enter.Enabled = true;
                            }
                        }
                        catch
                        {

                        }
                        
                        
                    }
                    else
                    {
                        MessageBox.Show(
                               "Введите капчу",
                               "Предупреждение",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning

                               );
                    }


                }
            }
            else
            {
                if (login == loginAdmin && password == pwdAdmin)
                {
                    includeAdmin includeAdmin = new includeAdmin(1);
                    includeAdmin.Show();
                    this.Hide();
                    textBoxLogin.Text = "";
                    textBoxPwd.Text = "";
                }
                else
                {
                    MessageBox.Show("Нет доступа к базе данных. Вызовите администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            
        }
        void edit()
        {

            capcha = "";
            string alfEng = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890";
            char[] masAlf = alfEng.ToCharArray();
            using (Bitmap bitmap = new Bitmap(((Point)textBoxLogin.Size).X,200))
            {
                // Создаем графику из изображения
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Заполняем фон белым цветом
                    graphics.Clear(Color.FromArgb(255, 224, 192));

                    // Рисуем красный прямоугольник
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        graphics.FillRectangle(brush, pbWidth, pbHeight, pbWidth, pbHeight);
                    }

                    // Рисуем текст
                    using (Font font = new Font("Comic Sans MS", 24))
                    {
                        using (Brush brush = new SolidBrush(Color.Black))
                        {
                            int x = 10;
                            int y = 10;
                            int xx;
                            int yy;
                            Pen pen = new Pen(Color.Gray, 1);
                            Pen pena = new Pen(Color.Gray, 4);
                            for (int i = 0; i < Random.Next(5,6); i++)
                            {
                                char mas = masAlf[Random.Next(0, masAlf.Length - 1)];
                                graphics.DrawString($"{mas}", font, brush, new PointF(x, y));
                                
                                xx = x+5;
                                yy = y+25;
                                x += Random.Next(24,60);
                                y = Random.Next(0, pbHeight - 40);
                                graphics.DrawLine(pena, new PointF(xx, yy), new PointF(x+5, y+25));


                                capcha += mas.ToString();
                            }
                            for (int i = 0; i < pbHeight; i += 5)
                            {
                                graphics.DrawLine(pen, new PointF(0, i), new PointF(pbWidth, i));
                            }
                            for (int i = 0; i < pbWidth; i += 5)
                            {
                                graphics.DrawLine(pen, new PointF(i, 0), new PointF(i, pbHeight));
                            }


                        }
                    }
                }


                pb.Image = (Bitmap)bitmap.Clone();
            }

           
        }
        private void exit_Click(object sender, EventArgs e)
        {
            DialogResult results = MessageBox.Show(
              "Вы действительно хотите выйти?",
              "Подтверждение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information
              );
            if (results == DialogResult.Yes)
            {
                if (status == 1)
                {
                    buckup();
                    deleteBuckup();
                }
                 
                Application.Exit();
            }
            

        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void textBoxPwd_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)       
                textBoxPwd.PasswordChar = '\0';      
            else
                textBoxPwd.PasswordChar = '*';
        }

        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressEn(e);
        }

        private void textBoxPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressEn(e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Auntification_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Auntification_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }

        private void Auntification_Shown(object sender, EventArgs e)
        {
            
            
        }

        private void Auntification_VisibleChanged(object sender, EventArgs e)
        {
            
            if (this.Visible)
            {
                string server = ConfigurationManager.ConnectionStrings["server"].ConnectionString;
                string user = ConfigurationManager.ConnectionStrings["user"].ConnectionString;
                string passworddd = ConfigurationManager.ConnectionStrings["pwd"].ConnectionString;
                string db = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
                try
                {
                    string cons = $"server={server};user={user};pwd={passworddd};database={db};";
                    MySqlConnection connection = new MySqlConnection(cons);
                    connection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SHOW TABLES", connection);
                    mySqlCommand.ExecuteNonQuery();
                    connection.Close();
                    status = 1;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Соединение c базой данных не уставнолено. Вызовите локального администратора для настройки подключения к бд.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = 0;
                    return;
                }
                textBoxLogin.Text = "";
                textBoxPwd.Text = "";
            }
        }
    }
}
