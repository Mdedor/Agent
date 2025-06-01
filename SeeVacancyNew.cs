using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;

namespace Agent
{
    public partial class SeeVacancyNew : Form
    {
        int currentRowIndex;
        int currentColumnIndex;

        string roleEmp;
        string searchIn;
        int resume;
        string profession;
        int applicantId;
        double countRecordsBD;
        double countRecords;
        int page = 1;
        string searchNowCount;
        double allPageCount;
        double countRecordsBDVacancy;
        double countRecordsVacancy;
        int idFilterResume = 0;
        int idFilterVacancy = 0;
        int idSortVacancy = -1;
        int idSortResume = -1;

        int aplicantID = 0;
        int vacancyID = 0;

        double countRecordsBDResume;
        double countRecordsResume;
        int pageResume = 1;
        int pageVacancy = 1;
        int flag = 1;
        string vacancyProfession = "0";
        string resumeProfession = "0";
        string searchNowCountVacancy;
        string searchNowCountResume;
        double allPageCountVacancy;
        double allPageCountResume;
        public SeeVacancyNew(int idResume = 0, string profession = "")
        {
            InitializeComponent();
            searchNowCountVacancy = "SELECT count(*) FROM vacancy INNER JOIN company ON vacancy.vacancy_company = company.id " +
                                    "INNER JOIN profession ON vacancy.vacancy_profession = profession.id  " +
                                    "where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4)";
            countRecordsVacancy = func.records(searchNowCountVacancy);
            countRecordsBDVacancy = func.records(searchNowCountVacancy);
            resume = idResume;
            roleEmp = func.search($"SELECT employe_post FROM employe WHERE id = {port.empIds}");

            searchIn = "SELECT vacancy.id, company.company_name as 'Компания', profession.name as 'Профессия', vacancy.vacancy_responsibilities as 'Обязанности', vacancy.vacancy_requirements as 'Требования', vacancy.vacancy_conditions as 'Условия', CONCAT( vacancy.vacancy_salary_by, ' - ', vacancy.vacancy_salary_before) as 'Размер зарплаты',  vacancy.vacancy_delete_status as 'Status',  companyc_linq as 'Cсылка' " +
                            "FROM vacancy " +
                            "INNER JOIN company ON vacancy.vacancy_company = company.id " +
                            "INNER JOIN profession ON vacancy.vacancy_profession = profession.id " +
                            "where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4) ";
            if (roleEmp == "2")
            {
                if (resume == 0)
                {
                    textBoxSearch.Visible = true;
                    comboBox1.Visible = true;
                    comboBox2.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    allPageCountVacancy = Math.Ceiling(countRecordsVacancy / 20);
                    label2.Text = $"{countRecordsVacancy} из  {countRecordsBDVacancy}";
                }
                else
                {
                    applicantId = Convert.ToInt32(func.search($"SELECT resume_applicant FROM resume WHERE id = {idResume}"));
                    searchIn += $" AND (profession.name = '{profession}') ";
                }

            }
        }
        string Search()
        {

            string searchNowCount = "SELECT count(*) FROM vacancy INNER JOIN company ON vacancy.vacancy_company = company.id INNER JOIN profession ON vacancy.vacancy_profession = profession.id  where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4)";
            string basis = "SELECT vacancy.id, company.company_name as 'Компания', profession.name as 'Профессия', vacancy.vacancy_responsibilities as 'Обязанности', vacancy.vacancy_requirements as 'Требования', vacancy.vacancy_conditions as 'Условия', CONCAT( vacancy.vacancy_salary_by, ' - ', vacancy.vacancy_salary_before) as 'Размер зарплаты',  vacancy.vacancy_delete_status as 'Status',  companyc_linq as 'Cсылка' " +
                            "FROM vacancy " +
                            "INNER JOIN company ON vacancy.vacancy_company = company.id " +
                            "INNER JOIN profession ON vacancy.vacancy_profession = profession.id " +
                            "where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4) ";

            if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedIndex != 0)
            {
                basis += $" AND (profession.name = '{comboBox2.SelectedItem}')";
                searchNowCount += $" AND (profession.name = '{comboBox2.SelectedItem}')";
            }
            if (textBoxSearch.Text.Length > 0)
            {


                basis += $" AND (company.company_name LIKE '%{textBoxSearch.Text}%' OR vacancy.vacancy_requirements LIKE '%{textBoxSearch.Text}%' OR vacancy.vacancy_conditions LIKE '%{textBoxSearch.Text}%' OR  vacancy.vacancy_responsibilities LIKE '%{textBoxSearch.Text}%')";
                searchNowCount += $" and (company.company_name LIKE '%{textBoxSearch.Text}%' OR vacancy.vacancy_requirements LIKE '%{textBoxSearch.Text}%' OR vacancy.vacancy_conditions LIKE '%{textBoxSearch.Text}%' OR  vacancy.vacancy_responsibilities LIKE '%{textBoxSearch.Text}%')";
            }
            
