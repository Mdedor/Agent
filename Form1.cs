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

        private void Auntification_Load(object sender, EventArgs e)
        {
            textBoxLogin.Text = "admins";
            textBoxPwd.Text = "admin";
            updateCaptcha.Click += buttonClicl;
            
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
            port.empIds = empId;
            if (post == "1")
            {
                MenuAdmin admin = new MenuAdmin();
                admin.Show();
                this.Hide();
            }
            else if (post == "2")
            {
                MenuManager manager = new MenuManager();
                manager.Show();
                this.Hide();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string loginAdmin = ConfigurationManager.AppSettings["loginAdmin"].ToString();
            string pwdAdmin = ConfigurationManager.AppSettings["paswordAdmin"].ToString();
            if(string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(textBoxPwd.Text))
            {
                string message = string.IsNullOrEmpty(textBoxLogin.Text) ? "Введите логин" : "Введите пароль";
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nextCaptcha = true;
                return; 
            }//добавить сообщение о незаполненности capcha
            
                login = textBoxLogin.Text;
                password = textBoxPwd.Text;
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
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation

                            );
                        textBoxLogin.Text = "";
                        textBoxPwd.Text = "";
                        nextCaptcha = true;
                        pbWidth = textBoxLogin.Width;
                        pbHeight = (textBoxPwd.Location.Y - textBoxLogin.Location.Y) + textBoxPwd.Height;
                        pb.Size = new Size(pbWidth, pbHeight);
                        updateCaptcha.Size = new Size(enter.Width, enter.Height);
                        textBoxCaptcha.Size = new Size(pbWidth, pbHeight);
                        pictureX = (this.Width / 2) - (((Point)pb.Size).X / 2) + this.Width;
                        buttonX = (this.Width / 2) - (((Point)updateCaptcha.Size).X / 2) + this.Width;
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
                    }
                }
            }
            else
            {
                countEr++;
                if (textBoxCaptcha.Text != "")
                {
                    passwordBD = func.search($"SELECT employe_pwd FROM employe WHERE employe_login = '{login}'");
                    if (BCrypt.Net.BCrypt.Verify(password, passwordBD) && textBoxCaptcha.Text == capcha)
                    {
                        auntification();
                    }else if (BCrypt.Net.BCrypt.Verify(password, passwordBD) && textBoxCaptcha.Text != capcha)
                    {
                        MessageBox.Show(
                                "Капча не пройдена",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation

                                );
                        edit();
                    }
                    else if (login == loginAdmin && password == pwdAdmin && textBoxCaptcha.Text == capcha)
                        {
                            includeAdmin includeAdmin = new includeAdmin();
                            includeAdmin.Show();
                            this.Hide();
                    }
                    else 
                    {
                        MessageBox.Show(
                            "Авторизация не пройдена.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation
                            );
                        edit();
                    }
                 }
                else
                {
                    MessageBox.Show(
                           "Введите капчу",
                           "Ошибка",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation

                           );
                }
                enter.Enabled = false;
                sleep();
                enter.Enabled = true;
                textBoxLogin.Text = "";
                textBoxPwd.Text = "";
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
                    graphics.Clear(Color.Gold);

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
                            Pen pen = new Pen(Color.Gray, 1);
                            for (int i = 0; i < Random.Next(4,6); i++)
                            {
                                char mas = masAlf[Random.Next(0, masAlf.Length - 1)];
                                graphics.DrawString($"{mas}", font, brush, new PointF(x, y));
                                x += Random.Next(24,60);
                                y = Random.Next(0, pbHeight - 40);

                                capcha += mas.ToString();
                            }
                            for (int i = 0;i < pbHeight; i +=5)
                            {
                                graphics.DrawLine(pen, new PointF(0, i), new PointF(pbWidth, i));
                            }
                            for (int i = 0; i < pbWidth; i += 5)
                            {
                                graphics.DrawLine(pen, new PointF(i, 0), new PointF(i, pbHeight));
                            }
                            graphics.DrawLine(pen, new PointF(0, 0), new PointF(pbWidth, pbHeight));
                            graphics.DrawLine(pen, new PointF(0, pbHeight), new PointF(pbWidth,0));

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
            func.FormPaint(this);
        }
    }
}
