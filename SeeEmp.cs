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
using System.Xml.Linq;
using MySql.Data.MySqlClient;
namespace Agent
{
    public partial class SeeEmp : Form
    {
        int employId;

        int currentRowIndex;
        int currentColumnIndex;
       
        public SeeEmp()
        {
            InitializeComponent();
        }

        private void SeeEmp_Load(object sender, EventArgs e)
        {
            labelFIO.Text = func.search($"SELECT CONCAT(employe_surname, ' ', employe_name, ' ', employe_partronymic) FROM employe WHERE id = '{port.empIds}'");
            load();
            dataGridView1.Columns["ФИО"].Width = 270;
            dataGridView1.Columns["Номер телефона"].Width = 170;
            dataGridView1.Columns["Адрес"].Width = 310;
            dataGridView1.Columns["Логин"].Width = 170;
            dataGridView1.Columns["Пароль"].Width = 200;
            dataGridView1.Columns["Роль"].Width = 140;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            
        }
        void load()
        {
            func.load(dataGridView1, $@"SELECT employe.id, CONCAT(employe.employe_surname,' ', employe.employe_name,' ', employe.employe_partronymic) as 'ФИО', employe.employe_phone_number as 'Номер телефона', employe.employe_adress as 'Адрес', employe.employe_login as 'Логин', employe.employe_pwd as 'Пароль', post.posts as 'Роль', employe.employe_delete_status as 'Status' 
                                        FROM employe 
                                        INNER JOIN post ON employe.employe_post = post.id");
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;
            CurrencyManager manager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            manager.SuspendBinding();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Status"].Value.ToString();
                if (status == "1")
                    row.Visible = false;

            }
            manager.ResumeBinding();
        }
        
        void menu(object sender, MouseEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();
            this.currentColumnIndex = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.MenuItems.Add(new MenuItem("Редактировать сотрудника", update));
                contextMenu.MenuItems.Add(new MenuItem("Удалить сотрудника", delete));
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
            employId = Convert.ToInt32( dataGridView1.Rows[currentRowIndex].Cells["id"].Value.ToString());
            AddE addE = new AddE(employId);
            addE.Show();
            this.Close();
            
        }
       
        void delete(object sender, EventArgs e)
        {
            employId = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["id"].Value.ToString());
            DialogResult results = MessageBox.Show(
                "Вы действительно хотите удалить сотрудника?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
            if (results == DialogResult.Yes)
            {
                func.direction($@"UPDATE employe
                             SET employe_delete_status = 1
                             WHERE id = {employId}");

                load();
                
            }
        }
        
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            menu(sender, e);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            AdminE adminE = new AdminE();
            adminE.Show();
            this.Close();
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SeeEmp_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
