﻿using System;
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
        public Auntification()
        {
            InitializeComponent();
        }

        private void Auntification_Load(object sender, EventArgs e)
        {
            textBoxLogin.Text = "admin";
            textBoxPwd.Text = "admin";
            
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
            if(login == loginAdmin && password == pwdAdmin)
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

                }else if (nextCaptcha)
                {
                    MessageBox.Show("Capcha");
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
                    edit();
                }
            }
        }
        void edit()
        {
            
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
