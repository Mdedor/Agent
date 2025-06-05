using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Agent
{
    public partial class AddV : Form
    {
        int companyIDS;
        int vacancyIDS;
        string responsibilities;
        string requerements;
        string conditions;
        string adress;
        string salary_by;
        string salary_before;
        int profession;
        int flag;

        public AddV(int companyID=0, int vacancyID = 0)
        {
            InitializeComponent();
            companyIDS = companyID;
            vacancyIDS = vacancyID;
        }
        void checkEnableUpdate()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxAdress.Text != adress)
                    count++;
                if (textBoxCond.Text != conditions)
                    count++;
                if (textBoxTreb.Text != requerements)
                    count++;
                if (textBoxObz.Text != responsibilities)
                    count++;
                if (comboBoxProfessioin.SelectedIndex != profession)
                    count++;
                if (textBoxSalaryBy.Text != salary_by)
                    count++;
                if (textBoxSalaryBefore.Text != salary_by)
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
        void checkEnable()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxAdress.Text.Length > 0)
                    count++;
                if (textBoxCond.Text.Length > 0)
                    count++;
                if (textBoxObz.Text.Length > 0)
                    count++;
                if (textBoxSalaryBefore.Text.Length > 0)
                    count++;
                if (textBoxSalaryBy.Text.Length > 0)
                    count++;
                if (textBoxTreb.Text.Length > 0)
                    count++;
                if (comboBoxProfessioin.SelectedIndex != -1)
                    count++;

                if (count == 7)
                {
                    buttonAddS.Enabled = true;
                }
                else
                {
                    buttonAddS.Enabled = false;
                }
            }
        }
        private void AddV_Load(object sender, EventArgs e)
        {

            MySqlConnection connection = new MySqlConnection(Connection.connect());
            connection.Open();

            string find = $"SELECT name FROM profession;";
            MySqlCommand com = new MySqlCommand(find, connection);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {

                if (!comboBoxProfessioin.Items.Contains(reader[0].ToString()))
                    comboBoxProfessioin.Items.Add(reader[0].ToString());
            }
            connection.Close();
            comboBoxProfessioin.MaxDropDownItems = 5;
            comboBoxProfessioin.DropDownWidth = 400;
            
            if (vacancyIDS != 0)
            {
                buttonAddS.Text = "Изменить";
                label1.Text = "Редактирование вакансии";

                MySqlConnection con = new MySqlConnection(Connection.connect());
                con.Open();
                string search = $"SELECT * FROM vacancy WHERE id = {vacancyIDS};";
                MySqlCommand comm = new MySqlCommand(search, con);
                MySqlDataReader readerr = comm.ExecuteReader();
                while (readerr.Read())
                {
                    comboBoxProfessioin.SelectedIndex = Convert.ToInt32(readerr[2].ToString()) - 1;
                    profession = Convert.ToInt32(readerr[2].ToString()) - 1;
                    textBoxTreb.Text = readerr[3].ToString();
                    textBoxObz.Text = readerr[4].ToString();
                    textBoxCond.Text = readerr[5].ToString();
                    textBoxAdress.Text = readerr[6].ToString();
                    textBoxSalaryBy.Text = readerr[7].ToString();
                    textBoxSalaryBefore.Text = readerr[8].ToString();
                }
                responsibilities = textBoxObz.Text.ToString();
                requerements = textBoxTreb.Text.ToString();
                conditions=textBoxCond.Text.ToString();
                adress=textBoxAdress.Text.ToString();
                salary_before = textBoxSalaryBefore.Text.ToString();
                salary_by = textBoxSalaryBy.Text.ToString();
                flag = 1;
            }
        }

        

        private void comboBoxProfessioin_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxTreb_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxObz_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxCond_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxSalaryBy_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxSalaryBefore_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxAdress_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
            {
                AdminC adminC = new AdminC();
                adminC.Show();
                this.Close();
            }
            else
            {
               SeeVacancyNew seeVacancyNew = new SeeVacancyNew();
                seeVacancyNew.Show();   
                this.Close();
            }
            
        }

        private void buttonAddS_Click_1(object sender, EventArgs e)
        {
            port.move = 1;
            if (vacancyIDS == 0)
            {
                int salaryBy = Convert.ToInt32(textBoxSalaryBy.Text);
                int salaryBefor = Convert.ToInt32(textBoxSalaryBefore.Text);
                if (salaryBy > salaryBefor)
                {
                    textBoxSalaryBefore.Text = salaryBy.ToString();
                    textBoxSalaryBy.Text = salaryBefor.ToString();
                }
                
                    

                DialogResult result = MessageBox.Show(
                "Добавить вакансию?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        func.direction($@"
                            INSERT INTO vacancy (vacancy_company, vacancy_profession, vacancy_responsibilities, vacancy_requirements, vacancy_conditions, vacancy_address, vacancy_salary_by, vacancy_salary_before)
                            SELECT '{companyIDS}', '{comboBoxProfessioin.SelectedIndex + 1}', '{textBoxObz.Text}', '{textBoxTreb.Text}', '{textBoxCond.Text}', '{textBoxAdress.Text}', '{textBoxSalaryBy.Text}', '{textBoxSalaryBefore.Text}'
                            WHERE NOT EXISTS (
                                SELECT 1 FROM vacancy 
                                WHERE vacancy_company = '{companyIDS}' 
                                AND vacancy_profession = '{comboBoxProfessioin.SelectedIndex + 1}' 
                                AND vacancy_responsibilities = '{textBoxObz.Text}' 
                                AND vacancy_requirements = '{textBoxTreb.Text}' 
                                AND vacancy_conditions = '{textBoxCond.Text}' 
                                AND vacancy_address = '{textBoxAdress.Text}' 
                                AND vacancy_salary_by = '{textBoxSalaryBy.Text}' 
                                AND vacancy_salary_before = '{textBoxSalaryBefore.Text}'
                                AND (vacancy_delete_status IS NULL OR vacancy_delete_status = 3 OR vacancy_delete_status = 4)
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
                    finally
                    {
                        textBoxCond.Clear();
                    textBoxAdress.Clear();
                    textBoxObz.Clear();
                    textBoxSalaryBefore.Clear();
                    textBoxSalaryBy.Clear();
                    textBoxTreb.Clear();
                    comboBoxProfessioin.SelectedIndex = -1;
                        MessageBox.Show("Запись успешно добавлена", "Уведомление");
                    }
                    
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                "Вы действительно хотите изменить вакансию?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
                if (result == DialogResult.Yes)
                {
                    responsibilities = textBoxObz.Text.ToString();
                    requerements = textBoxTreb.Text.ToString();
                    conditions = textBoxCond.Text.ToString();
                    adress = textBoxAdress.Text.ToString();
                    salary_before = textBoxSalaryBefore.Text.ToString();
                    salary_by = textBoxSalaryBy.Text.ToString();

                    string searchIn = $@"UPDATE vacancy SET vacancy_profession = '{comboBoxProfessioin.SelectedIndex + 1}', vacancy_responsibilities = '{responsibilities}', vacancy_requirements = '{requerements}', vacancy_conditions = '{conditions}', vacancy_address = '{adress}', vacancy_salary_by = '{salary_by}', vacancy_salary_before = '{salary_before}' WHERE id = {vacancyIDS};";
                    
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
                    SeeVacancy seeVacancy = new SeeVacancy();
                    seeVacancy.Show();
                    this.Close();
                }
            }

            
        }

        private void textBoxTreb_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRuPlus(e);
        }

        private void textBoxObz_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRuPlus(e);
        }

        private void textBoxCond_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRuPlus(e);
        }

        private void textBoxSalaryBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.salary(e);
        }

        private void textBoxSalaryBefore_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.salary(e);
        }

        private void textBoxAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.address(e);
        }

        private void AddV_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void comboBoxProfessioin_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddV_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }
    }
}
