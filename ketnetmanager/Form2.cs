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

        public Form2()
        {
            InitializeComponent();


        }
        private void Form2_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100,0,0,0);
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;
            button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            
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
            }
            else
            {
                pictureBox2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //kullanıcının olup olmadığını kontrol etmemize yarayan fonksiyon
            bool KullaniciVarMi(string kullaniciAdi, string sifre)
            {
               //bağlantıyı açtık
               using (SqlConnection baglanti = new SqlConnection(sqlbaglantisi))
                {
                    string sql = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciNick = @nick AND kullaniciSifre = @sifre";

                    using (SqlCommand komut = new SqlCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@nick", kullaniciAdi);
                        komut.Parameters.AddWithValue("@sifre", sifre);

                        baglanti.Open();
                        int count = (int)komut.ExecuteScalar();
                        baglanti.Close();

                        return count > 0;
                    }
                }
            }

            if (KullaniciVarMi(textBox2.Text, textBox1.Text)) // Şifreyi hashleyerek gönderin
            {
                MessageBox.Show("Başarılı Giriş");
            }
            else
            {
                MessageBox.Show("Başarısız Giriş");
            }
        }   
    }

}
