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

            if (!Regex.Match(Symbol, @"[а-яА-Я]|[\b]|[-]|[\s]|[.]|[,]").Success)
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

        public static void FormPaint(Form form)
        {
            Graphics g = form.CreateGraphics();
            Pen p1 = new Pen(Color.FromArgb(213, 213, 213),5);
            g.DrawLine(p1, 0, 0, form.Width - 1, 0);
            g.DrawLine(p1, 0, 0, 0, form.Height - 1);
            g.DrawLine(p1, form.Width - 1, 0, form.Width - 1, form.Height - 1);
            g.DrawLine(p1, 0, form.Height - 1, form.Width - 1, form.Height - 1);
        }
        static public void load(DataGridView gridView, string search)
        {
            
            MySqlConnection connection = new MySqlConnection(Connection.con);
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
            MySqlConnection conn = new MySqlConnection(Connection.con);
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
            MySqlConnection con = new MySqlConnection(Connection.con);
            con.Open();
            MySqlCommand com = new MySqlCommand(search, con);

            int num =com.ExecuteNonQuery();
            
            con.Close();
        }
        
    }
}
