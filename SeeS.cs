using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace Agent
{
    public partial class SeeS : Form
    {
        int currentRowIndex;
        int currentColumnIndex;
        int applicantID;

        string searchStering;
        bool resume;
        public SeeS(bool res = false)
        {
            InitializeComponent();
            resume = res;
            if (!resume)
            {
                searchStering = "SELECT  applicant_id,CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', applicant.applicant_phone_number as 'Номер телефона', applicant.applicant_address as 'Адресс', applicant.applicant_date_of_birth as 'Дата рождения', applicant.applicant_image, gender.genders as 'Пол', applicant.applicant_delete_status as 'Status'" +
                                                "FROM applicant " +
                                                "INNER JOIN gender ON applicant.applicant_gender = gender.id";
            }
            else
            {
                ladelHeader.Text = "Выберете соискателя для создания резюме";
                searchStering = @"SELECT  applicant_id,CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', applicant.applicant_phone_number as 'Номер телефона', applicant.applicant_address as 'Адресс', applicant.applicant_date_of_birth as 'Дата рождения', applicant.applicant_image, gender.genders as 'Пол', applicant.applicant_delete_status as 'Status' FROM applicant INNER JOIN gender ON applicant.applicant_gender = gender.id WHERE applicant_id NOT IN (SELECT resume_applicant  FROM resume);";
                
                
            }
            
        }
        void menu(object sender, MouseEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();
            this.currentColumnIndex = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {           
                if (resume == false)
                {
                    contextMenu.MenuItems.Add(new MenuItem("Редактировать соискателя", update));
                    contextMenu.MenuItems.Add(new MenuItem("Удалить соискателя", delete));

                }
                else
                {
                    contextMenu.MenuItems.Add(new MenuItem("Создать резюме", resum));
                }
                if (currentRowIndex >= 0)
                {
                    dataGridView1.Rows[currentRowIndex].Selected = true;
                    contextMenu.Show(dataGridView1, new Point(e.X, e.Y));
                }
                else
                {
                    currentRowIndex = 0;
                    dataGridView1.Rows[currentRowIndex].Selected = false;

                }
            }
  
        }
        void update(object sender, EventArgs e)
        {
            applicantID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["applicant_id"].Value.ToString());
            AddS addS = new AddS(applicantID);
            addS.Show();
            this.Close();
           
        }
        void resum(object sender, EventArgs e)
        {
            AddR addR = new AddR(Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["applicant_id"].Value.ToString()));
            addR.Show();
            this.Close();


        }
        void delete(object sender, EventArgs e)
        {
            applicantID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["applicant_id"].Value.ToString());
            DialogResult results = MessageBox.Show(
                "Вы действительно хотите удалить соискателя?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
            if (results == DialogResult.Yes)
            {
                func.direction($@"UPDATE applicant
                             SET applicant_delete_status = 1
                             WHERE applicant_id = {applicantID}");
                func.direction($"DELETE FROM resume WHERE resume_applicant ={applicantID}");
                dataGridView1.Columns.Remove("Изображение");
                load();
            }
            
        }
        
        void load()
        {
            func.load(dataGridView1, searchStering);


            dataGridView1.Columns["applicant_id"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["applicant_image"].Visible = false;


            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Изображение";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(imageColumn);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns["Адресс"].Width = 400;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string path = row.Cells["applicant_image"].Value.ToString();
                if (path == "")
                {
                    path = "default_user.png";
                }
                try
                {
                    row.Cells["Изображение"].Value = Image.FromFile($@"..\..\photo\{path}");
                }
                catch
                {
                    string pathError = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    row.Cells["Изображение"].Value = Image.FromFile(pathError+$@"\photo\{path}");
                }

            }
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
        private void SeeS_Load(object sender, EventArgs e)
        {
            load();
            dataGridView1.Columns["Соискатель"].Width = 340;
            dataGridView1.Columns["Номер телефона"].Width = 200;
            dataGridView1.Columns["Адресс"].Width = 400;
            dataGridView1.Columns["Дата рождения"].Width = 140;
            dataGridView1.Columns["Изображение"].Width = 250;
            dataGridView1.Columns["Пол"].Width = 120;
            
        }

        private void exit_Click(object sender, EventArgs e)
        {
            AdminS adminS = new AdminS();
            adminS.Show();
            this.Close();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            menu(sender, e);
        }

        

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SeeS_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
