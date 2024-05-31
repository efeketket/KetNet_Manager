using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ketnetmanager
{
    public partial class Form5 : Form
    {
        readonly string sqlbaglanti = Properties.Resources.sqllink;
        public int seciliadet = 1;
        public double totalFiyat = 0;
        public double selectedFiyat = 0;
        public string masatag;
        public Form5(string masatag)
        {
            InitializeComponent();
            this.masatag = masatag;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
            string komut = "SELECT urunIsim FROM kafeterya";

            using (SqlConnection connection = new SqlConnection(sqlbaglanti))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(komut, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader["urunIsim"].ToString());
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFiyat = GetFiyat(comboBox1.SelectedItem.ToString()) * seciliadet;
            label2.Text = (GetFiyat(comboBox1.SelectedItem.ToString()) * seciliadet).ToString();


        }

        private double GetFiyat(string secilen)
        {
                try
                {
                    using (SqlConnection connection = new SqlConnection(sqlbaglanti))
                    {
                        connection.Open();
                        string query = "SELECT urunFiyat FROM kafeterya WHERE urunIsim = @urunAdi";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@urunAdi", secilen);
                            double myFiyat = Convert.ToDouble(command.ExecuteScalar());
                            return myFiyat;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    return 0;
                }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
             seciliadet = Convert.ToInt32(numericUpDown1.Value);
             selectedFiyat = GetFiyat(comboBox1.SelectedItem.ToString()) * seciliadet;

            label2.Text = (GetFiyat(comboBox1.SelectedItem.ToString()) * seciliadet).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.totalFiyat = selectedFiyat;
            SatisLogIsle();
            Close();
        }

        private void SatisLogIsle() 
        {
            using (SqlConnection baglanti = new SqlConnection(sqlbaglanti))
            {
                baglanti.Open();
                string insertQuery = @"
                                     INSERT INTO satisLogs (Tarih, Saat, Durum, HedefMasa, SatilanOge,kazanc)
                                     VALUES (@tarih, @saat, @durum, @hedefmasa, @satilanoge, @kazanc)";

                using (SqlCommand command = new SqlCommand(insertQuery, baglanti))
                {
                    command.Parameters.AddWithValue("@tarih", DateTime.Today.ToShortDateString());
                    command.Parameters.AddWithValue("@saat", DateTime.Now.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@durum", "Satış.");
                    command.Parameters.AddWithValue("@hedefmasa", this.masatag);
                    command.Parameters.AddWithValue("@satilanoge", comboBox1.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@kazanc", this.totalFiyat);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
