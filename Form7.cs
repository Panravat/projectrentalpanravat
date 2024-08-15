using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            showEquipment();
            LoadCategories();
            // หากต้องการให้เป็นวันที่ของปี พ.ศ.
            CultureInfo thaiCulture = new CultureInfo("th-TH");
            DateTimeFormatInfo thaiDateTimeFormat = thaiCulture.DateTimeFormat;
            thaiDateTimeFormat.Calendar = new ThaiBuddhistCalendar();

            // แปลงวันที่ปัจจุบันเพิ่มขึ้น 7 วันในรูปแบบ "yyyy-MM-dd" และแปลงเป็น DateTime
            DateTime futureDate = DateTime.Now.AddDays(7);
            string formattedDate = futureDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime parsedDate = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            // กำหนดค่าให้กับ DateTimePicker
            date1.Value = parsedDate;


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
                    string query = "SELECT item,num,pic FROM item WHERE category = @category";
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
                // เรียกใช้ฟังก์ชัน databaseConnection() เพื่อเชื่อมต่อฐานข้อมูล
                string connectionString = "server=localhost;user id=root;password=;database=admin;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                DataSet ds = new DataSet();

                // เปิดการเชื่อมต่อกับฐานข้อมูล
                conn.Open();

                // สร้างคำสั่ง SQL เพื่อดึงข้อมูลทั้งหมดจากตาราง item
                string query = "SELECT item,num,date,sentbackdate FROM rental_backup";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // สร้าง DataAdapter และเตรียม DataSet
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

                // กำหนด DataSource ของ DataGridView
                dataGridView2.DataSource = ds.Tables[0].DefaultView;

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
            button4.Enabled = false;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // ดึงข้อมูลในเซลล์ของคอลัมน์ item และ num ในแถวที่ถูกคลิก
                DataGridViewCell itemCell = dataGridView1.Rows[e.RowIndex].Cells["item"];
                
                DataGridViewCell picCell = dataGridView1.Rows[e.RowIndex].Cells["pic"];
              

                // ตรวจสอบว่าข้อมูลไม่ใช่ค่าว่าง
                if (itemCell.Value != null  && picCell.Value != null && picCell.Value != DBNull.Value)
                {
                    // นำข้อมูลจากคอลัมน์ item ไปแสดงใน TextBox ชื่อ txtname
                    txtname.Text = itemCell.Value.ToString();

                    // นำข้อมูลจากคอลัมน์ num ไปแสดงใน TextBox ชื่อ txtnum
                
                    

                    byte[] imgData = (byte[])picCell.Value;
                    MemoryStream ms = new MemoryStream(imgData);
                    pic1.Image = Image.FromStream(ms);
                }
            }
            txtnum.ReadOnly = false;
        }
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
            string query = "SELECT item,num,pic FROM item WHERE category = @category";

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

        private void stxt_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            // เชื่อมต่อฐานข้อมูล
            connection.Open();

            // สร้างคำสั่ง SQL เพื่อดึงข้อมูล item ที่มีชื่อใกล้เคียงกับที่กรอกใน TextBox2
            string query = "SELECT item,num,pic FROM item WHERE item LIKE @item";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@item", "%" + stxt.Text + "%");

            // สร้าง DataAdapter และ DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // ปิดการเชื่อมต่อฐานข้อมูล
            connection.Close();

            // กำหนด DataSource ของ DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void stxt_Click(object sender, EventArgs e)
        {
            stxt.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีการกรอกข้อมูลในช่องข้อความที่ระบุหรือไม่
            if (string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(teltxt.Text) || string.IsNullOrEmpty(txtname.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง");
                return;
            }

            string item = txtname.Text;
            bool itemMatchesName = CheckIfItemMatchesName(item);

            // ถ้าไม่ตรงให้แสดงข้อความแจ้งเตือน
            if (!itemMatchesName)
            {
                MessageBox.Show("กรุณาลองอีกครั้ง");
                return;
            }

            // ตรวจสอบว่า txtnum เป็นตัวเลขที่มากกว่า 0 หรือไม่
            if (!int.TryParse(txtnum.Text, out int num) || num <= 0)
            {
                MessageBox.Show("กรุณาลองอีกครั้ง");
                return;
            }
            if (CheckIfNumGreaterThanItemNum(txtname.Text, num))
            {
                MessageBox.Show("กรุณาลองอีกครั้ง");
                return;
            }


            // เช็คว่าข้อมูลที่กรอกใน txtname มีอยู่ในคอลัม item หรือไม่
            bool itemExists = CheckIfItemExists(item);

            // ถ้าไม่มีให้เพิ่มข้อมูลใหม่
            if (!itemExists)
            {
                AddNewItem(item, num);
                showEquipment3();
                showEquipment2();
            }
            else
            {
                UpdateExistingItem(item, num);
                showEquipment3();
                showEquipment2();
            }
            nametxt.ReadOnly = true;
            teltxt.ReadOnly = true;
            txtnum.Text = string.Empty;
            txtname.Text = string.Empty;
            // เพิ่มข้อมูลลงในตาราง rental_backup

        }

        // เช็คว่าข้อมูลที่กรอกใน txtname มีอยู่ในคอลัม item หรือไม่
        private bool CheckIfItemExists(string item)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM rental_backup WHERE item = @item";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@item", item);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                    return false;
                }
            }
        }

        // เพิ่มข้อมูลใหม่เมื่อไม่มี item ในระบบ
        private void AddNewItem(string item, int num)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // เตรียมคำสั่ง SQL สำหรับอัปเดตค่าในคอลัมน์ num ของตาราง item
                string updateQuery = "UPDATE item SET num = num - @num WHERE item = @item";

                // สร้าง MySqlCommand และเตรียมพารามิเตอร์
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@num", num);
                updateCommand.Parameters.AddWithValue("@item", item);

                try
                {
                    // เปิดการเชื่อมต่อกับฐานข้อมูล
                    connection.Open();

                    // ทำการอัปเดตข้อมูลในตาราง item
                    updateCommand.ExecuteNonQuery();

                    // เตรียมคำสั่ง SQL สำหรับเพิ่มข้อมูลในตาราง rental_backup
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string currentDate2 = date1.Value.ToString("yyyy-MM-dd");
                    string insertQuery = "INSERT INTO rental_backup (name, tel, item, num, date,sentbackdate) VALUES (@name, @tel, @item, @num, @date,@sentbackdate)";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@name", nametxt.Text);
                    insertCommand.Parameters.AddWithValue("@tel", teltxt.Text);
                    insertCommand.Parameters.AddWithValue("@item", item);
                    insertCommand.Parameters.AddWithValue("@num", num);
                    insertCommand.Parameters.AddWithValue("@date", currentDate);
                    insertCommand.Parameters.AddWithValue("@sentbackdate", currentDate2);

                    // ทำการเพิ่มข้อมูลในตาราง rental_backup
                    insertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }


        // อัปเดตข้อมูลเมื่อมี item ในระบบแล้ว
        private void UpdateExistingItem(string item, int num)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string updateQuery2 = "UPDATE item SET num = num - @num WHERE item = @item";

                // สร้าง MySqlCommand และเตรียมพารามิเตอร์
                MySqlCommand updateCommand = new MySqlCommand(updateQuery2, connection);
                updateCommand.Parameters.AddWithValue("@num", num);
                updateCommand.Parameters.AddWithValue("@item", item);

                string updateQuery = "UPDATE rental_backup SET num = num + @num WHERE item = @item";
                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@num", num);

                

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    updateCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }
        private bool CheckIfItemMatchesName(string item)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM item WHERE item = @item";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@item", item);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                    return false;
                }
            }
        }
        private bool CheckIfNumGreaterThanItemNum(string item, int num)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT num FROM item WHERE item = @item";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@item", item);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int itemNum))
                    {
                        return num > itemNum;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }

            return false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Enabled = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // ดึงข้อมูลในเซลล์ของคอลัมน์ item และ num ในแถวที่ถูกคลิก
                DataGridViewCell itemCell = dataGridView2.Rows[e.RowIndex].Cells["item"];
                DataGridViewCell numCell = dataGridView2.Rows[e.RowIndex].Cells["num"];
         
               
                DataGridViewCell sentbackdateCell = dataGridView2.Rows[e.RowIndex].Cells["sentbackdate"];

                // ตรวจสอบว่าข้อมูลไม่ใช่ค่าว่าง
                if (itemCell.Value != null && numCell.Value != null  && sentbackdateCell.Value != null)
                {
                    // นำข้อมูลจากคอลัมน์ item ไปแสดงใน TextBox ชื่อ txtname
                    txtname.Text = itemCell.Value.ToString();

                    // นำข้อมูลจากคอลัมน์ num ไปแสดงใน TextBox ชื่อ txtnum
                    txtnum.Text = numCell.Value.ToString();
                   
                    

                    nametxt.ReadOnly = true;
                    teltxt.ReadOnly = true;
                    txtname.ReadOnly = true;
                    txtnum.ReadOnly = true;

                    // แปลงรูปแบบวันที่จาก sentbackdateCell เป็นรูปแบบ "yyyy-MM-dd"
                    string sentbackdate = sentbackdateCell.Value.ToString();
                    DateTime sentbackDateTime;
                    if (DateTime.TryParseExact(sentbackdate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out sentbackDateTime))
                    {
                        // แปลงวันที่เป็น พ.ศ. เป็น ค.ศ.
                        sentbackDateTime = sentbackDateTime.AddYears(-543);

                        // กำหนดค่าให้กับ DateTimePicker date1
                        date1.Value = sentbackDateTime;
                    }
                    
                }
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
            
            // ตัวแปรสำหรับเก็บ Connection String
            string connectionString = "server=localhost;user id=root;password=;database=admin;";

            // คำสั่ง SQL สำหรับการอัปเดตค่าในตาราง item
            string updateQuery = "UPDATE item INNER JOIN rental_backup ON item.item = rental_backup.item SET item.num = item.num + rental_backup.num";

            // คำสั่ง SQL สำหรับการลบข้อมูลในตาราง rental_backup
            string deleteQuery = "DELETE FROM rental_backup";

            // เชื่อมต่อฐานข้อมูล
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);

                try
                {
                    // เปิดการเชื่อมต่อ
                    connection.Open();

                    // ประมวลผลคำสั่ง SQL เพื่ออัปเดตค่าในตาราง item
                    updateCommand.ExecuteNonQuery();

                    // ประมวลผลคำสั่ง SQL เพื่อลบข้อมูลในตาราง rental_backup
                    deleteCommand.ExecuteNonQuery();

                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }

            // เปิด Form4
            
        }
        private bool CheckIfItemExistsInRentalBackup(string item)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM rental_backup WHERE item = @item";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@item", item);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการตรวจสอบข้อมูล: " + ex.Message);
                    return false;
                }
            }
        }

        private bool CheckIfItemExistsInItem(string item)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM item WHERE item = @item";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@item", item);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการตรวจสอบข้อมูล: " + ex.Message);
                    return false;
                }
            }
        }

        private void UpdateRentalAndItemTables(string item, int num)
        {
            // ตัวแปรสำหรับเก็บ Connection String
            string connectionString = "server=localhost;user id=root;password=;database=admin;";

            // ตรวจสอบว่า item ที่จะลบหรืออัพเดตมีอยู่ในทั้งสองตารางหรือไม่
            bool itemExistsInRentalBackup = CheckIfItemExistsInRentalBackup(item);
            bool itemExistsInItem = CheckIfItemExistsInItem(item);

            // ถ้าไม่มี item ที่ระบุในทั้งสองตาราง ให้แจ้งเตือนและยกเลิกการดำเนินการ
            if (!itemExistsInRentalBackup || !itemExistsInItem)
            {
                MessageBox.Show("กรุณาลองอีกครั้ง");
                return;
            }

            // ลบข้อมูลจากตาราง rental_backup ตามข้อมูลใน txtname ที่ตรงกับ item
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM rental_backup WHERE item = @item";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@item", item);

                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message);
                    return;
                }
            }

            // นำข้อมูลใน txtnum ไปบวกกับ num ในตาราง item ที่ txtname ตรงกับ item
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string updateQuery = "UPDATE item SET num = num + @num WHERE item = @item";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@num", num);
                updateCommand.Parameters.AddWithValue("@item", item);

                try
                {
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการอัปเดตข้อมูล: " + ex.Message);
                    return;
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nametxt.Text) || string.IsNullOrEmpty(teltxt.Text) || string.IsNullOrEmpty(txtname.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง");
                return;
            }

            string item = txtname.Text;
            bool itemMatchesName = CheckIfItemMatchesName(item);

            // ถ้าไม่ตรงให้แสดงข้อความแจ้งเตือน
            if (!itemMatchesName)
            {
                MessageBox.Show("กรุณาลองอีกครั้ง");
                return;
            }

            // ตรวจสอบว่า txtnum เป็นตัวเลขที่มากกว่า 0 หรือไม่
            if (!int.TryParse(txtnum.Text, out int num) || num <= 0)
            {
                MessageBox.Show("กรุณาลองอีกครั้ง");
                return;
            }
            UpdateRentalAndItemTables(item, num);
            showEquipment2();
            showEquipment3();
            txtnum.Text = string.Empty;
            txtname.Text = string.Empty;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            nametxt.Text = string.Empty;
            teltxt.Text = string.Empty;
            txtnum.Text = string.Empty;
            txtname.Text = string.Empty;
            txtnum.ReadOnly = false;
            txtname.ReadOnly = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ตัวแปรสำหรับเก็บ Connection String
            string connectionString = "server=localhost;user id=root;password=;database=admin;";

            // คำสั่ง SQL สำหรับการนำข้อมูลจากตาราง rental_backup ไปยังตาราง rental
            string insertQuery = "INSERT INTO rental (name, tel, item, num, date,sentbackdate) SELECT name, tel, item, num, date,sentbackdate FROM rental_backup";

            // คำสั่ง SQL สำหรับลบข้อมูลทั้งหมดในตาราง rental_backup
            string deleteQuery = "DELETE FROM rental_backup";

            // คำสั่ง SQL สำหรับเพิ่มข้อมูลในตาราง history โดยตรวจสอบว่าข้อมูลซ้ำกับที่มีอยู่แล้วหรือไม่
            string insertHistoryQuery = "INSERT INTO history (name, tel, date) " +
                                        "SELECT @name, @tel, @date " +
                                        "WHERE NOT EXISTS " +
                                        "(SELECT * FROM history WHERE name = @name AND tel = @tel AND date = @date)";

            // คำสั่ง SQL สำหรับอัปเดตข้อมูลในตาราง rental หากข้อมูลซ้ำ
            string updateRentalQuery = "UPDATE rental SET num = num + @num " +
                                       "WHERE name = @name AND tel = @tel AND item = @item";

            // คำสั่ง SQL สำหรับตรวจสอบข้อมูลซ้ำในตาราง rental
            string checkRentalQuery = "SELECT COUNT(*) FROM rental WHERE name = @name AND tel = @tel AND item = @item";

            // เชื่อมต่อฐานข้อมูล
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                MySqlCommand insertHistoryCommand = new MySqlCommand(insertHistoryQuery, connection);
                MySqlCommand updateRentalCommand = new MySqlCommand(updateRentalQuery, connection);
                MySqlCommand checkRentalCommand = new MySqlCommand(checkRentalQuery, connection);

                try
                {
                    // เปิดการเชื่อมต่อ
                    connection.Open();

                    // ตรวจสอบว่ามีข้อมูลที่ซ้ำในตาราง rental หรือไม่
                    checkRentalCommand.Parameters.AddWithValue("@name", nametxt.Text);
                    checkRentalCommand.Parameters.AddWithValue("@tel", teltxt.Text);
                    checkRentalCommand.Parameters.AddWithValue("@item", txtname.Text);

                    int count = Convert.ToInt32(checkRentalCommand.ExecuteScalar());
                    if (count == 0)
                    {
                        // ประมวลผลคำสั่ง SQL เพื่อนำข้อมูลไปยังตาราง rental
                        insertCommand.ExecuteNonQuery();

                        // เพิ่มข้อมูลในตาราง history โดยตรวจสอบข้อมูลซ้ำ
                        insertHistoryCommand.Parameters.AddWithValue("@name", nametxt.Text);
                        insertHistoryCommand.Parameters.AddWithValue("@tel", teltxt.Text);
                        insertHistoryCommand.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        insertHistoryCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // อัปเดตข้อมูลในตาราง rental หากข้อมูลซ้ำ
                        updateRentalCommand.Parameters.AddWithValue("@name", nametxt.Text);
                        updateRentalCommand.Parameters.AddWithValue("@tel", teltxt.Text);
                        updateRentalCommand.Parameters.AddWithValue("@num", txtnum.Text);
                        updateRentalCommand.Parameters.AddWithValue("@item", txtname.Text);
                        updateRentalCommand.ExecuteNonQuery();
                    }

                    // ประมวลผลคำสั่ง SQL เพื่อลบข้อมูลในตาราง rental_backup
                    deleteCommand.ExecuteNonQuery();

                    MessageBox.Show("ดำเนินการเรียบร้อยแล้ว");

                    this.Hide();
                    Form4 form4 = new Form4();
                    form4.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
