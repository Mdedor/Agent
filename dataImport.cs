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
using System.IO;
namespace Agent
{
    public partial class dataImport : Form
    {
        string filePath;
        int buttonChek = 0;
        int statuss;
        public dataImport(int status = 0)
        {
            InitializeComponent(); // Код переписать, сделать проверку на уникальность!!!!
            statuss = status;
        }

        void change()
        {
            int count = 0;
            if (comboBoxTables.SelectedIndex == -1)
                count++;
            if (buttonChek != 0)
            {
                count++;
            }
            if (count == 2)
                buttonAddS.Enabled = true;
        }

        private void ExportToCsv()
        {
            if (comboBoxTables.SelectedItem == null)
            {
                MessageBox.Show("Выберите таблицу для экспорта");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
            saveFileDialog.Title = "Экспорт в CSV";
            saveFileDialog.FileName = $"{comboBoxTables.SelectedItem}_export.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string tableName = comboBoxTables.SelectedItem.ToString();
                    string idColumn = func.search(@"SELECT COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_SCHEMA = 'agent' 
                            AND TABLE_NAME = 'post' LIMIT 1;");
                    string query = $@"CREATE TEMPORARY TABLE `tmp_table` SELECT* FROM `{tableName}`;
                    ALTER TABLE `tmp_table` DROP column {idColumn};
                    SELECT* FROM `tmp_table`;
                    DROP TABLE `tmp_table`;";



                    using (MySqlConnection con = new MySqlConnection(Connection.connect()))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            con.Open();
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                // Создаем список для хранения строк
                                List<string> lines = new List<string>();

                                // Получаем названия столбцов
                                List<string> columnNames = new List<string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    columnNames.Add(reader.GetName(i));
                                }

                                // Добавляем заголовок с названиями столбцов
                                lines.Add(string.Join(";", columnNames));

                                // Читаем данные
                                while (reader.Read())
                                {
                                    List<string> values = new List<string>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Type fieldType = reader.GetFieldType(i);
                                        if (fieldType == typeof(DateTime))
                                        {
                                            DateTime dateValue = reader.GetDateTime(i);
                                            values.Add(reader.IsDBNull(i) ? "NULL" : dateValue.ToString("yyyy-MM-dd"));
                                        }
                                        else
                                        {
                                            values.Add(reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString());
                                        }

                                    }
                                    lines.Add(string.Join(";", values));
                                }

                                // Записываем в файл
                                File.WriteAllLines(saveFileDialog.FileName, lines);
                            }
                        }
                    }

                    MessageBox.Show($"Данные из таблицы {tableName} успешно экспортированы в файл: {saveFileDialog.FileName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте: {ex.Message}");
                }
            }
        }
        private void dataImport_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(Connection.connect());
            connection.Open();

            string find = "USE agent;SHOW tables;";
            MySqlCommand com = new MySqlCommand(find, connection);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                comboBoxTables.Items.Add(reader[0].ToString());
            }
            connection.Close();
            if (statuss == 0)
            {

            }
            else
            {
                label6.Text = "Экспорт данных";
                buttonAddS.Text = "Экспортировать";
                button1.Visible = false;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            includeAdmin includeAdmin = new includeAdmin();
            includeAdmin.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Image files|*.csv;|All files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            buttonChek = 1;
            filePath = openFileDialog1.FileName.ToString();
            change();
        }

        private void buttonAddS_Click_1(object sender, EventArgs e)
        {
            if (statuss == 0)
            {
                string[] readText = File.ReadAllLines(filePath);
                string[] valField;
                string[] titleField = readText[0].Split(';');
                string tableName = comboBoxTables.SelectedItem.ToString();

                // Создаем соединение
                MySqlConnection con = new MySqlConnection(Connection.connect());
                con.Open();

                int totalInserted = 0;

                foreach (string str in readText.Skip(1).ToArray())
                {
                    valField = str.Split(';');

                    // Создаем условие для проверки дубликатов (предполагаем, что все поля должны совпадать)

                    string whereCondition = string.Join(" AND ", titleField.Select((field, index) => $"{field} = '{valField[index]}'"));

                    string checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE {whereCondition}";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, con);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        // Если дубликата нет, выполняем вставку
                        string insertQuery = $"INSERT INTO {tableName}({string.Join(",", titleField)}) VALUES (";
                        for (int i  = 0; i < valField.Length; i++)
                        {
                            if (valField[i].ToString() == "NULL")
                                insertQuery += $"{valField[i].ToString()}";
                            else
                                insertQuery += $"'{valField[i].ToString()}'";
                            int q = i;
                            if (valField.Length - (q + 1) != 0)
                            {
                                insertQuery += ",";
                            }
                        }
                        insertQuery += ");";
                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, con);
                        totalInserted += insertCmd.ExecuteNonQuery();
                    }
                }

                con.Close();

                MessageBox.Show($"Импортировано {totalInserted} записей в {tableName}. Пропущено {readText.Length - 1 - totalInserted} дубликатов.");
            }
            else
            {
                ExportToCsv();
            }
            
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            change();
        }
    }
}
