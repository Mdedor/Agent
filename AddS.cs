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
        int flag;
        int aplicantIds;
        Random Random = new Random();
        public AddS(int aplicantId)
        {
            InitializeComponent();
            aplicantIds = aplicantId;
            

        }
        string pathError = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

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
                if (bdPhoto != pictureBox1.ToString())
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
                    dateTimePicker1.Value = new DateTime(2006, 01, 01);
                    comboBoxGender.SelectedIndex = 0;
                    try
                    {
                        pictureBox1.Image = Image.FromFile($@"..\..\photo\default_user.png");
                    }
                    catch
                    {

                        pictureBox1.Image = Image.FromFile(pathError+$@"\photo\default_user.png");
                        throw;
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
                string newFilePath = $@"\photo\{photo}";
                try
                {
                    fileInfo.CopyTo($@"..\.." + $"{newFilePath}", true);
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    fileInfo.CopyTo($@"{newFilePath}", true);

                }

                try
                {
                    pictureBox1.Image = Image.FromFile($@"..\..\" + $"{newFilePath}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    pictureBox1.Image = Image.FromFile(pathError + $@"{newFilePath}");

                }
                
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Привышен размер фотографии, выберите другую фотографию");
                    photo = "";
                
            }
        }

        private void AddS_Load(object sender, EventArgs e)
        {
            comboBoxGender.Items.Add("Мужской");
            comboBoxGender.Items.Add("Женский");
            if (aplicantIds == 0)
            {
                comboBoxGender.SelectedIndex = 0;
                try
                {
                    pictureBox1.Image = Image.FromFile($@"..\..\photo\default_user.png");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    pictureBox1.Image = Image.FromFile(pathError + $@"\photo\default_user.png");

                }
                
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                buttonAddS.Text = "Изменить";
                label1.Text = "Редактирование соискателя";

                MySqlConnection connection = new MySqlConnection(Connection.con);
                connection.Open();
                string find = $"SELECT * FROM applicant WHERE applicant_id = {aplicantIds};";
                MySqlCommand com = new MySqlCommand(find, connection);
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    textBoxSurname.Text = reader[1].ToString();
                    textBoxName.Text = reader[2].ToString();
                    textBoxPatronomic.Text = reader[3].ToString();
                    maskedTextBoxPhoneNumber.Text = reader[4].ToString();
                    textBoxAdress.Text = reader[5].ToString();
                    dateTimePicker1.Text = reader[6].ToString();
                    bdPhoto = reader[7].ToString();
                    if (reader[7].ToString().Length > 0)
                        try
                        {
                            pictureBox1.Image = Image.FromFile($@"..\..\photo\{reader[7].ToString()}.png");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                            pictureBox1.Image = Image.FromFile(pathError + $@"\photo\{reader[7].ToString()}.png");

                        }
                    
                    else
                        try
                        {
                            pictureBox1.Image = Image.FromFile($@"..\..\photo\default_user.png");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                            pictureBox1.Image = Image.FromFile(pathError + $@"\photo\default_user.png");

                        }

                    if (reader[8].ToString() == "1")
                        comboBoxGender.SelectedIndex = 0;
                    else
                        comboBoxGender.SelectedIndex = 1;
                }
                name = textBoxName.Text;
                surname = textBoxSurname.Text;
                patronomic = textBoxPatronomic.Text;
                adress = textBoxAdress.Text;
                date = dateTimePicker1.Value.ToString();
                phone = maskedTextBoxPhoneNumber.Text.ToString();
                connection.Close();
                flag = 1;
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
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxAdress_TextChanged(object sender, EventArgs e)
        {
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
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
                checkEnableUpdate();
                checkEnable();
            }
        }
        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
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
                checkEnableUpdate();
                checkEnable();
            }
                
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
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
                checkEnableUpdate();
                checkEnable();
            }

        }

        private void textBoxAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.address(e);
        }

        private void AddS_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }

        private void maskedTextBoxPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (aplicantIds == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }
    }
}
