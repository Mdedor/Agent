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
    public partial class SeeCompany : Form
    {
        int currentRowIndex;
        int currentColumnIndex;
        string roleEmp;
        int pos;
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
            AdminC adminC = new AdminC();
            adminC.Show();
            this.Close();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            menu(sender,e);
        }

        private void SeeCompany_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }
    }
}
