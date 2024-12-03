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
        public dataImport()
        {
            InitializeComponent();
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
            string[] readText = File.ReadAllLines(filePath); 
            string[] valField;
            string[] titleField = readText[0].Split(';'); 
            string strCmd = $"INSERT INTO {comboBoxTables.SelectedItem}({String.Join(",", titleField)}) VALUES ";

            foreach (string str in readText.Skip(1).ToArray())
            {
                valField = str.Split(';');
                strCmd += "(";
                for (int i = 0; i < titleField.Length; i++)
                {
                    strCmd += $"'{valField[i]}'";
                    if (i != titleField.Length - 1)
                        strCmd += ",";
                }
                strCmd += "),";
            }
            strCmd = strCmd.Substring(0, strCmd.Length - 1) + ";";
            MySqlConnection con = new MySqlConnection(Connection.connect());
            con.Open();

            MySqlCommand cmd = new MySqlCommand(strCmd, con);
            int result = cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show($"Импортированно {result} записей в agent.{comboBoxTables.SelectedItem}");
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            change();
        }
    }
}
