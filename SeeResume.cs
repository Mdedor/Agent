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

namespace Agent
{
    public partial class SeeResume : Form
    {
        int currentRowIndex;

        string professions;
        int idVacancy;
        string searcIn;
        string roleEmp;
        int countRecords;
        string searchNowCount;
        int countRecordsBD;
        public SeeResume(string profession = " ", int VacancyID = 0) // ИЗМЕНИТЬ РАЗМЕР СТРАНИЦЫ!!!!!!!!!!!!!
        {
            professions = profession;
            InitializeComponent();
            idVacancy = VacancyID;
            searchNowCount = @"SELECT Count(*) FROM agent.resume 
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4);";
            countRecords = func.records(searchNowCount);
            countRecordsBD = func.records(searchNowCount);
            roleEmp = func.search($"SELECT employe_post FROM employe WHERE id = {port.empIds}");
            searcIn = $@"SELECT resume.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_delete_status as 'Status'
                        FROM resume  
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id ";
            if (roleEmp == "2")
            {
                if (idVacancy >= 0 && professions != " ")
                    searcIn += $"WHERE profession.name = '{professions}'";
                else
                {
                    textBoxSearch.Visible = true;
                    comboBox1.Visible = true;
                    comboBox2.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label2.Text = $"{countRecords} из {countRecordsBD}";
                }
            }else if (roleEmp == "1") 
            { 

            }
            
            
        }

