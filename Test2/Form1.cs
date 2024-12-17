using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDgv();
            Item();
        }

        public void LoadDgv()
        {
            try
            {
                string TextSql = "Select * From Tab";
                SqlDataAdapter adapter = new SqlDataAdapter(TextSql, DataBase.SqlCon);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                Dgv.DataSource = dataTable;
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                string TextSql = $"Insert into Tab values ('{(string)textBox1.Text}', '{(string)textBox2.Text}')";
                SqlConnection connection = new SqlConnection(DataBase.SqlCon);
                connection.Open();
                SqlCommand cmd = new SqlCommand(TextSql, connection);
                int kol = cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Запись добавлена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDgv();
                flowLayoutPanel1.Controls.Clear();
                Item();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string TextSql = $"Update Tab set stroka = '{(string)textBox1.Text}', stroka2 = '{(string)textBox2.Text}' where kod = '{Convert.ToInt32(textBox3.Text)}'";
                SqlConnection connection = new SqlConnection(DataBase.SqlCon);
                connection.Open();
                SqlCommand cmd = new SqlCommand(TextSql, connection);
                int kol = cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Запись изменина", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDgv();
                flowLayoutPanel1.Controls.Clear();
                Item();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string TextSql = $"Delete from Tab where kod = '{Convert.ToInt32(textBox3.Text)}'";
                SqlConnection connection = new SqlConnection(DataBase.SqlCon);
                connection.Open();
                SqlCommand cmd = new SqlCommand(TextSql, connection);
                int kol = cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Запись удалена","Сообщение",MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDgv();
                flowLayoutPanel1.Controls.Clear();
                Item();
            }
            catch
            {
                MessageBox.Show("Ошибка","Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Dgv_DoubleClick(object sender, EventArgs e)
        {
            textBox3.Text = Dgv.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = Dgv.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = Dgv.CurrentRow.Cells[2].Value.ToString();
        }

        public void Item()
        {
            SqlConnection connection = new SqlConnection(DataBase.SqlCon);
            connection.Open();  
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select count(kod) from Tab";

            int Count = (int)cmd.ExecuteScalar();

            cmd.CommandText = "Select kod from Tab";
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> Massive = new List<int>();
            while (reader.Read())
            {
                Massive.Add((int)reader.GetValue(0));
            }
            reader.Close();

            ListItem[] listItems = new ListItem[Count];

            for (int i = 0; i < Count; i++)
            {
                listItems[i] = new ListItem();

                cmd.CommandText = $"Select stroka from Tab where kod = {Massive[i]}";
                listItems[i].Stroka = (string)cmd.ExecuteScalar();

                cmd.CommandText = $"Select stroka2 from Tab where kod = {Massive[i]}";
                listItems[i].Stroka2 = (string)cmd.ExecuteScalar();

                if(flowLayoutPanel1.Controls.Count < 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                }
                else
                {
                    flowLayoutPanel1.Controls.Add(listItems[i]);
                }
            }
        }
    }
}
