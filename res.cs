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
    public partial class res : Form
    {
        int idVacancy;
        int idResumeID;
        int applicantID;
        string path;

        string searchIn;
        string roleEmp;
        public res(int vacancyID=0,int resumeID = 0)
        {
            idVacancy = vacancyID;
            idResumeID = resumeID;
            InitializeComponent();

            roleEmp = func.search($"SELECT employe_post FROM employe WHERE id = {port.empIds}");
            searchIn = $@"SELECT CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель', profession.name as 'Профессия', resume_education, resume_work_experience, resume.resume_knowledge_of_languages as 'Знание языков', resume.resume_personal_qualities as 'Личностные качества', resume.salary as 'Зарплата', applicant.applicant_image, CONCAT(applicant.applicant_phone_number, '^',applicant.applicant_address, '^', applicant.applicant_date_of_birth, '^'), resume.resume_applicant 
                         FROM resume  
                         INNER JOIN applicant ON resume.resume_applicant = applicant.applicant_id 
                         INNER JOIN profession ON resume.resume_profession = profession.id 
                         WHERE resume.id = {idResumeID}";

            if (roleEmp == "2")
            {
                if (idVacancy > 0)
                {
                    button1.Visible = true;
                }
                else
                {
                    button2.Visible = true;
                }
            } 
            
        }
            //SELECT direction.*, profession.name FROM agent.direction
            //inner join vacancy on direction_vacancy = vacancy.id
            //inner join profession on vacancy_profession = profession.id;

        private void res_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(Connection.connect());
            connection.Open();
            MySqlCommand command = new MySqlCommand(searchIn, connection);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                labelFIO.Text = reader[0].ToString();
                labelProfession.Text = reader[1].ToString();
                labelEducate.Text = reader[2].ToString();
                labelWorkExp.Text = reader[3].ToString().Replace(": ","\n");
                labelLang.Text = reader[4].ToString().Replace(", ", "\n");
                labelQual.Text = reader[5].ToString().Replace(", ", "\n");
                labelSalary.Text = reader[6].ToString()+" рублей";
                labelPesonal.Text = reader[8].ToString().Replace("^", "\n");
                path = reader[7].ToString();
                    if (path == "")
                    {
                        path = "default_user.png";
                    }
                
                try
                {
                    pictureBox2.Image = Image.FromFile($@"..\..\photo\{path}");
                }
                catch
                {
                    string pathError = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    pictureBox2.Image = Image.FromFile(pathError + $@"\photo\{path}");
                }
                applicantID = Convert.ToInt32(reader[9].ToString());
            }
                

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Создать направление с этим резюме?",
               "Подтверждение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information
               );
            if (result == DialogResult.Yes)
            {
                DateTime now = DateTime.Now;
                func.direction($"INSERT INTO direction(direction_aplicant,direction_vacancy,direction_employee,direction_date,direction_status) SELECT'{applicantID}','{idVacancy}','{port.empIds}','{now.ToString("yyyy-MM-dd")}','Ожидание' WHERE NOT EXISTS ( SELECT 1 FROM direction WHERE direction_aplicant = '{applicantID}' AND direction_vacancy = '{idVacancy}' AND  direction_status = 'Ожидание');");
                MessageBox.Show(
               "Направление успешно создано",
               "Уведомление"
               );
                SeeVacancy seeVacancy = new SeeVacancy();
                seeVacancy.Show();
                this.Close();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            
                this.Close();
            
            
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            SeeVacancy seeVacancy = new SeeVacancy(idResumeID,labelProfession.Text);
            seeVacancy.Show();
            this.Close();
        }

        private void res_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void labelProfession_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void res_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }
    }
}
