using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            showEquipment();
            LoadCategories();


        }
        private void showEquipment()
        {
            // ตรวจสอบว่ามีการเลือกหมวดหมู่ใน combobox1 หรือไม่
            if (comboBox1.SelectedItem != null && !string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
            {

                // เรียกใช้ฟังก์ชัน databaseConnection() เพื่อเชื่อมต่อฐานข้อมูล
                string connectionString = "server=localhost;user id=root;password=;database=admin;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                DataSet ds = new DataSet();

                try
                {
                    // เปิดการเชื่อมต่อกับฐานข้อมูล
                    conn.Open();

                    // สร้างคำสั่ง SQL โดยใช้ parameter เพื่อป้องกัน SQL Injection
                    MySqlCommand cmd;
                    string selectedCategory = comboBox1.SelectedItem.ToString();
                    string query = "SELECT item,category,num,pic FROM item WHERE category = @category";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@category", selectedCategory);

                    // สร้าง DataAdapter และเตรียม DataSet
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // ปิดการเชื่อมต่อเมื่อไม่ใช้งาน
                    conn.Close();
                }

                // กำหนด DataSource ของ DataGridView
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            else
            {
                // ถ้าไม่มีการเลือกหมวดหมู่ใน combobox1 ให้แสดงทุกข้อมูลในตาราง
                try
                {
                    // เรียกใช้ฟังก์ชัน databaseConnection() เพื่อเชื่อมต่อฐานข้อมูล
                    string connectionString = "server=localhost;user id=root;password=;database=admin;";
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();

                    // เปิดการเชื่อมต่อกับฐานข้อมูล
                    conn.Open();

                    // สร้างคำสั่ง SQL เพื่อดึงข้อมูลทั้งหมดจากตาราง item
                    string query = "SELECT item,category,num,pic FROM item";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // สร้าง DataAdapter และเตรียม DataSet
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    // กำหนด DataSource ของ DataGridView
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;

                    // ปิดการเชื่อมต่อ
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void showEquipment2()
        {
            try
            {
                // เรียกใช้ฟังก์ชัน databaseConnection() เพื่อเชื่อมต่อฐานข้อมูล
                string connectionString = "server=localhost;user id=root;password=;database=admin;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                DataSet ds = new DataSet();

                // เปิดการเชื่อมต่อกับฐานข้อมูล
                conn.Open();

                // สร้างคำสั่ง SQL เพื่อดึงข้อมูลทั้งหมดจากตาราง item
                string query = "SELECT item,category,num,pic FROM item";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // สร้าง DataAdapter และเตรียม DataSet
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

                // กำหนด DataSource ของ DataGridView
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                // ปิดการเชื่อมต่อ
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }



        private void datagridview_cellclick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // ดึงข้อมูลในเซลล์ของคอลัมน์ item และ num ในแถวที่ถูกคลิก
                DataGridViewCell itemCell = dataGridView1.Rows[e.RowIndex].Cells["item"];
                DataGridViewCell numCell = dataGridView1.Rows[e.RowIndex].Cells["num"];
                DataGridViewCell picCell = dataGridView1.Rows[e.RowIndex].Cells["pic"];
                DataGridViewCell catCell = dataGridView1.Rows[e.RowIndex].Cells["category"];

                // ตรวจสอบว่าข้อมูลไม่ใช่ค่าว่าง
                if (itemCell.Value != null && numCell.Value != null && picCell.Value != null && picCell.Value != DBNull.Value)
                {
                    // นำข้อมูลจากคอลัมน์ item ไปแสดงใน TextBox ชื่อ txtname
                    txtname.Text = itemCell.Value.ToString();

                    // นำข้อมูลจากคอลัมน์ num ไปแสดงใน TextBox ชื่อ txtnum
                    txtnum.Text = numCell.Value.ToString();
                    txtcat.Text = catCell.Value.ToString();

                    byte[] imgData = (byte[])picCell.Value;
                    MemoryStream ms = new MemoryStream(imgData);
                    pic1.Image = Image.FromStream(ms);
                }
            }
        }
        //คำสั่งเพิ่มหมวดหมู่
        private void LoadCategories()
        {
            // เชื่อมต่อกับฐานข้อมูล
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            // เขียนคำสั่ง SQL เพื่อค้นหาข้อมูล category ที่ไม่ซ้ำจากตาราง item
            string query = "SELECT DISTINCT category FROM item";

            // สร้างคำสั่ง SQL และเชื่อมต่อกับฐานข้อมูล
            MySqlCommand command = new MySqlCommand(query, connection);

            // เปิดการเชื่อมต่อ
            connection.Open();

            // สร้าง Reader เพื่ออ่านข้อมูล
            MySqlDataReader reader = command.ExecuteReader();

            // เพิ่มข้อมูลใน ComboBox โดยไม่ซ้ำกัน
            comboBox1.Items.Add("รายการทั้งหมด"); // เพิ่มตัวเลือก "รายการทั้งหมด"
            while (reader.Read())
            {
                string category = reader["category"].ToString();
                comboBox1.Items.Add(category);
            }

            // ปิดการเชื่อมต่อ
            reader.Close();
            connection.Close();
        }


        // สร้างฟังก์ชันสำหรับโหลดข้อมูลใน DataGridView โดยขึ้นอยู่กับการเลือกข้อมูลใน ComboBox
        private void LoadDataToDataGridView(string selectedCategory)
        {
            // เชื่อมต่อกับฐานข้อมูล
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            // เขียนคำสั่ง SQL เพื่อดึงข้อมูล item ที่มี category เท่ากับ selectedCategory
            string query = "SELECT item,category,num,pic FROM item WHERE category = @category";

            // สร้างคำสั่ง SQL และเชื่อมต่อกับฐานข้อมูล
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@category", selectedCategory);

            // สร้าง DataTable เพื่อเก็บข้อมูล
            DataTable dataTable = new DataTable();

            // สร้าง DataAdapter และเตรียม DataSet
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            // เปิดการเชื่อมต่อ
            connection.Open();

            // โหลดข้อมูลจากฐานข้อมูลเข้า DataTable
            adapter.Fill(dataTable);

            // กำหนด DataSource ของ DataGridView
            dataGridView1.DataSource = dataTable;

            // ปิดการเชื่อมต่อ
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // เรียกใช้เมธอดเพื่อเรียกข้อมูลตามค่าที่ถูกเลือกใน ComboBox
            if (comboBox1.SelectedItem != null && !string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
            {
                string selectedCategory = comboBox1.SelectedItem.ToString();
                if (selectedCategory == "รายการทั้งหมด")
                {
                    showEquipment2(); // เรียกใช้ showEquipment เมื่อเลือก "รายการทั้งหมด"
                }
                else
                {
                    LoadDataToDataGridView(selectedCategory);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            // เชื่อมต่อฐานข้อมูล
            connection.Open();

            // สร้างคำสั่ง SQL เพื่อดึงข้อมูล item ที่มีชื่อใกล้เคียงกับที่กรอกใน TextBox2
            string query = "SELECT item,category,num,pic FROM item WHERE item LIKE @item";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@item", "%" + textBox2.Text + "%");

            // สร้าง DataAdapter และ DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // ปิดการเชื่อมต่อฐานข้อมูล
            connection.Close();

            // กำหนด DataSource ของ DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // ตั้งค่าให้แสดงเฉพาะไฟล์รูปภาพ
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            // แสดงกล่องตัวเลือกไฟล์
            DialogResult result = openFileDialog.ShowDialog();

            // ตรวจสอบว่าผู้ใช้ได้เลือกไฟล์หรือไม่
            if (result == DialogResult.OK)
            {
                try
                {
                    // อ่านชื่อไฟล์ที่เลือก
                    string fileName = openFileDialog.FileName;

                    // โหลดรูปภาพจากไฟล์และแสดงใน PictureBox
                    pic1.Image = Image.FromFile(fileName);
                }
                catch (Exception ex)
                {
                    // แสดงข้อความเมื่อเกิดข้อผิดพลาดในการโหลดรูปภาพ
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        //ปุ่มเพิ่มรายการ
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtname.Text) &&
                int.TryParse(txtnum.Text, out int num) &&
                num != 0 &&
                pic1.Image != null)
            {
                string connectionString = "server=localhost;user id=root;password=;database=admin;";
                MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    string queryCheck = "SELECT COUNT(*) FROM item WHERE item = @itemName";
                    MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
                    cmdCheck.Parameters.AddWithValue("@itemName", txtname.Text);

                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (count > 0)
                    {
                        string queryUpdate = "UPDATE item SET num = num + @num WHERE item = @itemName";
                        MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, connection);
                        cmdUpdate.Parameters.AddWithValue("@num", num);
                        cmdUpdate.Parameters.AddWithValue("@itemName", txtname.Text);

                        cmdUpdate.ExecuteNonQuery();
                    }
                    else
                    {
                        string queryInsert = "INSERT INTO item (item, num, pic, category) VALUES (@itemName, @num, @pic, @cat)";
                        MySqlCommand cmdInsert = new MySqlCommand(queryInsert, connection);
                        cmdInsert.Parameters.AddWithValue("@itemName", txtname.Text);
                        cmdInsert.Parameters.AddWithValue("@cat", txtcat.Text);
                        cmdInsert.Parameters.AddWithValue("@num", num);

                        MemoryStream ms = new MemoryStream();
                        pic1.Image.Save(ms, pic1.Image.RawFormat);
                        byte[] imgData = ms.ToArray();
                        cmdInsert.Parameters.AddWithValue("@pic", imgData);

                        cmdInsert.ExecuteNonQuery();
                    }

                    // เพิ่มข้อมูลหมวดหมู่ลงใน ComboBox
                    string categoryQuery = "SELECT DISTINCT category FROM item";
                    MySqlCommand categoryCmd = new MySqlCommand(categoryQuery, connection);
                    MySqlDataReader reader = categoryCmd.ExecuteReader();

                    comboBox1.Items.Clear();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["category"].ToString());
                    }
                    reader.Close();

                    MessageBox.Show("ข้อมูลถูกเพิ่มเข้าสู่ระบบแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showEquipment2();

                    txtname.Text = "";
                    txtnum.Text = "";
                    txtcat.Text = "";
                    pic1.Image = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วนและถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ปุ่มลบรายการ
        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtname.Text) && int.TryParse(txtnum.Text, out int num) && num != 0)
            {
                string connectionString = "server=localhost;user id=root;password=;database=admin;";
                MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();

                    string queryCheck = "SELECT COUNT(*) FROM item WHERE item = @itemName";
                    MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
                    cmdCheck.Parameters.AddWithValue("@itemName", txtname.Text);

                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (count > 0)
                    {
                        string queryUpdate = "UPDATE item SET num = num - @num WHERE item = @itemName";
                        MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, connection);

                        cmdUpdate.Parameters.AddWithValue("@num", num);
                        cmdUpdate.Parameters.AddWithValue("@itemName", txtname.Text);

                        cmdUpdate.ExecuteNonQuery();

                        string queryDelete = "DELETE FROM item WHERE item = @itemName AND num <= 0";
                        MySqlCommand cmdDelete = new MySqlCommand(queryDelete, connection);
                        cmdDelete.Parameters.AddWithValue("@itemName", txtname.Text);

                        cmdDelete.ExecuteNonQuery();

                        // อัพเดตข้อมูลใน ComboBox เมื่อลบรายการแล้ว
                        UpdateComboBoxItems(connection);

                        MessageBox.Show("อัพเดตจำนวนสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        showEquipment2();
                        txtname.Text = "";
                        txtnum.Text = "";
                        txtcat.Text = "";
                        pic1.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบรายการที่ตรงกับชื่อที่ระบุ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วนและถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateComboBoxItems(MySqlConnection connection)
        {
            string categoryQuery = "SELECT DISTINCT category FROM item";
            MySqlCommand categoryCmd = new MySqlCommand(categoryQuery, connection);
            MySqlDataReader reader = categoryCmd.ExecuteReader();

            List<string> categories = new List<string>();
            while (reader.Read())
            {
                categories.Add(reader["category"].ToString());
            }
            reader.Close();

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(categories.ToArray());
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = string .Empty;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
    