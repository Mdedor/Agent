using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace Agent
{
    

    public partial class ExcelFile : Form
    {
        public ExcelFile()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.MaxDate = dateTimePicker1.Value;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddS_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();

            try
            {
                // Открываем новую книгу
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1]; // Первый лист


                // Заполняем данные

                worksheet.Cells[1, 1].Value = "Название компании";
                worksheet.Cells[1, 2].Value = "Сумма полученная от компании";
                MySqlConnection connection = new MySqlConnection(Connection.connect());
                connection.Open();
                MySqlCommand command = new MySqlCommand($@"SELECT company_name, sum(company_vacancy_cost) FROM agent.direction
                INNER JOIN vacancy ON vacancy.id = direction_vacancy
                INNER JOIN company ON company.id = vacancy_company
                WHERE direction_status = 'Принято' and direction_date BETWEEN '{dateTimePicker3.Value.ToString("yyyy-MM-dd")}' AND  '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' GROUP BY company.id;", connection);
                MySqlDataReader reader = command.ExecuteReader();
                int row = 2;
                int allSum = 0;
                while (reader.Read())
                {
                    worksheet.Cells[row, 1].Value = reader.GetString(0); // company_name
                    worksheet.Cells[row, 2].Value = reader.GetDecimal(1); // sum(company_vacancy_cost)
                    row++;
                    allSum += reader.GetInt32(1);
                }
                
                connection.Close();
                // Пример данных
                row++;
                worksheet.Cells[row, 1].Value = "Итоговая выручка";
                worksheet.Cells[row, 2].Value = $"{allSum} рублей";
                // Авто-подгонка ширины столбцов
                worksheet.Columns.AutoFit();
                // Сохраняем файл (формат .xlsx для Excel 2007+)

                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"......"));
                string documentPath = Path.Combine(projectRoot, "file.xls");
                workbook.SaveAs(documentPath);
                

                // Закрываем Excel
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show($"Файл успешно сохранен: {documentPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                // Освобождаем COM-объекты
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {

        }

        private void ExcelFile_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker3.MaxDate = DateTime.Now;
        }
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime RegistrationDate { get; set; }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
