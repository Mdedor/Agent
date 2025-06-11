using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;

namespace Agent
{
    public partial class SeeResume : Form
    {
        int currentRowIndex;

        string professions;
        int idVacancy;
        string searcIn;
        string roleEmp;


        double countRecordsBDResume;
        double countRecordsResume;
        int pageResume = 1;

        string vacancyProfession = "0";
        string resumeProfession = "0";

        string searchNowCountResume;

        double allPageCountResume;

        int procentHight;
        int procentWidth;
        int sizeFont = 0;
        Size resolution;

        string docPath;
        Size sizeStart;
        Size sizeButton;
        Point locationTextBox1;
        Point locationComboBox1;
        Point locationComboBox2;

        Point locationButton;
        Point locationPanel;

        Point locationStart;
        Point locationData;
        Point locationPictire;
        Point locationLabel;
        int hightRow;
        float fontRow;
        int widthData;
        int heightData;
        bool statusForm = false;
        public SeeResume(string profession = " ", int VacancyID = 0) // ИЗМЕНИТЬ РАЗМЕР СТРАНИЦЫ!!!!!!!!!!!!!
        {
            professions = profession;
            InitializeComponent();
            idVacancy = VacancyID;
            searchNowCountResume = @"SELECT Count(*) FROM agent.resume 
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4)";
            countRecordsBDResume = func.records(searchNowCountResume);
            countRecordsResume = func.records(searchNowCountResume);
            roleEmp = func.search($"SELECT employe_post FROM employe WHERE id = {port.empIds}");
            searcIn = $@"SELECT resume.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_delete_status as 'Status', applicant_id
                        FROM resume  
                        INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                        INNER JOIN profession ON resume.resume_profession = profession.id
                        WHERE (applicant_delete_status is null or applicant_delete_status = 4)";
            allPageCountResume = Math.Ceiling(countRecordsResume / 20);
            if (roleEmp == "2")
            {
                if (idVacancy >= 0 && professions != " ")
                    searcIn += $"WHERE profession.name = '{professions}'";
                else
                {
                    textBoxSearch.Visible = true;
                    comboBox1.Visible = true;
                    comboBox2.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    label12.Visible = true;
                    label11.Text = $"{countRecordsResume} из  {countRecordsBDResume}";
                }
            }else if (roleEmp == "1") 
            { 

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
        
        void editColumnsResume()
        {
            try
            {
                dataGridView1.Columns.Remove("Зарплат");
            }
            catch
            {

            }

            dataGridView1.Columns.Add("Зарплат", "Зарплата");
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {

                string so = dataGridViewRow.Cells["Соискатель"].Value.ToString();


                dataGridViewRow.Cells["Соискатели"].Value = $"{so.ToUpper()}";

                dataGridViewRow.Cells["Зарплат"].Value = $"{dataGridViewRow.Cells["Зарплата"].Value.ToString()} руб";
            }
        }
        void loadResume()
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
            dataGridView1.Columns.Add("Соискатели", "Соискатели");
            func.load(dataGridView1, searcIn);

            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["Соискатель"].Visible = false;
            dataGridView1.Columns["Профессия"].Visible = true;
            dataGridView1.Columns["Знание языков"].Visible = true;
            dataGridView1.Columns["Знание языков"].Visible = false;
            dataGridView1.Columns["applicant_id"].Visible = false;
            dataGridView1.Columns["Зарплата"].Visible = false;

            dataGridView1.Columns["Зарплата"].Width = 140;


            editColumnsResume();

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
        private void resumeList_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Без сортировки");
            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");

            
            comboBox2.Items.Add("Без фильтра");
            comboBox1.Items.Add("По возрастанию зарплаты");
            comboBox1.Items.Add("По убыванию зарплаты");

            loadResume();
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView1);
            resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            procentHight = resolution.Height / 100;
             procentWidth = resolution.Width / 100;


            if (resolution.Width > 1024 || resolution.Height > 768)
            {
                sizeFont = 10;
                if (resolution.Width > 1600 || resolution.Height > 900)
                {
                    sizeFont = 12;
                    if (resolution.Width > 1920 || resolution.Height > 1200)
                    {
                        sizeFont = 14;
                    }
                }
            }

            locationStart = this.Location;
            sizeStart = this.Size;
            locationData = dataGridView1.Location;
            widthData = dataGridView1.Width;
            heightData = dataGridView1.Height;

