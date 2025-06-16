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

    public partial class seeDirection : Form
    {
        int currentRowIndex;
        int currentColumnIndex;
        string docPath;
        Size sizeStart;
        Size sizeButton;
        Size sizeButton1;
        Size sizeButton2;
        Point locationButton;
        Point locationButton1;
        Point locationButton2;
        Point locationStart;
        Point locationData;
        Point locationPictire;
        Point locationPictire1;
        Point locationPictire2;
        Point locationLabel2;
        Point locationLabel1;
        Point locationLabel;
        int hightRow;
        Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
        float fontRow;
        int widthData;
        int heightData;
        int procentHight;
        int procentWidth;
        int sizeFont = 0;
        bool statusForm = false;
        string stringSearch = $@"SELECT direction.id, CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель',profession.name as 'Профессия' ,  CONCAT(employe.employe_surname, ' ', employe.employe_name, ' ', employe.employe_partronymic) as 'Работник', direction.direction_date as 'Дата направления', direction.direction_status as 'Статус', direction.direction_delete_status as 'Delete' 
                                       FROM direction
                                       INNER JOIN applicant ON direction.direction_aplicant = applicant.applicant_id 
                                       INNER JOIN employe ON direction.direction_employee = employe.id 
                                       INNER JOIN vacancy on direction_vacancy = vacancy.id 
                                       INNER JOIN profession on vacancy_profession = profession.id 
                                       ORDER BY direction.direction_date DESC;";
        public seeDirection()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
           
           MenuManager m = new MenuManager();
            m.Show();
            this.Close();
        }
        void loadDate()
        {
            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");

            func.load(dataGridView1, stringSearch);
            dataGridView1.Columns["id"].Visible = false;
            

            //dataGridView1.Columns["Работник"].Width = 270;

            
            dataGridView1.Columns["Delete"].Visible = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status_delete = row.Cells["Delete"].Value.ToString();
                string status = row.Cells["Статус"].Value.ToString();
                if (status == "Ожидание")
                {
                    row.Cells["Статус"].Style.ForeColor = Color.FromArgb(218, 165, 32);
                    if (status_delete == "1")
                        row.Visible = false;
                }else if(status == "Принято")
                {
                    row.Cells["Статус"].Style.ForeColor = Color.Green;
                    if (status_delete == "1")
                        row.Visible = false;
                }
                if (statusForm)
                {
                    row.Height = procentHight * 4; // Установка высоты для каждой строки
                    row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
                }
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            if (!statusForm)
            {
                dataGridView1.Columns["Соискатель"].Width = 250;
                dataGridView1.Columns["Работник"].Width = 220;

            }

        }
        private void seeDirection_Load(object sender, EventArgs e)
        {
            if (resolution.Width > 1024 || resolution.Height > 768)
            {
                sizeFont = 12;
                if (resolution.Width > 1600 || resolution.Height > 900)
                {
                    sizeFont = 14;
                    if (resolution.Width > 1920 || resolution.Height > 1200)
                    {
                        sizeFont = 16;
                    }
                }
            }
            loadDate();
            procentHight = resolution.Height / 100;
            procentWidth = resolution.Width / 100;
            locationStart = this.Location;
            sizeStart = this.Size;
            locationData = dataGridView1.Location;
            widthData = dataGridView1.Width;
            heightData = dataGridView1.Height;

            locationButton = exit.Location;
            locationButton1 = button1.Location;
            locationButton2 = buttonAddS.Location;
            sizeButton = exit.Size;
            sizeButton1 = button1.Size;
            sizeButton2 = buttonAddS.Size;
            locationPictire = pictureBox4.Location;
            locationPictire1 = pictureBox2.Location;
            locationPictire2 = pictureBox3.Location;


            locationLabel = ladelHeader.Location;
            locationLabel1 = label1.Location;
            locationLabel2 = label2.Location;
            hightRow = dataGridView1.RowTemplate.Height;
            fontRow = 10;


        }
        void menu(object sender, MouseEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();
            this.currentColumnIndex = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                    contextMenu.MenuItems.Add(new MenuItem("Изменить запись", dir));
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
        void dir(object sender, EventArgs e)
        {
            int dirId = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value);
            string dirStatus = dataGridView1.Rows[currentRowIndex].Cells["Статус"].Value.ToString();
            if (dirStatus == "Ожидание") 
                port.haveStatus = "Ожидание";
            else if (dirStatus == "Принято")
                port.haveStatus = "Принято";
            else if (dirStatus == "Отклонено")
                port.haveStatus = "Отклонено";
            
            DirectionStatus status = new DirectionStatus(dirId);
            status.ShowDialog();
            port.status = "";

            loadDate();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            menu(sender, e);
        }

        private void buttonAddS_Click(object sender, EventArgs e)
        {
            word word = new word();
            word.ShowDialog();

        }

        private void seeDirection_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelFile excelFile = new ExcelFile();
            excelFile.ShowDialog();

        }

        private void seeDirection_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }
        public void Sizeble()
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
                dataGridView1.Location = new Point(procentWidth, procentWidth * 3);
                dataGridView1.Width = resolution.Width - procentWidth * 2;
                dataGridView1.Height = procentHight * 90;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = procentHight * 4; // Установка высоты для каждой строки
                    row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
                }
                dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont + 2);
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                exit.Location = new Point(resolution.Width - procentWidth * 10, procentHight * 91 + dataGridView1.Location.Y);
                exit.Size = new Size(procentWidth * 9, procentHight * 5);
                exit.Font = new Font(ladelHeader.Font.FontFamily, sizeFont + 2, FontStyle.Bold);

                button1.Location = new Point(procentWidth * 2 + buttonAddS.Size.Width, procentHight * 91 + dataGridView1.Location.Y);

                

                buttonAddS.Location = new Point(procentWidth, procentHight * 91 + dataGridView1.Location.Y);

                

                statusForm = true;
                pictureBox4.Location = new Point(resolution.Width - 27 - 10, 10);
                ladelHeader.Font = new Font(ladelHeader.Font.FontFamily, 22, FontStyle.Bold);

                buttonAddS.Location = new Point(procentWidth, procentHight * 91 + dataGridView1.Location.Y);

                label1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);
                label2.Font = new Font(ladelHeader.Font.FontFamily, sizeFont);

                pictureBox2.Location = new Point(button1.Width + button1.Location.X + procentWidth, button1.Location.Y + 15);
                pictureBox3.Location = new Point(button1.Width + button1.Location.X + procentWidth, button1.Location.Y + 15 + pictureBox2.Height + 10);
                label1.Location = new Point(pictureBox2.Location.X + procentWidth, pictureBox2.Location.Y);
                label2.Location = new Point(pictureBox3.Location.X + procentWidth, pictureBox3.Location.Y);
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
                exit.Font = new Font(ladelHeader.Font.FontFamily, 14, FontStyle.Bold);

                button1.Location = locationButton1;
                button1.Size = sizeButton1;
                button1.Font = new Font(ladelHeader.Font.FontFamily, 14, FontStyle.Bold);

                buttonAddS.Location = locationButton2;
                buttonAddS.Size = sizeButton2;
                buttonAddS.Font = new Font(ladelHeader.Font.FontFamily, 14, FontStyle.Bold);
                pictureBox4.Location = locationPictire;
                ladelHeader.Location = locationLabel;

                dataGridView1.Font = new Font(ladelHeader.Font.FontFamily, 12);
                ladelHeader.Font = new Font(ladelHeader.Font.FontFamily, 18, FontStyle.Bold);

                label1.Font = new Font(ladelHeader.Font.FontFamily, 10);
                label2.Font = new Font(ladelHeader.Font.FontFamily, 10);

                pictureBox2.Location = locationPictire1;
                pictureBox3.Location = locationPictire2;

                label1.Location = locationLabel1;
                label2.Location = locationLabel2;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = hightRow; // Установка высоты для каждой строки
                    row.DefaultCellStyle.Font = new Font(ladelHeader.Font.FontFamily, fontRow);
                }
            }

            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Sizeble();
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }
    }
}
