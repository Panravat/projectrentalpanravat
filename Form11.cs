using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            showEquipment2();
        }
        private void showEquipment2()
        {
            // เรียกใช้ฟังก์ชัน databaseConnection() เพื่อเชื่อมต่อฐานข้อมูล
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            DataSet ds = new DataSet();

            // เปิดการเชื่อมต่อกับฐานข้อมูล
            conn.Open();

            // สร้างคำสั่ง SQL เพื่อดึงข้อมูลทั้งหมดจากตาราง history และเรียงลำดับตามวันที่
            string query = "SELECT * FROM history ORDER BY date DESC";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            // สร้าง DataAdapter และเตรียม DataSet
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            // กรองข้อมูลที่ซ้ำกันออก
            DataTable distinctTable = ds.Tables[0].AsEnumerable()
                .GroupBy(row => new
                {
                    Name = row["name"],
                    Tel = row["tel"],
                    Date = row["date"]
                })
                .Select(g => g.First())
                .CopyToDataTable();

            // กำหนด DataSource ของ DataGridView
            dataGridView1.DataSource = distinctTable.DefaultView;

            // ปิดการเชื่อมต่อ
            conn.Close();

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // เชื่อมต่อฐานข้อมูล
                connection.Open();

                // สร้างคำสั่ง SQL เพื่อดึงข้อมูล item ที่มีชื่อหรือเบอร์โทรใกล้เคียงกับที่กรอกใน TextBox2
                string query;
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    query = "SELECT * FROM history";
                }
                else
                {
                    query = "SELECT * FROM history WHERE name LIKE @name OR tel LIKE @tel";
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + textBox2.Text + "%");
                    cmd.Parameters.AddWithValue("@tel", "%" + textBox2.Text + "%");
                }

                // สร้าง DataAdapter และ DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // กรองข้อมูลที่ไม่ซ้ำกัน
                DataTable distinctTable = dataTable.AsEnumerable()
                    .GroupBy(row => new
                    {
                        Name = row["name"],
                        Tel = row["tel"],
                        Date = row["date"]
                    })
                    .Select(g => g.First())
                    .CopyToDataTable();

                // กำหนด DataSource ของ DataGridView
                dataGridView1.DataSource = distinctTable;

                // ปิดการเชื่อมต่อฐานข้อมูล
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
           textBox2.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
