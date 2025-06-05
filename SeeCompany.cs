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
    public partial class SeeCompany : Form
    {
        int currentRowIndex;
        int currentColumnIndex;
        string roleEmp;
        int pos;
        string docPath;
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
        public SeeCompany(int position=0)
        {
            InitializeComponent();
            pos = position;
        }

        private void SeeCompany_Load(object sender, EventArgs e)
        {
            roleEmp = func.search($"SELECT employe_post FROM employe WHERE id = {port.empIds}");
            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");
            func.load(dataGridView1, "SELECT id, company_name as 'Название', company_desceiption as 'Описание', company_phone_number as 'Номер для связи', company_address as 'Адресс расположения', companyc_linq as 'Cсылка', company_delete_status as 'Status' FROM company");
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Cсылка"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;

            dataGridView1.Columns["Название"].Width = 150;
            dataGridView1.Columns["Описание"].Width = 400;
            dataGridView1.Columns["Номер для связи"].Width = 200;
            dataGridView1.Columns["Адресс расположения"].Width = 350;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["Cсылка"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["Название"] = new DataGridViewLinkCell();
                    DataGridViewLinkCell c = r.Cells["Название"] as DataGridViewLinkCell;
                    c.LinkColor = Color.Green;
                    
                }
            }
            dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1")
                    row.Visible = false;

            }
            manager.ResumeBinding();

            locationStart = this.Location;
            sizeStart = this.Size;
            locationData = dataGridView1.Location;
            widthData = dataGridView1.Width;
            heightData = dataGridView1.Height;

            locationButton = button1.Location;
            sizeButton = button1.Size;
            locationPictire = pictureBox2.Location;
            locationLabel = ladelHeader.Location;
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
                if (pos == 0)
                {
                    contextMenu.MenuItems.Add(new MenuItem("Редактировать компанию", update));
                    contextMenu.MenuItems.Add(new MenuItem("Удалить компанию", delete));
                }
                else
                {
                    contextMenu.MenuItems.Add(new MenuItem("Создать вакансию", vaca));
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
        void update(object sender, EventArgs e)
        {
            int company = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value.ToString());
            AddC addC = new AddC(company);
            addC.Show();
            this.Close();
            
        }
        void vaca(object sender, EventArgs e)
        {
            int  company =Convert.ToInt32( dataGridView1.Rows[currentRowIndex].Cells["id"].Value.ToString());
            AddV addV = new AddV(company);
            addV.Show();
            this.Close();

        }
        void delete(object sender, EventArgs e)
        {
            string company = dataGridView1.Rows[currentRowIndex].Cells["id"].Value.ToString();
            DialogResult results = MessageBox.Show(
               "Вы действительно хотите удалить компанию? При удаление компании все вакансии этой компании будут УДАЛЕНЫ!!!!",
               "Подтверждение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information
               );
            if (results == DialogResult.Yes)
            {
                func.direction($@"UPDATE company
                             SET company_delete_status = 1
                             WHERE id = {company}");

                func.direction($@"UPDATE vacancy
                             SET vacancy_delete_status = 1
                             WHERE vacancy_company = {company}");
                MessageBox.Show("Компания и ее вакансии успешно удалены", "Уведомление");
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string link = dataGridView1.Rows[e.RowIndex].Cells["Cсылка"].Value.ToString();
                if (link !=  "")
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

        private void exit_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            menu(sender,e);
        }

        private void SeeCompany_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminC adminC = new AdminC();
            adminC.Show();
            this.Close();
        }

        private void SeeCompany_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int sizeFont = 0;
            func.FormPaint(this, Color.White);
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
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
            string exePath = Assembly.GetEntryAssembly().Location;
            // Переходим на несколько уровней вверх (например, из binDebug\netX.Y в корень проекта)
            string baseDir = Path.GetDirectoryName(exePath); // binDebug\netX.Y
            baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
            int procentHight = resolution.Height / 100;
            int procentWidth = resolution.Width / 100;
            if (!statusForm)
            {

                this.Size = resolution;
                this.Location = new Point(0, 0);
                docPath = Path.Combine(baseDir, "photo", "mini.png");
                pictureBox2.Image = Image.FromFile(docPath);
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

                button1.Location = new Point(resolution.Width - procentWidth * 10, procentHight * 91 + dataGridView1.Location.Y);
                button1.Size = new Size(procentWidth * 9, procentHight * 5);
                button1.Font = new Font(ladelHeader.Font.FontFamily, sizeFont+2, FontStyle.Bold);
                statusForm = true;
                pictureBox2.Location = new Point(resolution.Width - 27 - 10, 10);
                ladelHeader.Font = new Font(ladelHeader.Font.FontFamily, 22, FontStyle.Bold);
            }
            else
            {
                statusForm = false;

                this.Size = sizeStart;
                this.Location = locationStart;
                docPath = Path.Combine(baseDir, "photo", "fullsrcean.png");
                pictureBox2.Image = Image.FromFile(docPath);
                dataGridView1.Height = heightData;
                dataGridView1.Width = widthData;
                dataGridView1.Location = locationData;
                button1.Location = locationButton;
                button1 .Size = sizeButton;
                pictureBox2.Location = locationPictire;
                ladelHeader.Location = locationLabel;
                button1.Font = new Font(ladelHeader.Font.FontFamily, 14, FontStyle.Bold);
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
