using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ketnetmanager
{
    public partial class Form2 : Form
    {
        readonly string sqlbaglantisi = Properties.Resources.sqllink;
        bool isHide = true;

        public Form2()
        {
            InitializeComponent();


        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //manuel görsel değişiklikler
            this.panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            this.panel2.BorderStyle = BorderStyle.None;
            this.panel3.BorderStyle = BorderStyle.None;
            this.button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
            this.textBox1.PasswordChar = '*';
        }


        //varsa true, yoksa false alır
        public bool kullaniciKontrol(string kullaniciAdi, string sifre) 
        {
            //bağlantıyı açtık
            using (SqlConnection baglanti = new SqlConnection(sqlbaglantisi))
            {
                string ara = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciNick = @kullaniciad AND kullaniciSifre = @sifre";
                //gönderdiğimiz query verdiğimiz değişkenlerden kaç tane tablomuzda var onu sayıyor

                using (SqlCommand komut = new SqlCommand(ara, baglanti))
                {
                    komut.Parameters.AddWithValue("@kullaniciad", kullaniciAdi);
                    komut.Parameters.AddWithValue("@sifre", sifre);

                    baglanti.Open();
                    int count = (int)komut.ExecuteScalar();
                    baglanti.Close();

                    // kaç tane kayıt olduğuna bakar. 1'den büyükse 1 alır true döndürür
                    return count > 0;
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                pictureBox1.Show();
            } else
            {
                pictureBox1.Hide();
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (kullaniciKontrol(textBox2.Text, textBox1.Text) == true) // Şifreyi hashleyerek gönderin
            {
                
                Form1 form = new Form1();
                this.Hide();
                form.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Başarısız Giriş");
            }
        }


        //textboxa girildiğinde yürütülen fonksiyonlar
        private void textBox2_Enter(object sender, EventArgs e)
        {
            panel2.BackgroundImage = Resource1.bgimgbar2gray;
            textBox2.BackColor = Color.LightGray;

            panel3.BackgroundImage = Resource1.bgimgbar2white;
            textBox1.BackColor = Color.White;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            panel2.BackgroundImage = Resource1.bgimgbar2white;
            textBox2.BackColor = Color.White;

            panel3.BackgroundImage = Resource1.bgimgbar2gray;
            textBox1.BackColor = Color.LightGray;
        }


        //şifre göster-sakla fonksiyonu
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (isHide == false)
            {
                //şifreyi saklama fonksiyonları
                textBox1.PasswordChar = '*';
                pictureBox3.Image = Resource1.seepaswordtransph;
                isHide = true;
            } else
            {
                //şifreyi gösterme fonksiyonları
                textBox1.PasswordChar = (char)0;
                pictureBox3.Image = Resource1.hidepasswordtransph;
                isHide = false;
            }
        }
    }

}
