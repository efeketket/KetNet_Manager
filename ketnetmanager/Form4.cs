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
    public partial class Form4 : Form
    {
        readonly private int id;
        readonly string sqlbaglantisi = Properties.Resources.sqllink;

        public Form4(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            VerileriYukle();
        }

        private void VerileriYukle()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(sqlbaglantisi))
                {
                    baglanti.Open();
                    string selectQuery = "SELECT urunImg, urunIsim, urunFiyat, urunAciklama FROM kafeterya WHERE ID = @urunId";
                    using (SqlCommand komut = new SqlCommand(selectQuery, baglanti))
                    {
                        komut.Parameters.AddWithValue("@urunId", id);
                        using (SqlDataReader reader = komut.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["urunIsim"].ToString();
                                textBox3.Text = reader["urunFiyat"].ToString();
                                textBox2.Text = reader["urunAciklama"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("urunImg")))
                                {
                                    byte[] imageData = (byte[])reader["urunImg"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        pictureBox1.Image = Image.FromStream(ms);
                                        pictureBox1.BackgroundImage = Resource1.transparanth;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ürün bulunamadı.");
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Girilen ürünn bir fiyata ve isme sahip olmalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (double.TryParse(textBox3.Text, out double fiyat))
            {
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir fiyat girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection connection = new SqlConnection(sqlbaglantisi))
            {
                connection.Open();
                string updateQuery = @"
                                    UPDATE kafeterya 
                                    SET urunIsim = @isim, urunFiyat = @fiyat, urunAciklama = @aciklama, urunImg = @img
                                    WHERE ID = @id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.Parameters.AddWithValue("@img", görselEkle());
                    command.Parameters.AddWithValue("@isim", textBox1.Text);
                    command.Parameters.AddWithValue("@fiyat", Convert.ToDouble(textBox3.Text));
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

                MessageBox.Show("Ürün başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK);
                this.Close();
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
                using (MemoryStream stream = new MemoryStream())
                {
                    resizedImage.Save(stream, myGörsel.RawFormat);

                    return stream.ToArray();
                }

            }
        }

    }
}
