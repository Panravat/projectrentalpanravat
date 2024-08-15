using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

using iText.Layout.Element;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // สุ่มหมายเลข 1-26
            Random random = new Random();
            int randomNumber = random.Next(1, 27);
            string connectionString = "server=localhost;user id=root;password=;database=admin;";
            // สร้างการเชื่อมต่อฐานข้อมูล
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // เปิดการเชื่อมต่อ
                connection.Open();

                // สร้างคำสั่ง SQL สำหรับดึงข้อมูลรูปภาพที่มี id เท่ากับ randomNumber
                string query = "SELECT pic FROM random WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", randomNumber);

                // สร้างอ่านข้อมูลรูปภาพ
                byte[] imageData = (byte[])command.ExecuteScalar();

                // แสดงรูปภาพในกล่องรูปภาพ
                if (imageData != null && imageData.Length > 0)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imageData))
                    {
                       pic1.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                else
                {
                    MessageBox.Show("ผิดพลาดกรุณาลองใหม่: " + randomNumber);
                }
            }

        }



        // ฟังก์ชันสำหรับส่งเมลพร้อมพีดีเอฟ


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void CreatePDFFromImage1(System.Drawing.Image image, string outputFilePath)
        {
            Document document = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter writer = null;

            try
            {
                using (FileStream fs = new FileStream(outputFilePath, FileMode.Create))
                {
                    writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    // ปรับขนาดรูปภาพ
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                    pdfImage.ScaleToFit(PageSize.A4.Width - document.LeftMargin - document.RightMargin, PageSize.A4.Height - document.TopMargin - document.BottomMargin);
                    pdfImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                    // เพิ่มรูปภาพในเอกสาร PDF
                    document.Add(pdfImage);
                    document.Close();
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
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

                MessageBox.Show("ดำเนินการเสร็จสิ้น.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ผิดพลาดกรุณาลองใหม่: " + ex.Message);
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ากล่องรูปมีรูปภาพหรือไม่
            if (pic1.Image != null)
            {
                // ตรวจสอบว่าอีเมล์ถูกกรอกหรือไม่
                if (!string.IsNullOrEmpty(txtmail.Text))
                {
                    // สร้างไฟล์ PDF จากรูปภาพ
                    string outputFilePath = GetUniqueFilePath(Path.Combine(Path.GetTempPath(), "ใบเซียมซี.pdf")); // ระบุชื่อไฟล์ PDF ที่ต้องการเก็บผลลัพธ์

                    CreatePDFFromImage1(pic1.Image, outputFilePath);

                    // ส่งอีเมล์พร้อมแนบไฟล์ PDF
                    await SendEmailWithAttachment(txtmail.Text, outputFilePath);
                }
                else
                {
                    MessageBox.Show("กรุณาลองใหม่อีกครั้ง");
                }
            }
            else
            {
                MessageBox.Show("กรุณาลองใหม่อีกครั้ง");
            }
        }

        // เมธอดเพื่อตั้งชื่อไฟล์ใหม่เพื่อป้องกันชื่อซ้ำ
        private string GetUniqueFilePath(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = Path.GetExtension(filePath);
            int count = 1;

            while (File.Exists(filePath))
            {
                filePath = Path.Combine(directory, $"{fileNameWithoutExtension}_{count}{fileExtension}");
                count++;
            }

            return filePath;
        }


        


    }
}
