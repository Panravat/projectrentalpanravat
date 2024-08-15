using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            
            showEquipment2();
            showEquipment3();
            dataGridView2_TextChanged(sender, e);
            txtnum.ReadOnly = true;
            txtname.ReadOnly = true;
            nametxt.ReadOnly = true;
            teltxt.ReadOnly = true;

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
                string query = "SELECT item,num,pic FROM item";
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

        private void showEquipment3()
        {
            try
            {
                // เชื่อมต่อฐานข้อมูล
                string connectionString = "server=localhost;user id=root;password=;database=admin;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                DataSet ds = new DataSet();

                // เปิดการเชื่อมต่อกับฐานข้อมูล
                conn.Open();

                // สร้างคำสั่ง SQL เพื่อดึงข้อมูลที่ตรงกับข้อมูลที่กรอกในช่อง stxt จากตาราง rental
                string query = "SELECT * FROM rental WHERE tel LIKE @tel";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tel", "%" + stxt.Text + "%");

                // สร้าง DataAdapter และเตรียม DataSet
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

                // ตรวจสอบว่า DataSet มีข้อมูลหรือไม่
                if (ds.Tables[0].Rows.Count == 0)
                {
                    // หากไม่มีข้อมูล สร้าง DataTable ที่ไม่มีแถวข้อมูล
                    DataTable emptyTable = new DataTable();
                    dataGridView22.DataSource = emptyTable.DefaultView;
                }
                else
                {
                    // มีข้อมูล กำหนด DataSource ของ DataGridView
                    dataGridView22.DataSource = ds.Tables[0].DefaultView;
                }

                // ปิดการเชื่อมต่อ
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // ดึงข้อมูลในเซลล์ของคอลัมน์ item และ num ในแถวที่ถูกคลิก
                DataGridViewCell itemCell = dataGridView22.Rows[e.RowIndex].Cells["item"];
                DataGridViewCell numCell = dataGridView22.Rows[e.RowIndex].Cells["num"];
                DataGridViewCell nameCell = dataGridView22.Rows[e.RowIndex].Cells["name"];
                DataGridViewCell telCell = dataGridView22.Rows[e.RowIndex].Cells["tel"];
                DataGridViewCell sentbackdateCell = dataGridView22.Rows[e.RowIndex].Cells["sentbackdate"];

                // ตรวจสอบว่าข้อมูลไม่ใช่ค่าว่างหรือ null
                if (itemCell.Value != null && numCell.Value != null && nameCell.Value != null && telCell.Value != null && sentbackdateCell.Value != null)
                {
                    // นำข้อมูลจากคอลัมน์ item ไปแสดงใน TextBox ชื่อ txtname
                    txtname.Text = itemCell.Value.ToString();

                    // นำข้อมูลจากคอลัมน์ num ไปแสดงใน TextBox ชื่อ txtnum
                    txtnum.Text = numCell.Value.ToString();
                    nametxt.Text = nameCell.Value.ToString();
                    teltxt.Text = telCell.Value.ToString();

                    // แปลงวันที่จากข้อมูลในเซลล์ sentbackdateCell เป็นรูปแบบ "yyyy-MM-dd"
                    string sentbackdate = sentbackdateCell.Value.ToString();
                    DateTime sentbackDateTime;
                    if (DateTime.TryParseExact(sentbackdate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out sentbackDateTime))
                    {
                        // แปลงวันที่เป็น พ.ศ. เป็น ค.ศ.
                        sentbackDateTime = sentbackDateTime.AddYears(-543);

                        // กำหนดค่าให้กับ DateTimePicker date1
                        date1.Value = sentbackDateTime;

                        txtnum.ReadOnly = false;
                    }
                }

            }
        }


        private void stxt_TextChanged(object sender, EventArgs e)
        {
            if (stxt.Text.Length == 10) // ตรวจสอบว่ามีตัวเลขที่กรอกครบ 10 ตัวหรือไม่
            {
                string connectionString = "server=localhost;user id=root;password=;database=admin;";
                MySqlConnection connection = new MySqlConnection(connectionString);

                // เชื่อมต่อฐานข้อมูล
                connection.Open();

                // สร้างคำสั่ง SQL เพื่อดึงข้อมูล item ที่มีเบอร์โทรศัพท์ที่ตรงกับที่กรอกใน stxt
                string query = "SELECT * FROM rental WHERE tel LIKE @tel";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@tel", "%" + stxt.Text + "%");

                // สร้าง DataAdapter และ DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable2 = new DataTable();
                adapter.Fill(dataTable2);

                // ปิดการเชื่อมต่อฐานข้อมูล
                connection.Close();

                // กำหนด DataSource ของ DataGridView
                dataGridView22.DataSource = dataTable2;

                int totalNum = 0;

                foreach (DataGridViewRow row in dataGridView22.Rows)
                {
                    if (row.Cells["num"].Value != null && int.TryParse(row.Cells["num"].Value.ToString(), out int num))
                    {
                        totalNum += num;
                    }
                }

                // คูณด้วย 100
                totalNum *= 100;

                // แสดงผลลัพธ์ใน TextBox
                textBox1.Text = totalNum.ToString();

                // ตรวจสอบว่ามีข้อมูลหรือไม่
                if (dataTable2.Rows.Count == 0)
                {
                    // ถ้าไม่มีข้อมูล กำหนดค่าใน textBox1 เป็น "0"
                    textBox1.Text = "0";
                }
            }
            else
            {
                dataGridView22.DataSource = null; // ล้างข้อมูลที่แสดงใน DataGridView
                textBox1.Text = "0"; // กำหนดค่าใน textBox1 เป็น "0"
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = textBox1.SelectionStart;

            // ลบการสมัครสมาชิกชั่วคราวเพื่อลดการเรียกใช้ซ้ำ
            textBox1.TextChanged -= textBox1_TextChanged;

            // เก็บค่าเดิมที่ไม่มีคอมม่า
            string originalText = textBox1.Text.Replace(",", "");

            // ตรวจสอบว่าเป็นตัวเลขหรือไม่
            if (int.TryParse(originalText, out int num))
            {
                // แปลงตัวเลขให้มีคอมม่า
                textBox1.Text = num.ToString("N0");
            }

            // กำหนดตำแหน่งเคอร์เซอร์ใหม่หลังจากที่มีการปรับปรุงข้อความ
            cursorPosition = Math.Min(cursorPosition + (textBox1.Text.Length - originalText.Length), textBox1.Text.Length);

            // สมัครสมาชิกเหตุการณ์ TextChanged ใหม่
            textBox1.TextChanged += textBox1_TextChanged;

            // กำหนดตำแหน่งเคอร์เซอร์ใหม่
            textBox1.SelectionStart = cursorPosition;
        }


        private void dataGridView2_TextChanged(object sender, EventArgs e)
        {
            int totalNum = 0;

            foreach (DataGridViewRow row in dataGridView22.Rows)
            {
                if (row.Cells["num"].Value != null && int.TryParse(row.Cells["num"].Value.ToString(), out int num))
                {
                    totalNum += num;
                }
            }

            // คูณด้วย 100
            totalNum *= 100;

            // แสดงผลลัพธ์ใน TextBox
            textBox1.Text = totalNum.ToString();
        }

        private void SubtractFromRental(string itemName, int numToSubtract)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string updateQuery = "UPDATE rental SET num = num - @numToSubtract WHERE item = @itemName AND name = @name AND tel = @tel";
                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@numToSubtract", numToSubtract);
                command.Parameters.AddWithValue("@itemName", itemName);
                command.Parameters.AddWithValue("@name", nametxt.Text);
                command.Parameters.AddWithValue("@tel", teltxt.Text);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    showEquipment2();
                    showEquipment3();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูลจากตาราง rental: " + ex.Message);
                }
            }
        }

        // เมธอดสำหรับเพิ่มจำนวน numToSubtract ในคอลัมน์ item ของตาราง item
        private void AddToItem(string itemName, int numToSubtract)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string updateQuery = "UPDATE item SET num = num + @numToSubtract WHERE item = @itemName";
                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@numToSubtract", numToSubtract);
                command.Parameters.AddWithValue("@itemName", itemName);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    showEquipment2();
                    showEquipment3();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูลในตาราง item: " + ex.Message);
                }
            }
        }
        private void DeleteFromRentalIfNumIsZero(string itemName, string name, string tel)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM rental WHERE item = @itemName AND name = @name AND tel = @tel AND num <= 0";
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@itemName", itemName);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@tel", tel);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    showEquipment2();
                    showEquipment3();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูลจากตาราง rental: " + ex.Message);
                }
            }
        }
        // ตรวจสอบว่า item มีอยู่ในตาราง rental_backup หรือไม่
        bool CheckIfItemExistsInRentalBackup(string itemName)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM rental WHERE item = @itemName";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@itemName", itemName);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // คืนค่า true ถ้ามี item ในตาราง, ไม่เช่นนั้นคืนค่า false
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการตรวจสอบข้อมูล: " + ex.Message);
                    return false; // หากเกิดข้อผิดพลาดในการทำงานให้คืนค่า false
                }
            }
        }

        // ตรวจสอบว่า item มีอยู่ในตาราง item หรือไม่
        bool CheckIfItemExistsInItem(string itemName)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM item WHERE item = @itemName";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@itemName", itemName);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // คืนค่า true ถ้ามี item ในตาราง, ไม่เช่นนั้นคืนค่า false
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการตรวจสอบข้อมูล: " + ex.Message);
                    return false; // หากเกิดข้อผิดพลาดในการทำงานให้คืนค่า false
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ากรอกข้อมูลใน txtnum และ date1 หรือไม่
            if (string.IsNullOrEmpty(txtnum.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วนและเลือกวันที่ให้ถูกต้อง");
                return;
            }
            if (int.TryParse(txtnum.Text, out int numTextValue))
            {
                foreach (DataGridViewRow row in dataGridView22.Rows)
                {
                    if (row.Cells["num"].Value != null && int.TryParse(row.Cells["num"].Value.ToString(), out int numValueFromRow))
                    {
                        if (numTextValue > numValueFromRow)
                        {
                            MessageBox.Show("กรุณาลองอีกครั้ง");
                            return;
                        }
                    }
                }
            }
            // ตรวจสอบว่า txtnum เป็นตัวเลขหรือไม่
            if (!int.TryParse(txtnum.Text, out int numToSubtract))
            {
                MessageBox.Show("กรุณากรอกข้อมูลตัวเลขในช่อง 'จำนวน'");
                return;
            }

            // ตรวจสอบว่าวันที่ใน date1 เกินวันที่ปัจจุบันหรือไม่
            if (date1.Value < DateTime.Now)
            {
                // นับค่าปรับเพิ่ม
                int additionalFee = numToSubtract * 100;

                // นำราคาปรับมาแสดงใน textBox2
                int currentFee = 0;
                if (int.TryParse(textBox2.Text, out currentFee))
                {
                    textBox2.Text = (currentFee + additionalFee).ToString();
                }
                else
                {
                    textBox2.Text = additionalFee.ToString();
                }
                
            }
            
            // กำหนดชื่อ item ที่ต้องการลบหรืออัปเดต
            string itemName = txtname.Text;

            // ตรวจสอบว่า item นั้นมีอยู่ในทั้งสองตารางหรือไม่
            bool itemExistsInRentalBackup = CheckIfItemExistsInRentalBackup(itemName);
            bool itemExistsInItem = CheckIfItemExistsInItem(itemName);

            // ถ้า item ไม่มีอยู่ในทั้งสองตาราง ให้แจ้งเตือนและไม่ดำเนินการต่อ
            if (!itemExistsInRentalBackup || !itemExistsInItem)
            {
                MessageBox.Show("ไม่พบข้อมูลรายการที่ต้องการในฐานข้อมูล");
                return;
            }

            
            // ลบข้อมูลจากตาราง rental ตามข้อมูลใน txtname ที่ตรงกับ item
            SubtractFromRental(itemName, numToSubtract);

            // ทำการเพิ่มจำนวน numToSubtract ในคอลัมน์ item ของตาราง item ที่ txtname ตรงกับ item
            AddToItem(itemName, numToSubtract);

// ลบข้อมูลจากตาราง rental ถ้า num เป็น 0
            DeleteFromRentalIfNumIsZero(itemName, nametxt.Text, teltxt.Text);


            dataGridView2_TextChanged(sender, e);
            txtname.Text = "";
            txtnum.Text = "";
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าข้อมูลใน nametxt และ teltxt ไม่ใช่ค่าว่าง
            if (string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(teltxt.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง");
                return;
            }

            // กำหนดค่าชื่อและเบอร์โทรศัพท์ที่ต้องการลบ
            string nameToDelete = nametxt.Text;
            string telToDelete = teltxt.Text;

            // เรียกใช้เมธอดที่ทำการลบข้อมูลในตาราง rental ตามชื่อและเบอร์โทรศัพท์
            DeleteFromRental(nameToDelete, telToDelete);
            showEquipment2();
            showEquipment3();
            MessageBox.Show("ดำเนินการเรียบร้อยแล้ว");
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();

        }

        // เมธอดสำหรับลบข้อมูลในตาราง rental ตามชื่อและเบอร์โทรศัพท์ที่ระบุ
        private void DeleteFromRental(string name, string tel)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM rental WHERE name = @name AND tel = @tel";
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@tel", tel);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    showEquipment2();
                    showEquipment3();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูลจากตาราง rental: " + ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nametxt.Text = string.Empty;
            teltxt.Text = string.Empty;
            txtnum.Text = string.Empty;
            txtname.Text = string.Empty;
            txtnum.ReadOnly = false;
            txtname.ReadOnly = false;
        }

        private void stxt_Click(object sender, EventArgs e)
        {
            stxt.Text = string.Empty;
            dataGridView22.DataSource = null;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = textBox2.SelectionStart;

            // ลบการสมัครสมาชิกชั่วคราวเพื่อลดการเรียกใช้ซ้ำ
            textBox2.TextChanged -= textBox2_TextChanged;

            // เก็บค่าเดิมที่ไม่มีคอมม่า
            string originalText = textBox2.Text.Replace(",", "");

            // ตรวจสอบว่าเป็นตัวเลขหรือไม่
            if (int.TryParse(originalText, out int num))
            {
                // แปลงตัวเลขให้มีคอมม่า
                textBox2.Text = num.ToString("N0");
            }

            // กำหนดตำแหน่งเคอร์เซอร์ใหม่หลังจากที่มีการปรับปรุงข้อความ
            cursorPosition = Math.Min(cursorPosition + (textBox2.Text.Length - originalText.Length), textBox2.Text.Length);

            // สมัครสมาชิกเหตุการณ์ TextChanged ใหม่
            textBox2.TextChanged += textBox2_TextChanged;

            // กำหนดตำแหน่งเคอร์เซอร์ใหม่
            textBox2.SelectionStart = cursorPosition;
        }

        private void dataGridView22_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
