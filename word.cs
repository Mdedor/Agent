using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;
using System.IO;
using WordApp = Microsoft.Office.Interop.Word;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
namespace Agent
{
    public partial class word : Form
    {
        string startDate;
        string endDate;
        int count;
        int cool;
        int bad;
        int load;
        double percent;
        
        public word()
        {
            InitializeComponent();

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
        private void word_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker3.MaxDate = DateTime.Now;
            
            count = 0;
            cool = 0;
            bad = 0;
            load = 0;
            percent = 0.0;
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void exit_Click(object sender, EventArgs e)
        {
           this.Close();
        }
        private void exportToWord(System.Data.DataTable dataTable)
        {
            var wordDoc = new WordApp.Application();
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path += "\\doc.docx";
            Document doc = wordDoc.Documents.Open(path, ReadOnly: false);
            try
            {
                
                
                doc.Content.End = doc.Content.End; //не обязательно, но для ясности кода
                doc.Content.Select(); //Выделение курсора в конце

                // Пример добавления текста в конец:


                Range r = doc.Content;
                if (dataTable.Rows.Count > 0)
                {
                    Range rng = wordDoc.Selection.Range;
                    rng.Collapse(WdCollapseDirection.wdCollapseEnd); // или wdCollapseStart
                    r.Collapse(0);

                    WordApp.Table table = wordDoc.Application.ActiveDocument.Tables.Add(r, dataTable.Rows.Count + 1, dataTable.Columns.Count);
                    table.Borders.OutsideLineStyle = WordApp.WdLineStyle.wdLineStyleSingle;
                    table.Borders.InsideLineStyle = WordApp.WdLineStyle.wdLineStyleSingle;
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        table.Cell(1, i + 1).Range.Text = dataTable.Columns[i].ColumnName;
                    }
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        { table.Cell(i + 2, j + 1).Range.Text = dataTable.Rows[i][j].ToString(); }
                    }
                }
                doc.Content.InsertAfter("<nowDate>");
                var Items = new Dictionary<string, string>
            {
                { "<startDate>",startDate},
                { "<endDate>",endDate},
                { "<count>",count.ToString()},
                { "<cool>",cool.ToString()},
                { "<bad>",bad.ToString()},
                { "<percent>",percent.ToString()},
                { "<load>",load.ToString()},
                { "<nowDate>",DateTime.Now.ToString()}

            };


                WordApp.Find find = wordDoc.Selection.Find;
                foreach (var item in Items)
                {

                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;
                    object wrap = WdFindWrap.wdFindContinue;
                    object replace = WdReplace.wdReplaceAll;
                    find.Execute(FindText: find.Text, ReplaceWith: find.Replacement.Text, Replace: replace, Wrap: wrap, MatchCase: false, MatchWholeWord: false, MatchWildcards: false);
                }

            }
            catch (COMException ex)
            {
                Console.WriteLine($"COM Exception: {ex.Message}, ErrorCode: {ex.ErrorCode}");
                // Более подробная обработка ошибки: проверка кода ошибки, логирование и т.д.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
            }
            finally
            {
                wordDoc.Visible = true;
                // Обязательно освобождаем COM-объекты!
                Marshal.ReleaseComObject(doc);
                // ... Освобождение других объектов ...
                GC.Collect(); // Для сборки мусора
                GC.WaitForPendingFinalizers();
            }
            
        }
        private void buttonAddS_Click(object sender, EventArgs e)
        {
            startDate = dateTimePicker3.Value.ToString("d");
            endDate = dateTimePicker1.Value.ToString("d");
            string sss = $"SELECT COUNT(*) FROM agent.direction WHERE direction_date BETWEEN '{dateTimePicker3.Value.ToString("yyyy-MM-dd")}' AND '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}'";
            count = Convert.ToInt32(func.search(sss));
            cool = Convert.ToInt32(func.search(sss+ " AND direction_status = 'Принято'"));
            bad = Convert.ToInt32(func.search(sss + " AND direction_status = 'Отклонено'"));
            load = Convert.ToInt32(func.search(sss + " AND direction_status = 'Ожидание'"));
            if (cool!=0)
                percent = cool * 100 / count;
            
            
            var dataTable = new System.Data.DataTable();
            string search = $@"SELECT  CONCAT(applicant.applicant_surname, ' ',applicant.applicant_name, ' ', applicant.applicant_patronymic) as 'Соискатель',profession.name as 'Профессия' , direction.direction_date as 'Дата направления', direction.direction_status as 'Статус'
                                       FROM direction
                                       INNER JOIN applicant ON direction.direction_aplicant = applicant.applicant_id 
                                       INNER JOIN employe ON direction.direction_employee = employe.id 
                                       INNER JOIN vacancy on direction_vacancy = vacancy.id 
                                       INNER JOIN profession on vacancy_profession = profession.id 
                                        WHERE direction.direction_date BETWEEN '{dateTimePicker3.Value.ToString("yyyy-MM-dd")}' AND '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' 
                                       ORDER BY direction.direction_date DESC 
                                       ";
            returnDate(search, dataTable);
            exportToWord(dataTable);
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.MaxDate = dateTimePicker1.Value;
        }
    }
}
