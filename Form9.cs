using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Net;
using System.Net.Mail;
using iText.Layout.Element;



namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

       

        private void Form9_Load(object sender, EventArgs e)
        {
           
      
            showEquipment2();
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // แสดงวันที่ปัจจุบันใน TextBox เฉพาะเมื่อไม่มีการเลือกเซลล์ใน DataGridView
            txtdate.Text = currentDate;
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
                string query = "SELECT * FROM wish2";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    DataGridViewCell nameCell = dataGridView1.Rows[e.RowIndex].Cells["name"];
                    DataGridViewCell numCell = dataGridView1.Rows[e.RowIndex].Cells["wish"];
                    DataGridViewCell dateCell = dataGridView1.Rows[e.RowIndex].Cells["date"];
                    DataGridViewCell telCell = dataGridView1.Rows[e.RowIndex].Cells["tel"];
                    DataGridViewCell idCell = dataGridView1.Rows[e.RowIndex].Cells["id"];

                    if (dateCell.Value != null && numCell.Value != null && nameCell.Value != null && telCell.Value != null)
                    {
                        if (DateTime.TryParse(dateCell.Value.ToString(), out DateTime dateValue))
                        {
                            string formattedDate = dateValue.ToString("yyyy-MM-dd");

                            // ตรวจสอบว่าข้อมูลที่จะแสดงใน txtnum มีคำว่า "บาท" หรือไม่ แล้วตัดออก
                            string numValue = numCell.Value.ToString();
                            if (numValue.EndsWith(" บาท"))
                            {
                                numValue = numValue.Remove(numValue.LastIndexOf(" บาท"));
                            }

                            txtnum.Text = numValue;
                            txtname.Text = nameCell.Value.ToString();
                            txttel.Text = telCell.Value.ToString();
                            txtid.Text = idCell.Value.ToString();
                            txtdate.Text = formattedDate;

                            txtdate.ReadOnly = true;
                            txttel.ReadOnly = true;
                            txtid.ReadOnly = true;
                            button4.Enabled = false;
                        }
                        
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }





        private void stxt_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            // เชื่อมต่อฐานข้อมูล
            connection.Open();

            // สร้างคำสั่ง SQL เพื่อดึงข้อมูล item ที่มีชื่อใกล้เคียงกับที่กรอกใน stxt
            string query = "SELECT * FROM wish2 WHERE tel LIKE @tel";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@tel", "%" + stxt.Text + "%");

            // สร้าง DataAdapter และ DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable2 = new DataTable();
            adapter.Fill(dataTable2);

            // ปิดการเชื่อมต่อฐานข้อมูล
            connection.Close();

            // กำหนด DataSource ของ DataGridView
            dataGridView1.DataSource = dataTable2;


        }

        private void txtdate_TextChanged(object sender, EventArgs e)
        {
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
          

            // แสดงวันที่ปัจจุบันใน TextBox เฉพาะเมื่อไม่มีการเลือกเซลล์ใน DataGridView
            txtdate.Text = currentDate;
            txtname.Text = string.Empty;
            txttel.Text = string.Empty;
           
            txtnum.Text = string.Empty;
            txtid.Text = string.Empty;
           
            txttel.ReadOnly = false;
            txtdate.ReadOnly = false;
           
            txtid.ReadOnly = false;
            button4.Enabled = true;
        }

        private void UpdateWish2Table(int id, string name, string tel, string date, string num)
        {
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string updateQuery = "UPDATE wish2 SET name = @name, tel = @tel, date = @date, wish = @num WHERE id = @id";
                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@tel", tel);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการอัปเดตข้อมูลในตาราง wish2: " + ex.Message);
                }
            }
            showEquipment2();
        }

        private async Task SendEmailWithAttachment(string recipientEmail, string attachmentPath)
        {
            try
            {
                string senderEmail = "panravat00@gmail.com";
                string senderPassword = "ueub ipba zahh gdka";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(senderEmail);
                message.Subject = "PDF Document";
                message.Body = "Please find attached the PDF document.";

                message.To.Add(new MailAddress(recipientEmail));

                Attachment attachment = new Attachment(attachmentPath);
                message.Attachments.Add(attachment);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                await smtpClient.SendMailAsync(message);

                MessageBox.Show("ส่งอีเมลเรียบร้อยค่ะ อนุโมทนาเด้อ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
            
        }

        private void stxt_Click(object sender, EventArgs e)
        {
            stxt.Text = string.Empty;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            Form12 form12 = new Form12();
            form12.ShowDialog();
           
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtname.Text) && !string.IsNullOrEmpty(txtnum.Text) && !string.IsNullOrEmpty(txtdate.Text) && !string.IsNullOrEmpty(txttel.Text))
            {
                string name = txtname.Text;
                string tel = txttel.Text;
                string date = txtdate.Text;
                string num = txtnum.Text;

                if (DateTime.TryParse(date, out DateTime parsedDate))
                {
                    string connectionString = "server=localhost;user id=root;password=;database=admin;";
                    MySqlConnection connection = new MySqlConnection(connectionString);

                    try
                    {
                        connection.Open();

                        string insertQueryWish2 = "INSERT INTO wish2 (name, tel, wish, date) VALUES (@name, @tel, @wish, @date)";
                        MySqlCommand cmdWish2 = new MySqlCommand(insertQueryWish2, connection);
                        cmdWish2.Parameters.AddWithValue("@name", name);
                        cmdWish2.Parameters.AddWithValue("@tel", tel);
                        cmdWish2.Parameters.AddWithValue("@wish", num + " บาท");
                        cmdWish2.Parameters.AddWithValue("@date", parsedDate.ToString("yyyy-MM-dd"));
                        cmdWish2.ExecuteNonQuery();

                        MessageBox.Show("เพิ่มข้อมูลเรียบร้อยแล้ว");
                        showEquipment2();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูล: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("กรุณากรอกวันที่ในรูปแบบที่ถูกต้อง (yyyy-MM-dd)");
                    return;
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง");
                return;
            }

            try
            {
                string imagePath = @"C:\Users\HP\Downloads\อนุโมทนาบัตร (1).png";
                Document doc = new Document(PageSize.A4.Rotate());
                string outputFolderPath = @"D:\";
                string baseFileName = txtname.Text + "อนุโมทนา";
                string outputFilePath = Path.Combine(outputFolderPath, baseFileName + ".pdf");

                int fileNumber = 1;
                while (File.Exists(outputFilePath))
                {
                    outputFilePath = Path.Combine(outputFolderPath, baseFileName + "_" + fileNumber + ".pdf");
                    fileNumber++;
                }

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputFilePath, FileMode.Create));
                doc.Open();

                iTextSharp.text.Image bgImage = iTextSharp.text.Image.GetInstance(imagePath);
                bgImage.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);
                bgImage.SetAbsolutePosition(20, 0);
                doc.Add(bgImage);

                PdfContentByte cb = writer.DirectContent;
                BaseFont baseFont = BaseFont.CreateFont(@"C:\c#project\WindowsFormsApp1\Kart-Phuththkhun.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);



                float margin = 50; // ระยะขอบจากซ้ายและขวา

                // คำนวณความกว้างของหน้าเอกสาร
                float pageWidth = doc.PageSize.Width - (2 * margin);

                // คำนวณความกว้างของข้อความ
                float textWidth = baseFont.GetWidthPoint(txtname.Text, 50);

                // คำนวณตำแหน่ง x ที่จะให้ข้อความอยู่ตรงกลางระหว่างขอบซ้ายและขวา
                float xPosition = (doc.PageSize.Width - textWidth) / 2;

                cb.BeginText();
                cb.SetFontAndSize(font.BaseFont, 50);
                cb.SetTextMatrix(xPosition, 400); // ใช้ตำแหน่งที่คำนวณได้
                cb.ShowText(txtname.Text);
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(font.BaseFont, 30);
                cb.SetTextMatrix(400, 270);
                cb.ShowText(txtnum.Text);
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(font.BaseFont, 30);
                cb.SetTextMatrix(550, 140);
                cb.ShowText(txtdate.Text);
                cb.EndText();

                doc.Close();
                MessageBox.Show("ดำเนินการเรียบร้อยแล้ว");

                if (!string.IsNullOrEmpty(txtmail.Text))
                {
                    await SendEmailWithAttachment(txtmail.Text, outputFilePath);
                }
                else
                {
                    MessageBox.Show("สาธุค่ะ");
                }
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");


                // แสดงวันที่ปัจจุบันใน TextBox เฉพาะเมื่อไม่มีการเลือกเซลล์ใน DataGridView
                txtdate.Text = currentDate;
                txtname.Text = string.Empty;
                txttel.Text = string.Empty;

                txtnum.Text = string.Empty;
                txtid.Text = string.Empty;
                txtname.ReadOnly = false;
                txttel.ReadOnly = false;
                txtdate.ReadOnly = false;
                txtnum.ReadOnly = false;
                txtid.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtnum_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = txtnum.SelectionStart;

            // ลบการสมัครสมาชิกชั่วคราวเพื่อลดการเรียกใช้ซ้ำ
            txtnum.TextChanged -= txtnum_TextChanged;

            // เก็บค่าเดิมที่ไม่มีคอมม่า
            string originalText = txtnum.Text.Replace(",", "");

            // ตรวจสอบว่าเป็นตัวเลขหรือไม่
            if (int.TryParse(originalText, out int num))
            {
                // แปลงตัวเลขให้มีคอมม่า
                txtnum.Text = num.ToString("N0");
            }

            // กำหนดตำแหน่งเคอร์เซอร์ใหม่หลังจากที่มีการปรับปรุงข้อความ
            cursorPosition = Math.Min(cursorPosition + (txtnum.Text.Length - originalText.Length), txtnum.Text.Length);

            // สมัครสมาชิกเหตุการณ์ TextChanged ใหม่
            txtnum.TextChanged += txtnum_TextChanged;

            // กำหนดตำแหน่งเคอร์เซอร์ใหม่
            txtnum.SelectionStart = cursorPosition;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // เช็คว่าทุกช่องไม่เป็นค่าว่าง
            if (!string.IsNullOrEmpty(txtname.Text) && !string.IsNullOrEmpty(txttel.Text) &&
                !string.IsNullOrEmpty(txtdate.Text) && !string.IsNullOrEmpty(txtnum.Text) &&
                !string.IsNullOrEmpty(txtid.Text))
            {
                // ดึงข้อมูลจาก TextBoxes
                string name = txtname.Text;
                string tel = txttel.Text;
                string date = txtdate.Text;
                string num = txtnum.Text + " บาท"; // เพิ่มคำว่า "บาท" ต่อท้าย

                // เช็คว่า ID เป็นตัวเลขหรือไม่
                if (!int.TryParse(txtid.Text, out int id))
                {
                    MessageBox.Show("กรุณากรอกไอดีให้เป็นตัวเลข");
                    return;
                }

                // อัปเดตข้อมูลในตาราง wish2
                UpdateWish2Table(id, name, tel, date, num);

                // แสดงข้อความแจ้งเตือน
                MessageBox.Show("อัปเดตข้อมูลเรียบร้อยแล้ว");

                // เปิดให้แก้ไขข้อมูลใน TextBoxes อีกครั้ง
                

                string imagePath = @"C:\Users\HP\Downloads\อนุโมทนาบัตร (1).png";
                Document doc = new Document(PageSize.A4.Rotate());
                string outputFolderPath = @"D:\";
                string baseFileName = txtname.Text + "อนุโมทนา";
                string outputFilePath = Path.Combine(outputFolderPath, baseFileName + ".pdf");

                int fileNumber = 1;
                while (File.Exists(outputFilePath))
                {
                    outputFilePath = Path.Combine(outputFolderPath, baseFileName + "_" + fileNumber + ".pdf");
                    fileNumber++;
                }

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputFilePath, FileMode.Create));
                doc.Open();

                iTextSharp.text.Image bgImage = iTextSharp.text.Image.GetInstance(imagePath);
                bgImage.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);
                bgImage.SetAbsolutePosition(20, 0);
                doc.Add(bgImage);

                PdfContentByte cb = writer.DirectContent;
                BaseFont baseFont = BaseFont.CreateFont(@"C:\c#project\WindowsFormsApp1\Kart-Phuththkhun.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);

                float margin = 50; // ระยะขอบจากซ้ายและขวา

                // คำนวณความกว้างของหน้าเอกสาร
                float pageWidth = doc.PageSize.Width - (2 * margin);

                // คำนวณความกว้างของข้อความ
                float textWidth = baseFont.GetWidthPoint(txtname.Text, 50);

                // คำนวณตำแหน่ง x ที่จะให้ข้อความอยู่ตรงกลางระหว่างขอบซ้ายและขวา
                float xPosition = (doc.PageSize.Width - textWidth) / 2;

                cb.BeginText();
                cb.SetFontAndSize(font.BaseFont, 50);
                cb.SetTextMatrix(xPosition, 400); // ใช้ตำแหน่งที่คำนวณได้
                cb.ShowText(txtname.Text);
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(font.BaseFont, 30);
                cb.SetTextMatrix(400, 270);
                cb.ShowText(txtnum.Text);
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(font.BaseFont, 30);
                cb.SetTextMatrix(550, 140);
                cb.ShowText(txtdate.Text);
                cb.EndText();

                doc.Close();
                MessageBox.Show("ดำเนินการเรียบร้อยแล้ว");

                if (!string.IsNullOrEmpty(txtmail.Text))
                {
                    await SendEmailWithAttachment(txtmail.Text, outputFilePath);
                }
                else
                {
                    MessageBox.Show("สาธุค่ะ");
                }
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");


                // แสดงวันที่ปัจจุบันใน TextBox เฉพาะเมื่อไม่มีการเลือกเซลล์ใน DataGridView
                txtdate.Text = currentDate;
                txtname.Text = string.Empty;
                txttel.Text = string.Empty;

                txtnum.Text = string.Empty;
                txtid.Text = string.Empty;
                txtname.ReadOnly = false;
                txttel.ReadOnly = false;
                txtdate.ReadOnly = false;
                txtnum.ReadOnly = false;
                txtid.ReadOnly = false;
                button4.Enabled = true;

            }
            else
            {
                // แจ้งเตือนให้กรอกข้อมูลให้ครบทุกช่อง
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