            if (comboBox1.SelectedIndex == 1)
            {
                basis += $"ORDER BY vacancy.vacancy_salary_by";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                basis += $"ORDER BY vacancy.vacancy_salary_by DESC";
            }
            else if (comboBox1.SelectedIndex == 0)
            {

            }
            countRecordsVacancy = func.records(searchNowCount);
            countRecordsBDVacancy = func.records(searchNowCount);
            label2.Text = $"{countRecordsVacancy} из {countRecordsBDVacancy}";
            pageVacancy = 1;
            allPageCountVacancy = Math.Ceiling(countRecordsVacancy / 20);
            label5.Text = allPageCountVacancy.ToString();
            return basis;
        }
        void done()
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {


                string obz = dataGridViewRow.Cells["Обязанности"].Value.ToString();
                string tr = dataGridViewRow.Cells["Требования"].Value.ToString();




                dataGridViewRow.Cells["Специфика"].Value = $"{obz}\n{tr}\n";

            }
        }
        void load_load()
        {


            func.load(dataGridView1, searchIn);
            dataGridView1.Columns.Add("Специфика", "Специфика");
            done();

            dataGridView1.Columns["id"].Visible = false;


            dataGridView1.Columns["Обязанности"].Visible = false;
            dataGridView1.Columns["Требования"].Visible = false;





            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["Cсылка"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["Компания"] = new DataGridViewLinkCell();
                    DataGridViewLinkCell c = r.Cells["Компания"] as DataGridViewLinkCell;
                    c.LinkColor = Color.Green;

                }
            }

        }
        void menu(object sender, MouseEventArgs e)
        {


            ContextMenu contextMenu = new ContextMenu();
            this.currentColumnIndex = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                if (roleEmp == "3")
                {
                    if (resume == 0)
                        contextMenu.MenuItems.Add(new MenuItem("Выбрать соискателя", click_to));
                    else
                        contextMenu.MenuItems.Add(new MenuItem("Создать направление", dir));

                }
                else
                {
                    contextMenu.MenuItems.Add(new MenuItem("Редактировать вакансию", update));
                    contextMenu.MenuItems.Add(new MenuItem("Удалить вакансию", delete));
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
        void dir(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Создать направление с этим резюме?",
               "Подтверждение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information
               );
            if (result == DialogResult.Yes)
            {
                string vacancyProfession = dataGridView1.Rows[currentRowIndex].Cells["Профессия"].Value.ToString();
                int vacancyID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
                DateTime now = DateTime.Now;
                func.direction($"INSERT INTO direction(direction_aplicant,direction_vacancy,direction_employee,direction_date,direction_status) SELECT'{applicantId}','{vacancyID}','{port.empIds}','{now.ToString("yyyy-MM-dd")}','Ожидание' WHERE NOT EXISTS ( SELECT 1 FROM direction WHERE direction_aplicant = '{applicantId}' AND direction_vacancy = '{vacancyID}' AND  direction_status = 'Ожидание');");
                MessageBox.Show(
              "Направление успешно создано",
              "Уведомление"
              );
                SeeResume resume = new SeeResume();
                resume.Show();
                this.Close();
            }
        }

        void click_to(object sender, EventArgs e)
        {
            string vacancyProfession = dataGridView1.Rows[currentRowIndex].Cells["Профессия"].Value.ToString();
            int vacancyID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            SeeResume seeResume = new SeeResume(vacancyProfession, vacancyID);
            seeResume.Show();
            this.Hide();
            dataGridView1.Rows[currentRowIndex].Selected = false;
        }
        void update(object sender, EventArgs e)
        {
            int vacancyID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            AddV addV = new AddV(0, vacancyID);
            addV.Show();
            this.Close();

        }
        void delete(object sender, EventArgs e)
        {
            int vacancyID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            DialogResult results = MessageBox.Show(
               "Вы действительно хотите удалить вакансию?",
               "Подтверждение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information
               );
            if (results == DialogResult.Yes)
            {
                func.direction($@"UPDATE vacancy
                             SET vacancy_delete_status = 1
                             WHERE id = {vacancyID}");
                MessageBox.Show("Ваканисия успешно удалена", "Уведомление");
                load_load();
            }

        }
        private void SeeVacancyNew_Load(object sender, EventArgs e)
        {
            if (resume != 0)
            {
                //string searcNow = $@"SELECT resume.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_delete_status as 'Status'
                //        FROM resume  
                //        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                //        INNER JOIN profession ON resume.resume_profession = profession.id 
                //        WHERE resume.id = '{resume}'";
                //func.load(dataGridView2, searcNow);
                //dataGridView2.Columns["id"].Visible = false;
                //dataGridView2.Columns["Status"].Visible = false;
                //dataGridView2.Columns["Соискатель"].Width = 270;
                //dataGridView2.Columns["Профессия"].Width = 250;
                //dataGridView2.Columns["Знание языков"].Width = 208;
                //dataGridView2.Columns["Личностные качества"].Width = 300;
                //dataGridView2.Columns["Зарплата"].Width = 140;

                //dataGridView2.Visible = true;
            }



            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");
            comboBox2.Items.Add("Без фильтра");
            List<string> list = new List<string>();
            MySqlConnection connection = new MySqlConnection(Connection.connect());
            connection.Open();
            string find = $"SELECT name FROM profession;";
            MySqlCommand com = new MySqlCommand(find, connection);
            var pwd = "";
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {

                if (!comboBox2.Items.Contains(reader[0].ToString()))
                    comboBox2.Items.Add(reader[0].ToString());
            }
            connection.Close();

            comboBox1.Items.Add("По возрастанию зарплаты");
            comboBox1.Items.Add("По убыванию зарплаты");
            load_load();
            if (resume == 0)
            {

                allPageCount = Math.Ceiling(countRecordsVacancy / 20);
                editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
                label5.Text = allPageCount.ToString();
                if (allPageCount > 10)
                    textBox1.MaxLength = 2;
                if (allPageCount > 100)
                    textBox1.MaxLength = 3;
                else
                    textBox1.MaxLength = 4;
            }
        }
        void editPage(double countRecords, double countRecordsBD, Label label2, Label label5, TextBox textBox1, double allPageCount, int page, DataGridView dataGridView)
        {

            dataGridView.CurrentCell = null;

            int countRow = 20;
            int startRow = 20 * (page - 1);
            int endRow = startRow + countRow;

            for (int i = 0; i < countRecords; i++)
            {
                if (i < startRow || i > endRow)
                {
                    if (dataGridView.Rows.Count > i)
                    {
                        dataGridView.Rows[i].Visible = false;
                    }
                }
                else
                {
                    if (dataGridView.Rows.Count > i)
                    {
                        dataGridView.Rows[i].Visible = true;
                    }
                    
                }

            }
            double nowCount = 20;
            if (page * 20 > countRecords)
                nowCount = -1 * (20 * (page - 1) - countRecords);
            label2.Text = $"{nowCount} из  {countRecordsBD}";
            label5.Text = allPageCount.ToString();
            if (nowCount == 0)
                page = 0;
            textBox1.Text = page.ToString();

        }
        void change()
        {
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1" || status == "2")
                    row.Visible = false;
            }
            manager.ResumeBinding();
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["Cсылка"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["Компания"] = new DataGridViewLinkCell();
                    DataGridViewLinkCell c = r.Cells["Компания"] as DataGridViewLinkCell;
                    c.LinkColor = Color.Green;

                }
            }
            //dataGridView1.Columns["Компания"].Width = 160;
            //dataGridView1.Columns["Профессия"].Width = 200;
            //dataGridView1.Columns["Обязанности"].Width = 340;
            //dataGridView1.Columns["Требования"].Width = 270;
            //dataGridView1.Columns["Условия"].Width = 270;
            //dataGridView1.Columns["Размер зарплаты"].Width = 150;
            //dataGridView1.Columns["Адресс работы"].Width = 310;
            dataGridView1.Columns["id"].Visible = false;


            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;

            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            AdminC adminC = new AdminC();
            adminC.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (pageVacancy > 1)
                pageVacancy--;
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (countRecordsVacancy > pageVacancy * 20)
                pageVacancy++;
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns.Remove("Специфика");
            func.load(dataGridView1, Search());
            dataGridView1.Columns.Add("Специфика", "Специфика");
            done();
            dataGridView1.ClearSelection();
            change();

            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns.Remove("Специфика");
            func.load(dataGridView1, Search());
            dataGridView1.Columns.Add("Специфика", "Специфика");
            done();
            dataGridView1.ClearSelection();
            change();
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns.Remove("Специфика");
            func.load(dataGridView1, Search());
            dataGridView1.Columns.Add("Специфика", "Специфика");
            done();
            dataGridView1.ClearSelection();
            change();
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            menu(sender, e);

        }
    }
}
