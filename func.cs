using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Agent
{
    public class func
    {

        static public void keyPressRu(KeyPressEventArgs e)
        {
            
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]|[\b]|[-]|[\s]").Success)
            {
                e.Handled = true;
            }
        }
        static public void keyPressRuPlus(KeyPressEventArgs e)
        {

            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]|[\b]|[-]|[\s]|[.]|[,]|[0-9]").Success)
            {
                e.Handled = true;
            }
        }
        static public void keyPressEn(KeyPressEventArgs e)
        {

            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[a-zA-Z]|[\b]|[-]|[0-9]|[.]|[_]|[,]|[$]|[@]|[#]|[/]|[\\]").Success)
            {
                e.Handled = true;
            }
        }
        static public void salary(KeyPressEventArgs e)
        {

            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[\b]|[0-9]").Success)
            {
                e.Handled = true;
            }
        }
        static public void address(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '/' && e.KeyChar != '-')
            {
                e.Handled = true; // Предотвращаем ввод символа
            }
        }
        static public void rus_end(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true; // Предотвращаем ввод символа
            }
        }

        public static void FormPaint(Form form,Color color)
        {
            Graphics g = form.CreateGraphics();
            Pen p1 = new Pen(color,5);
            g.DrawLine(p1, 0, 0, form.Width - 1, 0);
            g.DrawLine(p1, 0, 0, 0, form.Height - 1);
            g.DrawLine(p1, form.Width - 1, 0, form.Width - 1, form.Height - 1);
            g.DrawLine(p1, 0, form.Height - 1, form.Width - 1, form.Height - 1);
        }
        static public void load(DataGridView gridView, string search)
        {
            
            MySqlConnection connection = new MySqlConnection(Connection.connect());
            connection.Open();
            MySqlCommand command = new MySqlCommand(search, connection);
            command.ExecuteNonQuery();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            gridView.DataSource = dataTable;
            connection.Close();
        }
        
        public static string search(string com)
        {
            MySqlConnection conn = new MySqlConnection(Connection.connect());
            conn.Open();
            MySqlCommand command = new MySqlCommand(com, conn);
            command.ExecuteScalar();
            var pwd = "$2a$12$FVR0VemGE//V5LB1LeHZsu0h6CmyCVD2px9YMTkH0gsSmY/3KDRfO";
            if (command.ExecuteScalar() == null)
            {
                return pwd;
            }
            else
            {
                pwd = command.ExecuteScalar().ToString();
            }

            conn.Close();
            return pwd;
        }
        public static void direction(string search)
        {
            MySqlConnection con = new MySqlConnection(Connection.connect());
            con.Open();
            MySqlCommand com = new MySqlCommand(search, con);

            port.directionStatus =com.ExecuteNonQuery();
            
            con.Close();
        }
        public static int records(string search) // написать try catch
        {
            int count = Convert.ToInt32(func.search(search));
            return count;
        }
        public static async void StartTimer()
        {
            Auntification auntification = new Auntification();
            int currentValue = Convert.ToInt32(ConfigurationManager.AppSettings["time"].ToString());
            TimeSpan ts = new TimeSpan(0, 0, currentValue);
            while (ts > TimeSpan.Zero)
            {
                await Task.Delay(1000);
                ts -= TimeSpan.FromSeconds(1);

                if (port.move == 1)
                {
                    ts = new TimeSpan(0, 0, currentValue);
                    port.move = 0;
                }


            }
            
            CloseAllAndOpenNew(auntification);

        }
        public static void CloseAllAndOpenNew(Form newForm)
        {

            var forms = Application.OpenForms.Cast<Form>().ToList();
            foreach (Form form in forms)
            {

                if (form.Name == newForm.Name && form.Text == newForm.Text)
                {
                    form.Show();

                    continue;

                }

                else
                {
                    port.block = form;
                    form.Hide();
                }
                   
            }
            
            // Открыть новую форму




        }

    }
}
