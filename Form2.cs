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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อผู้ใช้และรหัสผ่านถูกกรอกหรือไม่
            if (string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(passtxt.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
                return;
            }

            // เชื่อมต่อกับฐานข้อมูล
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // คำสั่ง SQL สำหรับตรวจสอบชื่อผู้ใช้และรหัสผ่าน
                string query = "SELECT COUNT(*) FROM user WHERE username = @username AND password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", nametxt.Text);
                command.Parameters.AddWithValue("@password", passtxt.Text);

                // ส่งคำสั่ง SQL ไปประมวลผล
                int count = Convert.ToInt32(command.ExecuteScalar());

                // ตรวจสอบว่ามีข้อมูลตรงกันหรือไม่
                if (count > 0)
                {
                    Form4 form4 = new Form4();
                    form4.Show(); // เปิดฟอร์มใหม่
                    this.Close(); // ปิดฟอร์มปัจจุบัน
                }
                else
                {
                    MessageBox.Show("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();
            form5.ShowDialog();
            

        }

        private void nametxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
