using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using WordApp = Microsoft.Office.Interop.Word;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;

namespace Agent
{
    public partial class AddC : Form
    {
        int componys;
        string name;
        string description;
        string adress;
        string link;
        string phoneNumber;
        string cost;
        int flag;

        string wordCompany;
        string wordDate;
        int wordCost;

        public AddC(int compony =0)
        {
            InitializeComponent();
            componys = compony;
        }
        void checkEnableUpdate()
        {
            if (flag == 1)
            {
                var count = 0;
                if (textBoxAdress.Text != adress)
                    count++;
                if (textBoxDesc.Text != description)
                    count++;
                if (textBoxLink.Text != link)
                    count++;
                if (textBoxName.Text != name)
                    count++;
                if (maskedTextBoxPhoneNumber.Text != phoneNumber)
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
        void checkEnable()
        {
            if (flag == 0)
            {
                var count = 0;
                if (textBoxDesc.Text.Length > 0)
                    count++;
                if (textBoxName.Text.Length > 0)
                    count++;
                if (textBoxAdress.Text.Length > 0)
                    count++;
                if (maskedTextBoxPhoneNumber.MaskFull)
                    count++;
                if (textBox1.Text.Length > 0)
                    count++;
                if (count >= 5)
                {
                    buttonAddS.Enabled = true;
                }
                else
                {
                    buttonAddS.Enabled = false;
                }
            }
        }
        public System.Data.DataTable returnDate(string query, System.Data.DataTable dataTable)
        {
            MySqlConnection connection = new MySqlConnection(Connection.connect());
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.SelectCommand.ExecuteNonQuery();
            adapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        private void exportToWord(System.Data.DataTable dataTable)
        {
            var wordApp = new WordApp.Application();
            try
            {
                string exePath = Assembly.GetEntryAssembly().Location;
                string baseDir = Path.GetDirectoryName(exePath);
                baseDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
                string docPath = Path.Combine(baseDir, "document", "cost.docx");


                WordApp.Document doc = null;

                try
                {
                    doc = wordApp.Documents.Open(docPath, ReadOnly: false);

                    // Fetch data from database
                    string wordCompany = func.search($@"SELECT company_name,company_vacancy_cost FROM agent.company WHERE company.id = {func.search("SELECT id FROM agent.company ORDER BY company.id DESC LIMIT 1;")}");
                    int wordCost = Convert.ToInt32(func.search($@"SELECT company_vacancy_cost FROM agent.company WHERE company.id = {func.search("SELECT id FROM agent.company ORDER BY company.id DESC LIMIT 1;")}"));

                    var replacements = new Dictionary<string, string>
        {
            { "<company>", wordCompany },
            { "<cost>", wordCost.ToString() },
            { "<Date>", DateTime.Now.ToString() }
        };

                    foreach (var item in replacements)
                    {
                        WordApp.Find find = wordApp.Selection.Find;
                        find.Text = item.Key;
                        find.Replacement.Text = item.Value;
                        object wrap = WordApp.WdFindWrap.wdFindContinue;
                        object replace = WordApp.WdReplace.wdReplaceAll;

                        find.Execute(
                            FindText: find.Text,
                            ReplaceWith: find.Replacement.Text,
                            Replace: replace,
                            Wrap: wrap,
                            Forward: true,
                            MatchCase: false,
                            MatchWholeWord: false,
                            MatchWildcards: false
                        );
                    }

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Word Documents|*.docx",
                        Title = "Сохранить документ",
                        FileName = "Отчет_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        doc.SaveAs2(saveFileDialog.FileName);
                        Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                }
                finally
                {
                    if (doc != null)
                    {
                        doc.Close(false);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                    }
                }
            }
            finally
            {
                if (wordApp != null)
                {
                    wordApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }
            }
            //}
            //catch (COMException ex)
            //{
            //    Console.WriteLine($"COM Exception: {ex.Message}, ErrorCode: {ex.ErrorCode}");
            //    // Более подробная обработка ошибки: проверка кода ошибки, логирование и т.д.
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"General Exception: {ex.Message}");
            //}
            //finally
            //{
            //    wordDoc.Visible = true;
            //    // Обязательно освобождаем COM-объекты!
            //    Marshal.ReleaseComObject(doc);
            //    // ... Освобождение других объектов ...
            //    GC.Collect(); // Для сборки мусора
            //    GC.WaitForPendingFinalizers();
            //}

        }
        private void AddC_Load(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            Pen p1 = new Pen(Color.Pink);
            g.DrawLine(p1, 0, 0, this.Width - 10, 0);
            g.DrawLine(p1, 0, 0, 0, this.Height - 10);
            g.DrawLine(p1, this.Width - 10, 0, this.Width - 10, this.Height - 10);
            g.DrawLine(p1, 0, this.Height - 10, Width - 10, this.Height - 10);
        

            if (componys != 0)
            {
                buttonAddS.Text = "Изменить";
                label1.Text = "Редактирование компании";
                MySqlConnection con = new MySqlConnection(Connection.connect());
                con.Open();
                string search = $"SELECT * FROM company WHERE id = {componys};";
                MySqlCommand comm = new MySqlCommand(search, con);
                MySqlDataReader readerr = comm.ExecuteReader();
                while (readerr.Read())
                {

                    textBoxName.Text = readerr[1].ToString();
                    textBoxDesc.Text = readerr[2].ToString();
                    maskedTextBoxPhoneNumber.Text = readerr[3].ToString();
                    textBoxAdress.Text = readerr[4].ToString();
                    textBoxLink.Text = readerr[5].ToString();
                    textBox1.Text = readerr[7].ToString();

                }
                name = textBoxName.Text;
                adress = textBoxAdress.Text;
                link = textBoxLink.Text;
                phoneNumber = maskedTextBoxPhoneNumber.Text;
                description = textBoxDesc.Text;
                cost = textBox1.Text;
                flag = 1;
            }
        }
        private void exit_Click(object sender, EventArgs e)
        {
            port.move = 1;
            AdminC adminC = new AdminC();
            adminC.Show();
            this.Close();
        }

        private void buttonAddS_Click(object sender, EventArgs e)
        {
            port.move = 1;
            name = textBoxName.Text;
            adress = textBoxAdress.Text;
            link = textBoxLink.Text;
            phoneNumber = maskedTextBoxPhoneNumber.Text;
            description = textBoxDesc.Text;
            

            if (componys == 0)
            {
                DialogResult result = MessageBox.Show(
                "Добавить компанию?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        func.direction($@"
                                INSERT INTO company (company_name, company_desceiption, company_phone_number, company_address, companyc_linq,company_vacancy_cost) 
                                SELECT '{name}', '{description}', '{phoneNumber}', '{adress}', '{link}',{textBox1.Text} 
                                WHERE NOT EXISTS (
                                    SELECT 1 FROM company 
                                    WHERE company_name = '{name}' AND company_phone_number = '{phoneNumber}' AND company_address = '{adress}' AND  (company_delete_status IS NULL OR company_delete_status = 3 OR company_delete_status = 4)
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
                    textBoxDesc.Clear();
                    textBoxAdress.Clear();
                    textBoxLink.Clear();
                    textBoxName.Clear();
                    textBox1.Clear();
                    maskedTextBoxPhoneNumber.Clear();

                    var dataTable = new System.Data.DataTable();

                    //;

                    string search = $@"SELECT company_name,company_vacancy_cost FROM agent.company Where company.id = {func.search("SELECT id FROM agent.company order by company.id desC limit 1;")};
                                       ";
                    returnDate(search, dataTable);
                    exportToWord(dataTable);

                    DialogResult results = MessageBox.Show(
                       "Создать вакансии?",
                       "Подтверждение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information
                       );
                    if (results == DialogResult.Yes)
                    {
                        int componyId = Convert.ToInt32(func.search($"SELECT id FROM company WHERE company_name = '{name}' and company_address = '{adress}' and company_phone_number = '{phoneNumber}'"));
                        AddV addV = new AddV(componyId);
                        addV.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                "Вы действительно хотите изменить компанию?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );
                if (result == DialogResult.Yes)
                {

                    string searchIn = $@"UPDATE company SET company_name = '{name}', company_desceiption = '{description}', company_phone_number = '{phoneNumber}', company_address = '{adress}', companyc_linq = '{link}', company_vacancy_cost = {textBox1.Text};";
                    
                    try
                    {
                        func.direction(searchIn);


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
                    MessageBox.Show("Запись успешно изменена", "Уведомление");
                    SeeVacancy seeVacancy = new SeeVacancy();
                    seeVacancy.Show();
                    this.Close();
                }
            }
            
            
            
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (componys == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxDesc_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (componys == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void maskedTextBoxPhoneNumber_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void textBoxAdress_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (componys == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBoxLink_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (componys == 0)
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

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.rus_end(e);
        }

        private void textBoxDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.rus_end(e);
        }

        private void textBoxLink_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.keyPressEn(e);
        }

        private void AddC_Paint(object sender, PaintEventArgs e)
        {
            func.FormPaint(this);
        }

        private void maskedTextBoxPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (componys == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            port.move = 1;
            if (componys == 0)
                checkEnable();
            else
            {
                checkEnableUpdate();
                checkEnable();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            func.salary(e);
        }
    }
}
