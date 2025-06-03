using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Point = System.Drawing.Point;

namespace Agent
{
    public partial class SeeVacancy : Form
    {

        int currentRowIndex;
        int currentColumnIndex;
        string searcIn = $@"SELECT resume.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_delete_status as 'Status', applicant_id
                        FROM resume  
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4)";
        string roleEmp;
        string searchIn;
        string stingSearchingVacancy;
        string stingSearchingResume;
        int resume;
        string profession;
        int applicantId;

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
        //ИЗМЕНИТЬ ВСЮ СТРАНИЧКУ, ДИЗАЙН ГОВНО!! КОМУ Я ЭТО ПИШУ ?! Самому себе из будующего)) тоже самое с формой просмотра резюме!! да и со всем формами, пересмотреть дизайн
        public SeeVacancy(int idResume = 0, string profession = "")
        {
            InitializeComponent();
            searchNowCountVacancy = "SELECT count(*) FROM vacancy INNER JOIN company ON vacancy.vacancy_company = company.id " +
                                    "INNER JOIN profession ON vacancy.vacancy_profession = profession.id  " +
                                    "where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4)";
            countRecordsVacancy = func.records(searchNowCountVacancy);
            countRecordsBDVacancy = func.records(searchNowCountVacancy);

            searchNowCountResume = @"SELECT Count(*) FROM agent.resume 
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4)";
            countRecordsBDResume = func.records(searchNowCountResume);
            countRecordsResume = func.records(searchNowCountResume);

            resume = idResume;
            roleEmp = func.search($"SELECT employe_post FROM employe WHERE id = {port.empIds}");

