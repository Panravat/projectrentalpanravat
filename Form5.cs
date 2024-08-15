using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }
        //ยืนยัน
        private void button2_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าข้อมูลใน txtname, txtpass และ txtpass2 ถูกกรอกหรือไม่
            if (string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtpass.Text) || string.IsNullOrEmpty(txtpass2.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
                return;
            }

            // ตรวจสอบว่าข้อมูลที่กรอกใน txtpass และ txtpass2 ตรงกันหรือไม่
            // เช็คว่ารหัสผ่านไม่ตรงกัน
            if (txtpass.Text != txtpass2.Text)
            {
                MessageBox.Show("รหัสผ่านไม่ตรงกัน");
                return;
            }

            // เช็คว่ารหัสผ่านต้องเป็นตัวเลข 10 ตัว
            if (!Regex.IsMatch(txtpass.Text, @"^\d{10}$"))
            {
                MessageBox.Show("รหัสผ่านต้องเป็นตัวเลข 10 ตัวเท่านั้น");
                return;
            }


            // เชื่อมต่อกับฐานข้อมูล
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // เปิดการเชื่อมต่อ
                    connection.Open();

                    // ตรวจสอบว่าชื่อผู้ใช้ที่กรอกใน txtname มีอยู่ในฐานข้อมูลหรือไม่
                    string query = "SELECT COUNT(*) FROM user WHERE username = @username OR password = @password";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", txtname.Text);
                    command.Parameters.AddWithValue("@password", txtpass.Text);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("ชื่อผู้ใช้นี้หรือเบอร์โทรศัพท์นี้มีอยู่แล้วในระบบ");
                        return;
                    }

                    // เพิ่มข้อมูลผู้ใช้ใหม่ลงในฐานข้อมูล
                    query = "INSERT INTO user (username, password) VALUES (@username, @password)";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", txtname.Text);
                    command.Parameters.AddWithValue("@password", txtpass.Text);

                    // ส่งคำสั่ง SQL ไปประมวลผล
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("เพิ่มข้อมูลผู้ใช้เรียบร้อยแล้ว");
                        Form2 form2 = new Form2();
                        form2.ShowDialog();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("มีข้อผิดพลาดเกิดขึ้นในการเพิ่มข้อมูลผู้ใช้");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการทำงาน: " + ex.Message);
                }
            }
        }
        //ล้าง
        private void button1_Click(object sender, EventArgs e)
        {
            txtname.Text = string.Empty;
            txtpass.Text = string.Empty;
            txtpass2.Text = string.Empty;
        }
    }
}
