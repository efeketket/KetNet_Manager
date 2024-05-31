using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ketnetmanager
{
    public partial class Form3 : Form
    {

        readonly string sqlbaglantisi = Properties.Resources.sqllink;
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.Title = "Resim Seç";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Doğru dosya türü seçtiğinizden emin olun" + ex.Message);
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(sqlbaglantisi))
            {
                connection.Open();
                string insertQuery = @"
                                     INSERT INTO kafeterya (urunImg, urunIsim, urunFiyat, urunAciklama)
                                     VALUES (@img, @isim, @fiyat, @aciklama)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@img", görselEkle());
                    command.Parameters.AddWithValue("@isim", textBox1.Text);
                    command.Parameters.AddWithValue("@fiyat", Convert.ToInt32(textBox3.Text));
                    if (string.IsNullOrEmpty(textBox2.Text))
                    {
                        command.Parameters.AddWithValue("@aciklama", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@aciklama", textBox2.Text);
                    }


                    command.ExecuteNonQuery();
                }
            }
        }

        private byte[] görselEkle()
        {
            Image myGörsel = pictureBox1.Image;
            using (Bitmap resizedImage = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; 
                    g.DrawImage(myGörsel, 0, 0, 100, 100);
                }

                // Save the resized image to a MemoryStream
                using (MemoryStream stream = new MemoryStream())
                {
                    resizedImage.Save(stream, myGörsel.RawFormat);

                    return stream.ToArray();
                }

            }
        }
    }
}