            locationButton = exit.Location;
            sizeButton = exit.Size;
            locationPictire = pictureBox2.Location;
            locationLabel = ladelHeader.Location;
            hightRow = dataGridView1.RowTemplate.Height;
            fontRow = 10;
            locationTextBox1 = textBoxSearch.Location;
            locationComboBox1 = comboBox1.Location;
            locationComboBox2 = comboBox2.Location;
            locationPanel = panel1.Location;
        }
        void menu(object sender, MouseEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();

            this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                if (roleEmp == "3")
                {
                    contextMenu.MenuItems.Add(new MenuItem("Посмотреть подробную информацию", click_to));
                }else if (roleEmp == "2")
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
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView1);
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
            

        }

       

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            func.load(dataGridView1, Search());
            editColumnsResume();
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
            editColumnsResume();
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
            editColumnsResume();
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
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            
        }

        private void exit_Click_2(object sender, EventArgs e)
        {
            if (roleEmp == "3")
            {
                if (idVacancy >= 0 && professions != " ")
                {
                    SeeVacancy seeVacancy = new SeeVacancy();
                    seeVacancy.Show();
                    this.Close();
                }
                else
                {
                    MenuRecruter menuManager = new MenuRecruter();
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

        private void label10_Click(object sender, EventArgs e)
        {
            if (pageResume > 1)
                pageResume--;
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView1);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (countRecordsResume > pageResume * 20)
                pageResume++;
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView1);
        }

        private void textBoxSearch_TextChanged_1(object sender, EventArgs e)
        {
            func.load(dataGridView1, Search());
            dataGridView1.ClearSelection();
            editColumnsResume();
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView1);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = procentHight * 4; // Установка высоты для каждой строки
                row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
            }
            dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont + 2);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            func.load(dataGridView1, Search());
            dataGridView1.ClearSelection();
            editColumnsResume();
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView1);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = procentHight * 4; // Установка высоты для каждой строки
                row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
            }
            dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont + 2);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            func.load(dataGridView1, Search());
            dataGridView1.ClearSelection();
            editColumnsResume();
            editPage(countRecordsResume, countRecordsBDResume, label11, label8, textBox2, allPageCountResume, pageResume, dataGridView1);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = procentHight * 4; // Установка высоты для каждой строки
                row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
            }
            dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont + 2);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDown_1(object sender, MouseEventArgs e)
        {
            menu(sender, e);
        }

        private void SeeResume_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            func.FormPaint(this, Color.White);
            
            string exePath = Assembly.GetEntryAssembly().Location;
            // Переходим на несколько уровней вверх (например, из binDebug\netX.Y в корень проекта)
            string baseDir = Path.GetDirectoryName(exePath); // binDebug\netX.Y
            

            if (!statusForm)
            {

                this.Size = resolution;
                this.Location = new Point(0, 0);
                docPath = Path.Combine(baseDir, "photo", "mini.png");
                try
                {
                    pictureBox2.Image = Image.FromFile(docPath);
                }
                catch
                {
                    baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
                    docPath = Path.Combine(baseDir, "photo", "mini.png");
                    pictureBox2.Image = Image.FromFile(docPath);
                }
                textBoxSearch.Location = new Point(procentWidth, procentWidth * 3);
                comboBox1.Location = new Point(procentWidth + textBoxSearch.Width + textBoxSearch.Location.X, procentWidth * 3);
                comboBox2.Location = new Point(procentWidth + comboBox1.Location.X + comboBox1.Width, procentWidth * 3);

                dataGridView1.Location = new Point(procentWidth, textBoxSearch.Location.Y + textBoxSearch.Height + procentWidth);
                dataGridView1.Width = resolution.Width - procentWidth * 2;
                dataGridView1.Height = procentHight * 86;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = procentHight * 4; // Установка высоты для каждой строки
                    row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
                }
                dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont + 2);
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                exit.Location = new Point(resolution.Width - procentWidth * 10, procentHight * 87 + dataGridView1.Location.Y);
                exit.Size = new Size(procentWidth * 9, procentHight * 5);
                exit.Font = new Font(ladelHeader.Font.FontFamily, sizeFont + 2, FontStyle.Bold);
                statusForm = true;
                pictureBox2.Location = new Point(resolution.Width - 27 - 10, 10);
                ladelHeader.Font = new Font(ladelHeader.Font.FontFamily, 22, FontStyle.Bold);
                panel1.Location = new Point(procentWidth, dataGridView1.Height + dataGridView1.Location.Y + procentHight);
            }
            else
            {
                statusForm = false;

                this.Size = sizeStart;
                this.Location = locationStart;
                docPath = Path.Combine(baseDir, "photo", "fullsrcean.png");
                try
                {
                    pictureBox2.Image = Image.FromFile(docPath);
                }
                catch
                {
                    baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
                    docPath = Path.Combine(baseDir, "photo", "fullsrcean.png");
                    pictureBox2.Image = Image.FromFile(docPath);
                }
                dataGridView1.Height = heightData;
                dataGridView1.Width = widthData;
                dataGridView1.Location = locationData;
                exit.Location = locationButton;
                exit.Size = sizeButton;
                pictureBox2.Location = locationPictire;
                ladelHeader.Location = locationLabel;
                textBoxSearch.Location = locationTextBox1;
                comboBox1.Location = locationComboBox1;
                comboBox2.Location = locationComboBox2;
                panel1.Location = locationPanel;
                exit.Font = new Font(ladelHeader.Font.FontFamily, 14, FontStyle.Bold);
                dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, 12);
                ladelHeader.Font = new Font(ladelHeader.Font.FontFamily, 18, FontStyle.Bold);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = hightRow; // Установка высоты для каждой строки
                    row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, fontRow);
                }
            }

            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }
    }
}
