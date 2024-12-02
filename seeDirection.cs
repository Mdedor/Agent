using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            MenuManager menuManager = new MenuManager();
            menuManager.Show();
            this.Close();
        }
        void loadDate()
        {
            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");
            func.load(dataGridView1, stringSearch);
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Соискатель"].Width = 270;
            dataGridView1.Columns["Профессия"].Width = 250;
            dataGridView1.Columns["Работник"].Width = 270;
            dataGridView1.Columns["Дата направления"].Width = 100;
            dataGridView1.Columns["Статус"].Width = 100;

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
                }
            }
        }
        private void seeDirection_Load(object sender, EventArgs e)
        {
            loadDate();
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
            func.FormPaint(this);
        }
    }
}
