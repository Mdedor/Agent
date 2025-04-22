using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Agent
{
    public partial class AddE : Form
    {
        string name;
        string surname;
        string patronomic;
        string adress;
        string phone;
        string pwd;
        string login;
        int post;
        int empoyIds;
        int gender;
        string photo;
        int flag;
        Random Random = new Random();
        public AddE(int employId)
        {
            InitializeComponent();
            empoyIds = employId;
        }
        void checkEnable()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxSurname.Text.Length > 0)
                    count++;
                if (textBoxName.Text.Length > 0)
                    count++;
                if (textBoxPatronomic.Text.Length > 0)
                    count++;
                if (textBoxAdress.Text.Length > 0)
                    count++;
                if (maskedTextBoxPhoneNumber.MaskFull)
                    count++;
                if (textBoxLogin.Text.Length > 0)
                    count++;
                if (textBoxPwd.Text.Length > 0)
                    count++;
                if (comboBoxPost.SelectedIndex != -1)
                    count++;

                if (count == 8)
                {
                    buttonAddS.Enabled = true;
                }
                else
                {
                    buttonAddS.Enabled = false;
                }
            }
        }
        void checkEnableUpdate()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxSurname.Text != surname)
                    count++;
                if (textBoxName.Text != name)
                    count++;
                if (textBoxPatronomic.Text != patronomic)
                    count++;
                if (textBoxAdress.Text != adress)
                    count++;
                if (maskedTextBoxPhoneNumber.Text != phone)
                    count++;
                if (textBoxLogin.Text != login)
                    count++;
                if (textBoxPwd.Text != pwd)
                    count++;
                if (comboBoxPost.SelectedIndex + 1 != post)
                    count++;
                if (count > 0)
                {
                    buttonAddS.Enabled = true;
                }
                else
                {
                    buttonAddS.Enabled = false;
                }
            }

        }

        private void AddE_Load(object sender, EventArgs e)
        {
           comboBoxPost.Items.Add("Администратор");
           comboBoxPost.Items.Add("Менеджер");
            if (empoyIds != 0)
            {
                buttonAddS.Text = "Изменить";
                label1.Text = "Редактирование резюме";
                MySqlConnection connection = new MySqlConnection(Connection.connect());
                connection.Open();
                string find = $"SELECT * FROM employe WHERE id = {empoyIds};";
                MySqlCommand com = new MySqlCommand(find, connection);
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    textBoxSurname.Text = reader[1].ToString();
                    textBoxName.Text = reader[2].ToString();
                    textBoxPatronomic.Text = reader[3].ToString();
                    maskedTextBoxPhoneNumber.Text = reader[4].ToString();
                    textBoxAdress.Text = reader[5].ToString();
                    textBoxLogin.Text = reader[6].ToString();
                    pwd = reader[7].ToString();
                    
                    post = Convert.ToInt32(reader[8].ToString());
                    if (post == 1)
                    {
                        comboBoxPost.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBoxPost.SelectedIndex = 1;
                    }
                }
                name = textBoxName.Text;
                surname = textBoxSurname.Text;
                patronomic = textBoxPatronomic.Text;
                adress = textBoxAdress.Text;
                phone = maskedTextBoxPhoneNumber.Text.ToString();
                login = textBoxLogin.Text;
                post = comboBoxPost.SelectedIndex;
                connection.Close();
                flag = 1;
            }
        }

        private void buttonAddS_Click_1(object sender, EventArgs e)
        {
            if (empoyIds == 0) {

                name = textBoxName.Text;
                surname = textBoxSurname.Text;
                patronomic = textBoxPatronomic.Text;
                adress = textBoxAdress.Text;
                phone = maskedTextBoxPhoneNumber.Text;
                login = textBoxLogin.Text;
                pwd = BCrypt.Net.BCrypt.HashPassword(textBoxPwd.Text);

                DialogResult result = MessageBox.Show(
               "Добавить сотрудника?",
               "Подтверждение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information
               );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        func.direction($@"
                                INSERT INTO employe (employe_surname, employe_name, employe_partronymic, employe_phone_number, employe_adress, employe_login, employe_pwd, employe_post) 
                                SELECT '{surname}', '{name}', '{patronomic}', '{phone}', '{adress}', '{login}', '{pwd}', {comboBoxPost.SelectedIndex + 1} 
                                WHERE NOT EXISTS (
                                    SELECT 1 FROM employe 
                                    WHERE employe_login = '{login}' AND  (employe_delete_status IS NULL OR employe_delete_status = 3 OR employe_delete_status = 4)
                                );
                            ");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Ошибка SQL: {ex.Message}, Number: {ex.Number}");
                        // Обработка специфических ошибок SQL, например, проверка номера ошибки (ex.Number)
                        // для определения типа ошибки (нарушение целостности, дубликат ключа и т.д.)
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show($"Ошибка операции с базой данных: {ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show($"Ошибка аргумента: {ex.Message}"); // Например, некорректный запрос
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Непредвиденная ошибка: {ex.Message}");
                    }
                    
                    textBoxName.Clear();
                    textBoxAdress.Clear();
                    textBoxPatronomic.Clear();
                    textBoxSurname.Clear();
                    maskedTextBoxPhoneNumber.Clear();
                    textBoxLogin.Clear();
                    textBoxPwd.Clear();
                }

                
            }
            else
            {
                string update = $@"UPDATE employe 
                            SET employe_surname = '{textBoxSurname.Text}', employe_name = '{textBoxName.Text}', employe_partronymic = '{textBoxPatronomic.Text}', employe_phone_number = '{maskedTextBoxPhoneNumber.Text}', employe_adress = '{textBoxAdress.Text}', employe_login = '{textBoxLogin.Text}', employe_post = {comboBoxPost.SelectedIndex + 1} ";

                if (textBoxPwd.Text.Length > 0)
                {
                    string addPwd = $", employe_pwd='{BCrypt.Net.BCrypt.HashPassword(textBoxPwd.Text)}' ";

                    update += addPwd;
                }
                update += $"WHERE id = {empoyIds}";
                DialogResult results = MessageBox.Show(
                   "Вы действительно хотите изменить сотрудника?",
                   "Подтверждение",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Information
                   );
                if (results == DialogResult.Yes)
                {
                    func.direction(update);
                    MessageBox.Show("Запись успешно изменена", "Уведомление");
                    SeeEmp seeEmp = new SeeEmp();
                    seeEmp.Show();
                    this.Close();
                }
                
            }
        }

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (!string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                var words = textBoxSurname.Text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(words[i]))
                    {
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                    }
                }
                textBoxSurname.Text = string.Join(" ", words);
                textBoxSurname.SelectionStart = textBoxSurname.Text.Length;

                textBoxSurname.SelectionStart = textBoxSurname.Text.Length;
                textBoxSurname.SelectionLength = 0;
                textBoxSurname.TextChanged -= textBoxSurname_TextChanged;
                textBoxSurname.Text = textBoxSurname.Text;
                textBoxSurname.TextChanged += textBoxSurname_TextChanged;

            }
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (!string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                var words = textBoxName.Text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(words[i]))
                    {
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                    }
                }
                textBoxName.Text = string.Join(" ", words);
                textBoxName.SelectionStart = textBoxName.Text.Length;

                textBoxName.SelectionStart = textBoxName.Text.Length;
                textBoxName.SelectionLength = 0;
                textBoxName.TextChanged -= textBoxName_TextChanged;
                textBoxName.Text = textBoxName.Text;
                textBoxName.TextChanged += textBoxName_TextChanged;
            }
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxPatronomic_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (!string.IsNullOrWhiteSpace(textBoxPatronomic.Text))
            {
                var words = textBoxPatronomic.Text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(words[i]))
                    {
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                    }
                }
                textBoxPatronomic.Text = string.Join(" ", words);
                textBoxPatronomic.SelectionStart = textBoxPatronomic.Text.Length;

                textBoxPatronomic.SelectionStart = textBoxPatronomic.Text.Length;
                textBoxPatronomic.SelectionLength = 0;
                textBoxPatronomic.TextChanged -= textBoxPatronomic_TextChanged;
                textBoxPatronomic.Text = textBoxPatronomic.Text;
                textBoxPatronomic.TextChanged += textBoxPatronomic_TextChanged;
            }
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void maskedTextBoxPhoneNumber_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void textBoxAdress_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void comboBoxPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxPwd_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }

        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            port.move = 1;
            if (empoyIds == 0)
            {
                AdminE adminE = new AdminE();
                adminE.Show();
                this.Close();
            }
            else
            {
                SeeEmp seeEmp = new SeeEmp();
                seeEmp.Show();
                this.Close();
            }
        }

        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRu(e);
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRu(e);
        }

        private void textBoxPatronomic_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRu(e);
        }

        private void textBoxAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.address(e);
        }

        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressEn(e);
        }

        private void textBoxPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressEn(e);
        }

        private void AddE_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }

        private void maskedTextBoxPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (empoyIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }
    }
}