            searchIn = $@"SELECT vacancy.id, company.company_name as 'Компания', profession.name as 'Профессия', vacancy.vacancy_responsibilities as 'Обязанности', vacancy.vacancy_requirements as 'Требования', vacancy.vacancy_conditions as 'Условия',   CONCAT( vacancy.vacancy_salary_by, ' - ', vacancy.vacancy_salary_before) as 'Размер зарплаты ₽', vacancy.vacancy_delete_status as 'Status', companyc_linq as 'Cсылка'   
                        FROM vacancy 
                        INNER JOIN company ON vacancy.vacancy_company = company.id 
                        INNER JOIN profession ON vacancy.vacancy_profession = profession.id
                        where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4)";
            if (roleEmp == "3")
            {
                if (resume == 0)
                {
                    textBoxSearch.Visible = true;
                    comboBox1.Visible = true;
                    comboBox2.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = $"{countRecordsVacancy} из  {countRecordsBDVacancy}";

                    label11.Text = $"{countRecordsResume} из  {countRecordsBDResume}";
                }
                else
                {
                    applicantId = Convert.ToInt32(func.search($"SELECT resume_applicant FROM resume WHERE id = {idResume}"));
                    searchIn += $" AND (profession.name = '{profession}') ";
                }

            }

        }
        void editColumnsResume()
        {
            try
            {
                dataGridView2.Columns.Remove("Зарплат");
            }
            catch
            {

            }
            
            dataGridView2.Columns.Add("Зарплат", "Зарплата");
            foreach (DataGridViewRow dataGridViewRow in dataGridView2.Rows)
            {

                string so = dataGridViewRow.Cells["Соискатель"].Value.ToString();
                string pro = dataGridViewRow.Cells["Профессия"].Value.ToString();
                string zn = dataGridViewRow.Cells["Знание языков"].Value.ToString();
                string ka = dataGridViewRow.Cells["Личностные качества"].Value.ToString();
                dataGridViewRow.Cells["Соискатели"].Value = $"{so.ToUpper()}\n{pro}\n{zn}\n{ka}";
                dataGridView2.Columns["Зарплат"].Width = 100;
                dataGridViewRow.Cells["Зарплат"].Value = $"{dataGridViewRow.Cells["Зарплата"].Value.ToString()} руб";
            }
        }
        void loadComboBoxPRofession()
        {
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
        }
        void loadResume(string search)
        {
            loadComboBoxPRofession();

            dataGridView2.Columns.Add("Соискатели", "Соискатели");
            func.load(dataGridView2, search);

            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["Status"].Visible = false;
            dataGridView2.Columns["Соискатель"].Visible = false;
            dataGridView2.Columns["Профессия"].Visible = false;
            dataGridView2.Columns["Знание языков"].Visible = false;
            dataGridView2.Columns["Личностные качества"].Visible = false;
            dataGridView2.Columns["applicant_id"].Visible = false;
            dataGridView2.Columns["Зарплата"].Visible = false;


            dataGridView2.Refresh();
            editColumnsResume();

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView2.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1" || status == "3")
                    row.Visible = false;

            }
            manager.ResumeBinding();
            allPageCountResume = Math.Ceiling(countRecordsResume / 20);

            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);
            label5.Text = allPageCountResume.ToString();
            if (allPageCountResume > 10)
                textBox1.MaxLength = 2;
            if (allPageCountResume > 100)
                textBox1.MaxLength = 3;
            else
                textBox1.MaxLength = 4;
        }

        string Search()
        {
            if (flag == 1)
            {
                string searchNowCount = "SELECT count(*) FROM vacancy INNER JOIN company ON vacancy.vacancy_company = company.id INNER JOIN profession ON vacancy.vacancy_profession = profession.id  where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4)";
                string basis = "SELECT vacancy.id, company.company_name as 'Комапния', profession.name as 'Профессия', vacancy.vacancy_responsibilities as 'Обязанности', vacancy.vacancy_requirements as 'Требования', vacancy.vacancy_conditions as 'Условия', CONCAT( vacancy.vacancy_salary_by, ' - ', vacancy.vacancy_salary_before) as 'Размер зарплаты',  vacancy.vacancy_delete_status as 'Status',  companyc_linq as 'Cсылка' " +
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
                if (resumeProfession != "0")
                {

                    basis += $" (profession.name = '{resumeProfession}') ";
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    basis += $"ORDER BY vacancy.vacancy_salary_by";
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    basis += $"ORDER BY vacancy.vacancy_salary_by DESC";
                }else if (comboBox1.SelectedIndex == 0)
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
            else
            {
                // ГДЕ СУКА ПРОВЕРКА НА УДАЛЕНЫЕ Applicant!???????????
                string searchNowCount = @"SELECT Count(*) FROM agent.resume INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4)";
                string basis = $@"SELECT resume.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_delete_status as 'Status', applicant_id FROM resume  
                                INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                                INNER JOIN profession ON resume.resume_profession = profession.id   
                                WHERE (applicant_delete_status is null or applicant_delete_status = 4)"; ;
                //if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedIndex != 0 || textBoxSearch.Text.Length > 0 || resumeProfession != "0")
                //{
                //    basis += "WHERE ";
                //}
                if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedIndex != 0)
                {
                    basis += $" and (profession.name = '{comboBox2.SelectedItem}')";
                    searchNowCount += $" and (profession.name = '{comboBox2.SelectedItem}')";

                }
                if (resumeProfession != "0")
                {
                    basis += $"  and (profession.name = '{resumeProfession}') ";
                    searchNowCount += $"AND profession.name = '{vacancyProfession}'";
                }
                if (textBoxSearch.Text.Length > 0)
                {
                    
                    basis += $" AND  (CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) LIKE '%{textBoxSearch.Text}%' OR resume.resume_personal_qualities LIKE '%{textBoxSearch.Text}%' OR resume.resume_knowledge_of_languages LIKE '%{textBoxSearch.Text}%')";
                    searchNowCount += $" and (CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) LIKE '%{textBoxSearch.Text}%' OR resume.resume_personal_qualities LIKE '%{textBoxSearch.Text}%' OR resume.resume_knowledge_of_languages LIKE '%{textBoxSearch.Text}%')";
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    basis += $"ORDER BY resume.salary";
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    basis += $"ORDER BY resume.salary DESC";
                }
                countRecordsResume = func.records(searchNowCount);
                label11.Text = $"{countRecordsResume} из {countRecordsBDResume}";
                countRecordsBDResume = func.records(searchNowCount);
                pageResume = 1;
                allPageCountResume = Math.Ceiling(countRecordsResume / 20);
                label8.Text = allPageCountResume.ToString();
                editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);
                return basis;
            }

        }

        void editColumnVacancy()
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {


                string obz = dataGridViewRow.Cells["Обязанности"].Value.ToString();
                string tr = dataGridViewRow.Cells["Требования"].Value.ToString();
                string us = dataGridViewRow.Cells["Условия"].Value.ToString();



                dataGridViewRow.Cells["Специфика"].Value = $"{obz}\n{tr}\n{us}\n";

            }
        }
        void load_load()
        {


            func.load(dataGridView1, searchIn);
            dataGridView1.Columns.Add("Специфика", "Специфика");
            editColumnVacancy();

            dataGridView1.Columns["id"].Visible = false;


            dataGridView1.Columns["Обязанности"].Visible = false;
            dataGridView1.Columns["Требования"].Visible = false;
            dataGridView1.Columns["Условия"].Visible = false;




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
        private void SeeVacancy_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");
            comboBox2.Items.Add("Без фильтра");
            comboBox1.Items.Add("Без сортировки");
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
            loadResume(searcIn);
            if (resume == 0)
            {

                allPageCountVacancy = Math.Ceiling(countRecordsVacancy / 20);
                editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
                label8.Text = allPageCountVacancy.ToString();
                if (allPageCountVacancy > 10)
                    textBox1.MaxLength = 2;
                if (allPageCountVacancy > 100)
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
                    r.Cells["Комапния"] = new DataGridViewLinkCell();
                    DataGridViewLinkCell c = r.Cells["Комапния"] as DataGridViewLinkCell;
                    c.LinkColor = Color.Green;

                }
            }

            editColumnVacancy();
            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;
        }
        void menuResume(object sender, MouseEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();

            this.currentRowIndex = dataGridView2.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                if (vacancyID == 0)
                {
                    contextMenu.MenuItems.Add(new MenuItem("Посмотреть подробную информацию", clickToResume));
                    contextMenu.MenuItems.Add(new MenuItem("Выбрать вакансию", clickToClient));
                }
                else
                {
                    contextMenu.MenuItems.Add(new MenuItem("Создать направление", dir2));
                }
                //else if (roleEmp == "1")
                //{
                //    contextMenu.MenuItems.Add(new MenuItem("Редактировать резюме", update));
                //    contextMenu.MenuItems.Add(new MenuItem("Удалить резюме", delete));
                //}

                if (currentRowIndex >= 0)
                {
                    dataGridView2.Rows[currentRowIndex].Selected = true;
                    contextMenu.Show(dataGridView2, new Point(e.X, e.Y));
                }
                else
                {
                    currentRowIndex = 0;


                }
            }

        }
        void clickToResume(object sender, EventArgs e)
        {
            int resume = Convert.ToInt32(dataGridView2.Rows[currentRowIndex].Cells["id"].Value);
            res res = new res(0, resume);
            res.Show();
            this.Hide();

        }
        void menuVacancy(object sender, MouseEventArgs e)
        {


            ContextMenu contextMenu = new ContextMenu();
            this.currentColumnIndex = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                if (roleEmp == "3")
                {
                    if (aplicantID ==0)
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
            vacancyID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            DialogResult result = MessageBox.Show(
               "Создать направление с этим резюме?",
               "Подтверждение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information
               );
            if (result == DialogResult.Yes)
            {


                DateTime now = DateTime.Now;
                func.direction($"INSERT INTO direction(direction_aplicant,direction_vacancy,direction_employee,direction_date,direction_status) SELECT'{aplicantID}','{vacancyID}','{port.empIds}','{now.ToString("yyyy-MM-dd")}','Ожидание' WHERE NOT EXISTS ( SELECT 1 FROM direction WHERE direction_aplicant = '{applicantId}' AND direction_vacancy = '{vacancyID}' AND  direction_status = 'Ожидание');");
                MessageBox.Show(
              "Направление успешно создано",
              "Уведомление"
              );
                restart();
            }
        }
        void dir2(object sender, EventArgs e)
        {
            aplicantID = Convert.ToInt32(dataGridView2.Rows[currentRowIndex].Cells["id"].Value);
            DialogResult result = MessageBox.Show(
              "Создать направление с этим резюме?",
              "Подтверждение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information
              );
            if (result == DialogResult.Yes)
            {
                DateTime now = DateTime.Now;
                func.direction($"INSERT INTO direction(direction_aplicant,direction_vacancy,direction_employee,direction_date,direction_status) SELECT'{aplicantID}','{vacancyID}','{port.empIds}','{now.ToString("yyyy-MM-dd")}','Ожидание' WHERE NOT EXISTS ( SELECT 1 FROM direction WHERE direction_aplicant = '{aplicantID}' AND direction_vacancy = '{vacancyID}' AND  direction_status = 'Ожидание');");
                MessageBox.Show(
               "Направление успешно создано",
               "Уведомление"
               );
                restart();
            }
        }
        void clickToClient(object sender, EventArgs e)
        {
             vacancyProfession = dataGridView2.Rows[currentRowIndex].Cells["Профессия"].Value.ToString();
             searchIn += $"AND profession.name = '{vacancyProfession}'";
            radioButton1.Checked = true;
            aplicantID = Convert.ToInt32(dataGridView2.Rows[currentRowIndex].Cells["id"].Value);

             searchNowCountVacancy += $"AND profession.name = '{vacancyProfession}'";
            countRecordsVacancy = func.records(searchNowCountVacancy);
            dataGridView1.Columns.Clear();

            load_load();
            pageVacancy = 1;
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
            
            //SeeResume seeResume = new SeeResume( vacancyProfession,vacancyID);
            //seeResume.Show();
            //this.Hide();
            //dataGridView1.Rows[currentRowIndex].Selected = false;
        }
        void click_to(object sender, EventArgs e)
        {
             resumeProfession = dataGridView1.Rows[currentRowIndex].Cells["Профессия"].Value.ToString();
            searcIn += $"AND profession.name = '{resumeProfession}'";

            radioButton2.Checked = true;

            vacancyProfession = dataGridView1.Rows[currentRowIndex].Cells["Профессия"].Value.ToString();
            vacancyID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            searchNowCountResume += $"AND profession.name = '{vacancyProfession}'";
            countRecordsResume = func.records(searchNowCountResume);
            dataGridView2.Columns.Clear();
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);

            loadResume(Search());
            pageResume = 1;
            
            //SeeResume seeResume = new SeeResume( vacancyProfession,vacancyID);
            //seeResume.Show();
            //this.Hide();
            //dataGridView1.Rows[currentRowIndex].Selected = false;
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
        private void SeeVacancy_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
        private void SeeVacancy_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void exit_Click_2(object sender, EventArgs e)
        {
            Auntification auntification = new Auntification();
            auntification.Show();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBoxSearch.Text = stingSearchingVacancy;

                if (comboBox1.SelectedIndex != -1)
                    comboBox1.SelectedIndex = idSortVacancy;
                if (comboBox2.SelectedIndex != -1)
                    comboBox2.SelectedIndex = idFilterVacancy;
            }
            else
            {
                flag = 2;
                stingSearchingVacancy = textBoxSearch.Text;

                if (comboBox1.SelectedIndex != -1)
                    idSortVacancy = comboBox1.SelectedIndex;
                if (comboBox2.SelectedIndex != -1)
                    idFilterVacancy = comboBox2.SelectedIndex;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBoxSearch.Text = stingSearchingResume;

                if (comboBox1.SelectedIndex != -1)
                    comboBox1.SelectedIndex = idSortResume;

                if (comboBox2.SelectedIndex != -1)
                    comboBox2.SelectedIndex = idFilterResume;
            }
            else
            {
                flag = 1;
                stingSearchingResume = textBoxSearch.Text;

                if (comboBox2.SelectedIndex != -1)
                    idFilterResume = comboBox2.SelectedIndex;
                if (comboBox1.SelectedIndex != -1)
                    idSortResume = comboBox1.SelectedIndex;


            }
        }

        private void textBoxSearch_TextChanged_1(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                dataGridView1.Columns.Remove("Специфика");
                func.load(dataGridView1, Search());
                dataGridView1.Columns.Add("Специфика", "Специфика");
                editColumnVacancy();
                dataGridView1.ClearSelection();
                change();
                editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);

            }
            else
            {
                func.load(dataGridView2, Search());
                dataGridView2.ClearSelection();
                editColumnsResume();
                editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (comboBox1.SelectedIndex != idSortVacancy )
                {
                    dataGridView1.Columns.Remove("Специфика");
                func.load(dataGridView1, Search());
                dataGridView1.Columns.Add("Специфика", "Специфика");
                editColumnVacancy();
                dataGridView1.ClearSelection();
                change();
                editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
                }

            }
            else
            {
                //if (comboBox1.SelectedIndex != idSortResume && comboBox2.SelectedIndex != idFilterResume)
                func.load(dataGridView2, Search());

                editColumnsResume();
                editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                //if (comboBox1.SelectedIndex != idSortVacancy && comboBox2.SelectedIndex != idFilterVacancy)
                //{
                dataGridView1.Columns.Remove("Специфика");
                func.load(dataGridView1, Search());
                dataGridView1.Columns.Add("Специфика", "Специфика");
                editColumnVacancy();
                dataGridView1.ClearSelection();
                change();
                editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
                //}

            }
            else
            {
                //if (comboBox1.SelectedIndex != idSortResume && comboBox2.SelectedIndex != idFilterResume)
                func.load(dataGridView2, Search());
                dataGridView1.ClearSelection();
                editColumnsResume();
                editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);

            }
        }

        private void dataGridView1_MouseDown_2(object sender, MouseEventArgs e)
        {
            radioButton1.Checked = true;
            menuVacancy(sender, e);
        }

        private void dataGridView2_MouseDown(object sender, MouseEventArgs e)
        {
            radioButton2.Checked = true;
            menuResume(sender,e);
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            if (pageVacancy > 1)
                pageVacancy--;
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

            if (countRecordsVacancy > pageVacancy * 20)
                pageVacancy++;
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (countRecordsResume > pageResume * 20)
                pageResume++;
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);

        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (pageResume > 1)
                pageResume--;
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string link = dataGridView1.Rows[e.RowIndex].Cells["Cсылка"].Value.ToString();
                if (link != "")
                {
                    try
                    {
                        System.Diagnostics.Process.Start(link);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ссылка указана не верно. Удалите или измените ссылку", "Ошибка");
                    }

                }

            }
        }

        void restart()
        {
            searcIn = $@"SELECT resume.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_delete_status as 'Status', applicant_id
                        FROM resume  
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id ";
            searchNowCountVacancy = "SELECT count(*) FROM vacancy INNER JOIN company ON vacancy.vacancy_company = company.id INNER JOIN profession ON vacancy.vacancy_profession = profession.id  where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4)";
            countRecordsVacancy = func.records(searchNowCountVacancy);
            countRecordsBDVacancy = func.records(searchNowCountVacancy);

            searchNowCountResume = @"SELECT Count(*) FROM agent.resume 
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4)";
            countRecordsBDResume = func.records(searchNowCountResume);
            countRecordsResume = func.records(searchNowCountResume);

            roleEmp = func.search($"SELECT employe_post FROM employe WHERE id = {port.empIds}");

            searchIn = $@"SELECT vacancy.id, company.company_name as 'Компания', profession.name as 'Профессия', vacancy.vacancy_responsibilities as 'Обязанности', vacancy.vacancy_requirements as 'Требования', vacancy.vacancy_conditions as 'Условия',   CONCAT( vacancy.vacancy_salary_by, ' - ', vacancy.vacancy_salary_before) as 'Размер зарплаты ₽', vacancy.vacancy_delete_status as 'Status', companyc_linq as 'Cсылка'   
                        FROM vacancy 
                        INNER JOIN company ON vacancy.vacancy_company = company.id 
                        INNER JOIN profession ON vacancy.vacancy_profession = profession.id
                        where (vacancy_delete_status IS NULL OR vacancy_delete_status = 4)";
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            vacancyProfession = "0";
            resumeProfession = "0";
            load_load();
            loadResume(searcIn);
            idFilterResume = 0;
            idFilterVacancy = 0;
            aplicantID = 0;
            vacancyID = 0;
            idSortResume = 0;
            idSortVacancy = 0;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = "Фильтрация";
            comboBox1.Text = "Сортировка";
            stingSearchingResume = "";
            stingSearchingVacancy = "";
            textBoxSearch.Text = "";

            pageResume = 1;
            pageVacancy = 1;
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView2);
            editPage(countRecordsVacancy, countRecordsBDVacancy, label2, label5, textBox1, allPageCountVacancy, pageVacancy, dataGridView1);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            restart();
        }

        private void посмотретьРезюмеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            currentRowIndex = e.RowIndex;
        }
    }
}