        private void resumeList_Load(object sender, EventArgs e)
        {
            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");

            if (idVacancy != 0)
            {
                exit.Location = new Point(1161, 870);
                this.Size = new Size(1391, 1048);
                string searcIn = $@"SELECT vacancy.id, company.company_name as 'Компания', profession.name as 'Профессия', vacancy.vacancy_responsibilities as 'Обязанности', vacancy.vacancy_requirements as 'Требования', vacancy.vacancy_conditions as 'Условия', CONCAT( vacancy.vacancy_salary_by, ' - ', vacancy.vacancy_salary_before) as 'Размер зарплаты',  vacancy.vacancy_address as 'Адресс работы',  vacancy.vacancy_delete_status as 'Status' FROM vacancy INNER JOIN company ON vacancy.vacancy_company = company.id INNER JOIN profession ON vacancy.vacancy_profession = profession.id WHERE vacancy.id ='{idVacancy}'";
                func.load(dataGridView2, searcIn);
                dataGridView2.Columns["id"].Visible = false;
                dataGridView2.Columns["Status"].Visible = false;
                dataGridView2.Columns["Компания"].Width = 100;
                dataGridView2.Columns["Профессия"].Width = 140;
                dataGridView2.Columns["Обязанности"].Width = 260;
                dataGridView2.Columns["Требования"].Width = 200;
                dataGridView2.Columns["Условия"].Width = 200;
                dataGridView2.Columns["Размер зарплаты"].Width = 110;
                dataGridView2.Columns["Адресс работы"].Width = 300;



                dataGridView2.Visible = true;
            }
            comboBox2.Items.Add("Без фильтра");
            comboBox1.Items.Add("По возрастанию зарплаты");
            comboBox1.Items.Add("По убыванию зарплаты");
            List<string> list = new List<string>();
            MySqlConnection connection = new MySqlConnection(Connection.connect());
            connection.Open();
            string find = $"SELECT name FROM profession;";
            MySqlCommand com = new MySqlCommand(find, connection);

            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {

                if (!comboBox2.Items.Contains(reader[0].ToString()))
                    comboBox2.Items.Add(reader[0].ToString());
            }
            connection.Close();
            func.load(dataGridView1, searcIn);
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["Соискатель"].Width = 270;
            dataGridView1.Columns["Профессия"].Width = 250;
            dataGridView1.Columns["Знание языков"].Width = 250;
            dataGridView1.Columns["Личностные качества"].Width = 400;
            dataGridView1.Columns["Зарплата"].Width = 140;
            
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1" || status == "3")
                    row.Visible = false;

            }
            manager.ResumeBinding();
        }
        void menu(object sender, MouseEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();

            this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                if (roleEmp == "2")
                {
                    contextMenu.MenuItems.Add(new MenuItem("Посмотреть подробную информацию", click_to));
                }else if (roleEmp == "1")
                {
                    contextMenu.MenuItems.Add(new MenuItem("Редактировать резюме", update));
                    contextMenu.MenuItems.Add(new MenuItem("Удалить резюме", delete));
                }
                
                if (currentRowIndex >= 0)
                {
                    dataGridView1.Rows[currentRowIndex].Selected = true;
                    contextMenu.Show(dataGridView1, new Point(e.X, e.Y));
                }
                else
                {
                    currentRowIndex = 0;


                }
            }

        }
        string Search()
        {
            string searchNowCount = @"SELECT Count(*) FROM agent.resume INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4)";
            string basis = $@"SELECT resume.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_delete_status as 'Status' FROM resume  INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id INNER JOIN profession ON resume.resume_profession = profession.id  "; ;
            if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedIndex != 0 || textBoxSearch.Text.Length > 0)
            {
                basis += "WHERE ";
            }
            if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedIndex != 0)
            {
                basis += $"(profession.name = '{comboBox2.SelectedItem}')";
                searchNowCount += $" and (profession.name = '{comboBox2.SelectedItem}')";
            }
            if (textBoxSearch.Text.Length > 0)
            {
                if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedIndex != 0)
                    basis += " AND ";
                basis += $"(CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) LIKE '%{textBoxSearch.Text}%' OR resume.resume_personal_qualities LIKE '%{textBoxSearch.Text}%' OR resume.resume_knowledge_of_languages LIKE '%{textBoxSearch.Text}%')";
                searchNowCount += $" and (CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) LIKE '%{textBoxSearch.Text}%' OR resume.resume_personal_qualities LIKE '%{textBoxSearch.Text}%' OR resume.resume_knowledge_of_languages LIKE '%{textBoxSearch.Text}%')";
            }
            if (comboBox1.SelectedIndex == 0)
            {
                basis += $"ORDER BY resume.salary";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                basis += $"ORDER BY resume.salary DESC";
            }
            countRecords = func.records(searchNowCount);
            label2.Text = $"{countRecords} из {countRecordsBD}";
            return basis;
        }
        void click_to(object sender, EventArgs e)
        {
            int resume = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            res res = new res(idVacancy, resume);
            res.Show();
            this.Hide();

        }
        void update(object sender, EventArgs e)
        {
            int resumes = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            AddR addR = new AddR(0, resumes);
            addR.Show();
            this.Close();

        }
        void delete(object sender, EventArgs e)
        {
            DialogResult results = MessageBox.Show(
               "Вы действительно хотите удалить резюме?",
               "Подтверждение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information
               );
            if (results == DialogResult.Yes)
            {
                int resume = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
                func.direction($"DELETE FROM resume WHERE id ={resume}");
                func.load(dataGridView1, searcIn);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (roleEmp == "2")
            {
                if (idVacancy >= 0 && professions != " ")
                {
                    SeeVacancy seeVacancy = new SeeVacancy();
                    seeVacancy.Show();
                    this.Close();
                }
                else
                {
                    MenuManager menuManager = new MenuManager();
                    menuManager.Show();
                    this.Close();
                }
            }
            else
            {
                AdminS adminS = new AdminS();
                adminS.Show();
                this.Close();
            }

        }

       

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            menu(sender, e);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            func.load(dataGridView1, Search());
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1" || status == "3")
                    row.Visible = false;

            }
            manager.ResumeBinding();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            func.load(dataGridView1, Search());
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1" || status == "3")
                    row.Visible = false;

            }
            manager.ResumeBinding();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            func.load(dataGridView1, Search());
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1" || status == "3")
                    row.Visible = false;

            }
            manager.ResumeBinding();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SeeResume_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
