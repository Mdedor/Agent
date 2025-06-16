using MySql.Data.MySqlClient;
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

namespace Agent
{
    public partial class AddR : Form
    {
        int aplicantID;
        int resumeId;
        string educ;
        string salary;
        string pers;
        string lang;
        int prof;
        string exp;
        int flag;
        public AddR(int applicant=0,int resume=0)
        {
            InitializeComponent();
            aplicantID = applicant;
            resumeId = resume;
            if (resumeId != 0)
            {
                label1.Text = "Редактировать резюме";
            }
        }
        void checkEnable()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxEducation.Text.Length > 0)
                    count++;
                if (textBoxSalary.Text.Length > 0)
                    count++;
                if (textBoxPers.Text.Length > 0)
                    count++;
                if (textBoxLang.Text.Length > 0)
                    count++;
                if (comboBoxProfessioin.SelectedIndex != -1)
                    count++;
                if (textBoxExp.Text.Length > 0)
                    count++;

                if (count == 6)
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
                if (textBoxEducation.Text != educ)
                    count++;
                if (textBoxSalary.Text != salary)
                    count++;
                if (textBoxPers.Text != pers)
                    count++;
                if (textBoxLang.Text != lang)
                    count++;
                if (comboBoxProfessioin.SelectedIndex != prof)
                    count++;
                if (textBoxExp.Text != exp)
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
        private void AddR_Load(object sender, EventArgs e)
        {
            flag = 1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Connection.connect()))
                {
                    connection.Open();
                    string find = "SELECT name FROM profession;";
                    using (MySqlCommand com = new MySqlCommand(find, connection))
                    using (MySqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string professionName = reader[0].ToString();
                            if (!comboBoxProfessioin.Items.Contains(professionName))
                                comboBoxProfessioin.Items.Add(professionName);
                        }
                    }
                }

                comboBoxProfessioin.MaxDropDownItems = 5;
                comboBoxProfessioin.DropDownWidth = 400;

                if (resumeId != 0)
                {
                    try
                    {
                        buttonAddS.Text = "Изменить";
                        label1.Text = "Редактирование резюме";

                        using (MySqlConnection con = new MySqlConnection(Connection.connect()))
                        {
                            con.Open();
                            string search = $"SELECT * FROM resume WHERE id = {resumeId};";
                            using (MySqlCommand comm = new MySqlCommand(search, con))
                            using (MySqlDataReader readerr = comm.ExecuteReader())
                            {
                                while (readerr.Read())
                                {
                                    aplicantID = Convert.ToInt32(readerr[1].ToString());
                                    prof = Convert.ToInt32(readerr[2].ToString()) - 1;
                                    salary = readerr[3].ToString();
                                    educ = readerr[4].ToString();
                                    exp = readerr[5].ToString();
                                    lang = readerr[6].ToString();
                                    pers = readerr[7].ToString();
                                }
                            }
                        }

                        comboBoxProfessioin.SelectedIndex = prof;
                        textBoxSalary.Text = salary;
                        textBoxEducation.Text = educ;
                        textBoxExp.Text = exp;
                        textBoxLang.Text = lang;
                        textBoxPers.Text = pers;
                    }
                    catch (Exception ex)
                    {
                        flag = 0;
                        MessageBox.Show($"Ошибка при загрузке резюме: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                flag = 0;
                MessageBox.Show($"Ошибка при загрузке профессий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddS_Click(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
            {
                DialogResult result = MessageBox.Show(
                "Добавить резюме?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
                if (result == DialogResult.Yes)
                {

                    string searchIn = $@"
                                INSERT INTO resume (resume_applicant, resume_profession, salary, resume_education, resume_work_experience, resume_knowledge_of_languages, resume_personal_qualities)
                                SELECT '{aplicantID}', '{comboBoxProfessioin.SelectedIndex + 1}', '{Convert.ToInt32(textBoxSalary.Text)}', '{textBoxEducation.Text}', '{textBoxExp.Text}', '{textBoxLang.Text}', '{textBoxPers.Text}'
                                WHERE NOT EXISTS (
                                    SELECT 1 FROM resume 
                                    WHERE resume_applicant = '{aplicantID}' 
                                    AND resume_profession = '{comboBoxProfessioin.SelectedIndex + 1}' 
                                    AND salary = '{Convert.ToInt32(textBoxSalary.Text)}' 
                                    AND resume_education = '{textBoxEducation.Text}' 
                                    AND resume_work_experience = '{textBoxExp.Text}' 
                                    AND resume_knowledge_of_languages = '{textBoxLang.Text}' 
                                    AND resume_personal_qualities = '{textBoxPers.Text}'
                                );";

                    try
                    {
                        func.direction(searchIn);
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
                    MessageBox.Show("Запись успешно добавлена", "Уведомление");
                    AdminS adminS = new AdminS();
                    adminS.Show();
                    this.Close();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                "Вы действительно хотите изменить резюме?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
                if (result == DialogResult.Yes)
                {
                    string searchIn = $@"UPDATE resume SET resume_profession = '{comboBoxProfessioin.SelectedIndex + 1}', salary = '{Convert.ToInt32(textBoxSalary.Text)}', resume_education = '{textBoxEducation.Text}', resume_work_experience = '{textBoxExp.Text}', resume_knowledge_of_languages = '{textBoxLang.Text}', resume_personal_qualities = '{textBoxPers.Text}' WHERE  id = {resumeId};";
                    try
                    {
                        func.direction(searchIn);
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
                    
                    MessageBox.Show("Запись успешно изменена", "Уведомление");
                    SeeResume seeResume = new SeeResume();
                    seeResume.Show();
                    this.Close();
                }
            }
            
        }

        private void textBoxSalary_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }



        }

        private void comboBoxProfessioin_SelectedIndexChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void textBoxExp_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void textBoxLang_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void textBoxPers_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void textBoxEducation_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            port.move = 1;
            if (resumeId == 0)
            {
                SeeS see = new SeeS(true);
                see.Show();
                this.Close();
            }
            else
            {
                SeeResume seeResume = new SeeResume();
                seeResume.Show();
                this.Close();
            }
            
        }

        private void textBoxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.salary(e);
        }

        private void textBoxExp_KeyPress(object sender, KeyPressEventArgs e)
        {
           func.rus_end(e);
        }

        private void textBoxLang_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRuPlus(e);
        }

        private void textBoxPers_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRuPlus(e);
        }

        private void textBoxEducation_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.rus_end(e);
        }

        private void AddR_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void AddR_Move(object sender, EventArgs e)
        {
            port.move = 1;
        }
    }
}
