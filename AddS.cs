using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace Agent
{
    public partial class AddS : Form
    {
        string name;
        string surname;
        string patronomic;
        string adress;
        string phone;
        string image;
        string date;
        int gender;
        string photo;
        int empIds;
        string bdPhoto;
        string nowPhoto;
        int flag;
        int aplicantIds;
        int sees;
        string pathError;
        Random Random = new Random();
        public AddS(int aplicantId, int see=0)
        {
            InitializeComponent();
            aplicantIds = aplicantId;
            sees = see;
            if (sees != 0)
            {
                textBoxName.Enabled = false;
                textBoxSurname.Enabled = false;
                textBoxPatronomic.Enabled = false;
                textBoxAdress.Enabled = false;
                maskedTextBoxPhoneNumber.Enabled = false;
                comboBoxGender.Enabled = false;
                dateTimePicker1.Enabled = false;
                buttonImage.Visible = false;
                buttonAddS.Visible = false;
                label1.Text = "Подробная информация";
                label8.Visible = false;
            }

        }
        

        void checkEnable()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxSurname.Text.Length > 0)
                    count++;
                if (textBoxName.Text.Length > 0)
                    count++;
                if (textBoxPatronomic.Text.Length > 0)
                    count++;
                if (textBoxAdress.Text.Length > 0)
                    count++;
                if (maskedTextBoxPhoneNumber.MaskFull)
                    count++;


                if (count == 5)
                {
                    buttonAddS.Enabled = true;
                }
                else
                {
                    buttonAddS.Enabled = false;
                }
            }
        }
        void checkEnableUpdate()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxSurname.Text != surname)
                    count++;
                if (textBoxName.Text != name)
                    count++;
                if (textBoxPatronomic.Text != patronomic)
                    count++;
                if (textBoxAdress.Text != adress)
                    count++;
                if (maskedTextBoxPhoneNumber.Text != phone)
                    count++;
                if (bdPhoto != nowPhoto && nowPhoto != "default_user.png")
                    count++;

                if (count > 0)
                {
                    buttonAddS.Enabled = true;
                }
                else
                {
                    buttonAddS.Enabled = false;
                }
            }

        }
        private void buttonAddS_Click(object sender, EventArgs e)
        {
            pathError = Assembly.GetEntryAssembly().Location;
            string path;
            string baseDir = Path.GetDirectoryName(pathError);
            if (aplicantIds == 0)
            {
                name = textBoxName.Text;
                surname = textBoxSurname.Text;
                patronomic = textBoxPatronomic.Text;
                adress = textBoxAdress.Text;
                phone = maskedTextBoxPhoneNumber.Text;
                date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                if (comboBoxGender.SelectedIndex == 0)
                    gender = 1;
                else
                    gender = 2;
                DialogResult result = MessageBox.Show(
                "Добавить соискателя?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        func.direction($@"
                                INSERT INTO applicant (applicant_surname, applicant_name, applicant_patronymic, applicant_phone_number, applicant_address, applicant_date_of_birth, applicant_image, applicant_gender) 
                                SELECT '{surname}', '{name}', '{patronomic}', '{phone}', '{adress}', '{date}', '{photo}', {gender} 
                                WHERE NOT EXISTS (
                                    SELECT 1 FROM applicant 
                                    WHERE applicant_surname = '{surname}' 
                                    AND applicant_name = '{name}' 
                                    AND applicant_patronymic = '{patronomic}' 
                                    AND applicant_phone_number = '{phone}' 
                                    AND applicant_address = '{adress}' 
                                    AND applicant_date_of_birth = '{date}' 
                                    AND applicant_image = '{photo}' 
                                    AND applicant_gender = {gender} 
                                    AND (applicant_delete_status IS NULL OR applicant_delete_status = 3 OR applicant_delete_status = 4)
                                );
                            ");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Ошибка SQL: {ex.Message}, Number: {ex.Number}");
                        // Обработка специфических ошибок SQL, например, проверка номера ошибки (ex.Number)
                        // для определения типа ошибки (нарушение целостности, дубликат ключа и т.д.)
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show($"Ошибка операции с базой данных: {ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show($"Ошибка аргумента: {ex.Message}"); // Например, некорректный запрос
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Непредвиденная ошибка: {ex.Message}");
                    }
                    
                    textBoxName.Clear();
                    textBoxAdress.Clear();
                    textBoxPatronomic.Clear();
                    textBoxSurname.Clear();
                    maskedTextBoxPhoneNumber.Clear();
                    dateTimePicker1.MaxDate = DateTime.Now.AddYears(-14);
                    dateTimePicker1.Value = DateTime.Now.AddYears(-14);
                    comboBoxGender.SelectedIndex = 0;
                    try
                    {
                        var combinedPath = Path.Combine(baseDir, "photo", "default_user.png");
                        path = Path.GetFullPath(combinedPath);
                        pictureBox1.Image = Image.FromFile(path);
                    }
                    catch (Exception ex)
                    {

                        var combinedPath = Path.Combine(baseDir, "..\\..", "photo", "default_user.png");
                        path = Path.GetFullPath(combinedPath);
                        pictureBox1.Image = Image.FromFile(path);

                    }

                    MessageBox.Show("Запись успешно добавлена", "Уведомление");
                    DialogResult results = MessageBox.Show(
                       "Создать резюме?",
                       "Подтверждение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information
                       );
                    if (results == DialogResult.Yes)
                    {
                        int applicantId = Convert.ToInt32(func.search($"SELECT applicant_id FROM applicant WHERE (applicant_surname = '{surname}' and applicant_name = '{name}' and applicant_patronymic = '{patronomic}' and applicant_address = '{adress}' and applicant_phone_number = '{phone}')"));

                        AddR addR = new AddR(applicantId);
                        addR.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                DialogResult results = MessageBox.Show(
                   "Вы действительно хотите изменить соискателя?",
                   "Подтверждение",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Information
                   );
                if (results == DialogResult.Yes)
                {
                    
                    try
                    {
                        func.direction($@"UPDATE applicant
                             SET applicant_surname = '{textBoxSurname.Text}', applicant_name = '{textBoxName.Text}', applicant_patronymic = '{textBoxPatronomic.Text}', applicant_phone_number = '{maskedTextBoxPhoneNumber.Text}', applicant_address = '{textBoxAdress.Text}', applicant_date_of_birth = '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', applicant_image = '{photo}', applicant_gender = '{comboBoxGender.SelectedIndex + 1}'
                             WHERE applicant_id = {aplicantIds}");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Ошибка SQL: {ex.Message}, Number: {ex.Number}");
                        // Обработка специфических ошибок SQL, например, проверка номера ошибки (ex.Number)
                        // для определения типа ошибки (нарушение целостности, дубликат ключа и т.д.)
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show($"Ошибка операции с базой данных: {ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show($"Ошибка аргумента: {ex.Message}"); // Например, некорректный запрос
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Непредвиденная ошибка: {ex.Message}");
                    }
                    flag = 0;
                    
                    MessageBox.Show("Запись успешно изменена", "Уведомление");
                    SeeS seeS = new SeeS();
                    seeS.Show();
                    this.Close();

                } 
            }  
        }

        private void buttonImage_Click(object sender, EventArgs e)
        {
            string baseDir = Path.GetDirectoryName(pathError);
            string spath;
            port.move = 1;
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

                string filePath = openFileDialog1.FileName;
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length < (5 * 8 * 1024 * 1024))
                {
                    string[] path = filePath.Split('\\');
                string[] namePhoto = path[path.Length - 1].Split('.');

                DateTime dateTime = new DateTime();
                string photoName = BCrypt.Net.BCrypt.EnhancedHashPassword(dateTime.ToLongTimeString());
                photoName= photoName.Replace('.', 't');
                photoName= photoName.Replace('\\', 't');
                photoName= photoName.Replace('/', 't');
                photo = photoName + "." + namePhoto[namePhoto.Length-1];
                nowPhoto = photo;
                string newFilePath = $@"\photo\{photo}";
                try
                {
                    var combinedPath = Path.Combine(baseDir,  "photo", photo);
                    spath = Path.GetFullPath(combinedPath);
                    fileInfo.CopyTo(spath, true);
                }
                catch(Exception ex)
                {
                   
                    
                    var combinedPath = Path.Combine(baseDir, "..\\..", "photo", photo);
                     spath = Path.GetFullPath(combinedPath);
                    fileInfo.CopyTo(spath, true);

                }

                try
                {
                    var combinedPath = Path.Combine(baseDir,"photo", photo);
                    spath = Path.GetFullPath(combinedPath);
                    pictureBox1.Image = Image.FromFile(spath);
                    
                }
                catch (Exception ex)
                {
                    var combinedPath = Path.Combine(baseDir, "..\\..", "photo", photo);
                    spath = Path.GetFullPath(combinedPath);
                    pictureBox1.Image = Image.FromFile(spath);

                }
                
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Привышен размер фотографии, выберите другую фотографию");
                    photo = "";
                
            }
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void AddS_Load(object sender, EventArgs e)
        {
            comboBoxGender.Items.Add("Мужской");
            comboBoxGender.Items.Add("Женский");
            pathError = Assembly.GetEntryAssembly().Location;
            string path;
            string baseDir = Path.GetDirectoryName(pathError);

            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-14);

            var valueToSet = DateTime.Now.AddYears(-14);
            if (valueToSet < dateTimePicker1.MinDate)
                valueToSet = dateTimePicker1.MinDate;
            else if (valueToSet > dateTimePicker1.MaxDate)
                valueToSet = dateTimePicker1.MaxDate;

            dateTimePicker1.Value = valueToSet;
            flag = 1;
            if (aplicantIds == 0)
            {
                comboBoxGender.SelectedIndex = 0;
                try
                {
                    var combinedPath = Path.Combine(baseDir, "photo", "default_user.png");
                    path = Path.GetFullPath(combinedPath);

                    pictureBox1.Image = Image.FromFile(path);
                }
                catch (Exception ex)
                {
                    var combinedPath = Path.Combine(baseDir, "..\\..", "photo", "default_user.png");
                    path = Path.GetFullPath(combinedPath);
                    pictureBox1.Image = Image.FromFile(path);

                }
                
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {   
                if (sees == 0)
                {
                    buttonAddS.Text = "Изменить";
                    label1.Text = "Редактирование соискателя";
                }
                

                MySqlConnection connection = new MySqlConnection(Connection.connect());
                connection.Open();
                string find = $"SELECT * FROM applicant WHERE applicant_id = {aplicantIds};";
                MySqlCommand com = new MySqlCommand(find, connection);
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    surname = reader[1].ToString();
                    name = reader[2].ToString();
                    patronomic = reader[3].ToString();
                    phone = reader[4].ToString();
                    adress = reader[5].ToString();
                    date = reader[6].ToString();
                    bdPhoto = reader[7].ToString();
                    nowPhoto = reader[7].ToString();
                    

                    if (reader[7].ToString().Length > 0)
                        try
                        {
                           
                            path = Path.Combine(baseDir, "photo", $"{bdPhoto}");
                            pictureBox1.Image = Image.FromFile(path);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                            try
                            {
                                path = Path.Combine(baseDir, $@"..\..","photo",$"{bdPhoto}");
                                pictureBox1.Image = Image.FromFile(path);
                            }
                            catch
                            {

                                path = Path.Combine(baseDir, $@"photo", "default_user.png");
                                pictureBox1.Image = Image.FromFile(path);
                            }
                            

                        }
                    
                    else
                        try
                        {
                            path = Path.Combine(baseDir, $@"photo","default_user.png");
                            pictureBox1.Image = Image.FromFile(path);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                            path = Path.Combine(baseDir, $@"..\..","photo","default_user.png");
                            pictureBox1.Image = Image.FromFile(path);

                        }

                    if (reader[8].ToString() == "1")
                        comboBoxGender.SelectedIndex = 0;
                    else
                        comboBoxGender.SelectedIndex = 1;
                }
                textBoxSurname.Text = surname;
                textBoxName.Text = name;
                textBoxPatronomic.Text = patronomic;
                maskedTextBoxPhoneNumber.Text = phone;
                textBoxAdress.Text = adress;
                dateTimePicker1.Text = date;


                
                connection.Close();

            }
                            
        }
        
        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            func.keyPressRu(e);
            
        }

        private void textBoxPatronomic_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRu(e);
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressRu(e);
        }

        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void textBoxAdress_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (!string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                var words = textBoxSurname.Text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(words[i]))
                    {
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                    }
                }
                textBoxSurname.Text = string.Join(" ", words);
                textBoxSurname.SelectionStart = textBoxSurname.Text.Length;

                textBoxSurname.SelectionStart = textBoxSurname.Text.Length;
                textBoxSurname.SelectionLength = 0;
                textBoxSurname.TextChanged -= textBoxSurname_TextChanged;
                textBoxSurname.Text = textBoxSurname.Text;
                textBoxSurname.TextChanged += textBoxSurname_TextChanged;
                
             }
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }
        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

       

        private void exit_Click_1(object sender, EventArgs e)
        {
            if (aplicantIds == 0)
            {
                AdminS adminS = new AdminS();
                adminS.Show();
                this.Close();
            }
            else
            {
                SeeS seeS   = new SeeS();
                seeS.Show();
                this.Close();
            }
            
        }

        private void maskedTextBoxPhoneNumber_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void textBoxPatronomic_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (!string.IsNullOrWhiteSpace(textBoxPatronomic.Text))
            {
                var words = textBoxPatronomic.Text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(words[i]))
                    {
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                    }
                }
                textBoxPatronomic.Text = string.Join(" ", words);
                textBoxPatronomic.SelectionStart = textBoxPatronomic.Text.Length;

                textBoxPatronomic.SelectionStart = textBoxPatronomic.Text.Length;
                textBoxPatronomic.SelectionLength = 0;
                textBoxPatronomic.TextChanged -= textBoxPatronomic_TextChanged;
                textBoxPatronomic.Text = textBoxPatronomic.Text;
                textBoxPatronomic.TextChanged += textBoxPatronomic_TextChanged;
            }
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
                
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (!string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                var words = textBoxName.Text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(words[i]))
                    {
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                    }
                }
                textBoxName.Text = string.Join(" ", words);
                textBoxName.SelectionStart = textBoxName.Text.Length;

                textBoxName.SelectionStart = textBoxName.Text.Length;
                textBoxName.SelectionLength = 0;
                textBoxName.TextChanged -= textBoxName_TextChanged;
                textBoxName.Text = textBoxName.Text;
                textBoxName.TextChanged += textBoxName_TextChanged;
            }
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }

        }

        private void textBoxAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.address(e);
        }

        private void AddS_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this, Color.FromArgb(213, 213, 213));
        }

        private void maskedTextBoxPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnable();
                checkEnableUpdate();

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void AddS_MouseMove(object sender, MouseEventArgs e)
        {
            port.move = 1;
        }
    }
}
