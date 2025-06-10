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
        string docPath;
        string searchStering;
        bool resume;
        Size sizeStart;
        Size sizeButton;
        Point locationButton;
        Point locationStart;
        Point locationData;
        Point locationPictire;
        Point locationLabel;
        int hightRow;
        float fontRow;
        int widthData;
        int heightData;
        bool statusForm = false;
        public SeeS(bool res = false)
        {
            InitializeComponent();
            resume = res;
            if (!resume)
            {
                searchStering = "SELECT  applicant_id,CONCAT(applicant.applicant_surname, ' ',LEFT(applicant.applicant_name,1), '.', LEFT(applicant.applicant_patronymic,1),'.') as 'Соискатель', applicant.applicant_phone_number as 'Номер телефона', applicant.applicant_address as 'Адресс', applicant.applicant_image, applicant.applicant_delete_status as 'Status' " +
                                                "FROM applicant " +
                                                "INNER JOIN gender ON applicant.applicant_gender = gender.id";
            }
            else
            {
                ladelHeader.Text = "Выберете соискателя для создания резюме";
                searchStering = @"SELECT  applicant_id,CONCAT(applicant.applicant_surname, ' ',LEFT(applicant.applicant_name,1), '.', LEFT(applicant.applicant_patronymic,1),'.') as 'Соискатель', applicant.applicant_phone_number as 'Номер телефона', applicant.applicant_address as 'Адресс', applicant.applicant_image, applicant.applicant_delete_status as 'Status' FROM applicant INNER JOIN gender ON applicant.applicant_gender = gender.id WHERE applicant_id NOT IN (SELECT resume_applicant  FROM resume);";
                
                
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
                    contextMenu.MenuItems.Add(new MenuItem("Посмотреть подробную информацию", see));
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
        void see(object sender, EventArgs e)
        {
            applicantID = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["applicant_id"].Value.ToString());
            AddS addS = new AddS(applicantID,1);
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
                    try
                    {
                        string pathError = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                        row.Cells["Изображение"].Value = Image.FromFile(pathError + $@"\photo\{path}");
                    }
                    catch {
                        row.Cells["Изображение"].Value = Image.FromFile($@"..\..\photo\default_user.png");
                    }
                    
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
                row.Cells["Адресс"].Value = row.Cells["Адресс"].Value.ToString().Split(',')[0];
              row.Cells["Номер телефона"].Value = row.Cells["Номер телефона"].Value.ToString().Substring(0, 2)+" (***) ***" +row.Cells["Номер телефона"].Value.ToString().Substring(12);
                if (status == "1" || status == "3")
                    row.Visible = false;
            }
            manager.ResumeBinding();
        }
        private void SeeS_Load(object sender, EventArgs e)
        {
            load();
            // Поднимаемся на 3 уровня вверх
                                                                         // Добавляем относительный путь к документу
            
            dataGridView1.Columns["Соискатель"].Width = 340;
            dataGridView1.Columns["Номер телефона"].Width = 200;
            dataGridView1.Columns["Адресс"].Width = 400;
            //if (resume)
            //{
            //    dataGridView1.Columns["Пол"].Width = 120;
            //    dataGridView1.Columns["Дата рождения"].Width = 140;
            //}
            locationStart = this.Location;
            sizeStart = this.Size;
            locationData = dataGridView1.Location;
            widthData = dataGridView1.Width;
            heightData = dataGridView1.Height;
            dataGridView1.Columns["Изображение"].Width = 250;
            locationButton = exit.Location;
            sizeButton = exit.Size;
            locationPictire = pictureBox2.Location;
            locationLabel = ladelHeader.Location;
            hightRow = dataGridView1.RowTemplate.Height;
            fontRow = 10;
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
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void SeeS_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }

       


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int sizeFont=0;
            func.FormPaint(this, Color.White);
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            if (resolution.Width >1024 || resolution.Height > 768)
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
            string exePath = Assembly.GetEntryAssembly().Location;
            // Переходим на несколько уровней вверх (например, из binDebug\netX.Y в корень проекта)
            string baseDir = Path.GetDirectoryName(exePath); // binDebug\netX.Y
            baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
            int procentHight = resolution.Height /100;
            int procentWidth = resolution.Width /100;
            if (!statusForm)
            {

                this.Size = resolution;
                this.Location = new Point(0, 0);
                docPath = Path.Combine(baseDir, "photo", "mini.png");
                pictureBox2.Image = Image.FromFile(docPath);
                dataGridView1.Location = new Point(procentWidth, procentWidth*3);
                dataGridView1.Width = resolution.Width - procentWidth*2;
                dataGridView1.Height = procentHight * 90;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = procentHight * 10; // Установка высоты для каждой строки
                    row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
                }
                dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont+2);
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                
                exit.Location = new Point(resolution.Width - procentWidth * 10, procentHight * 91+dataGridView1.Location.Y);
                exit.Size = new Size(procentWidth*9,procentHight*5);
                statusForm = true;
                pictureBox2.Location = new Point(resolution.Width -27-10,10);
                ladelHeader.Font = new Font(ladelHeader.Font.FontFamily, 22,FontStyle.Bold);
            }
            else { 
                statusForm = false;

                this.Size = sizeStart;
                this.Location = locationStart;
                docPath = Path.Combine(baseDir, "photo", "fullsrcean.png");
                pictureBox2.Image = Image.FromFile(docPath);
                dataGridView1.Height = heightData;
                dataGridView1.Width = widthData;
                dataGridView1.Location = locationData;
                exit.Location=locationButton;
                exit.Size = sizeButton;
                pictureBox2.Location = locationPictire;
                ladelHeader.Location = locationLabel;
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
