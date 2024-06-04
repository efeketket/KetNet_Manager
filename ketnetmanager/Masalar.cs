using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace ketnetmanager
{
    internal class Masalar 
    {
        readonly string sqlbaglantisi = Properties.Resources.sqllink;

        public string myDosya = Resource1.masalogs;

        public double saatlikUcret { get; set; }
        public string MasaTag { get; set; }
        public string GecenSure { get; set; }
        public double ToplamBorc { get; set; }
        public bool IsAcik = false;
        public int segment = 0;

        private Stopwatch Kronometre = new Stopwatch();

        public Masalar(string tag,Dictionary<string,double> fiyatList)
        {
            this.MasaTag = tag;
            this.saatlikUcret = fiyatList.Values.ElementAt(getMasaData("masaDurum","masaSegment"));
        }



        //verilen table ve sütuna göre gerekli datayı masanın tablosundan çeken method.
        public int getMasaData(string aranantablo,string aranansutun)
        {
            int deger = 0;

            using (SqlConnection connection = new SqlConnection(sqlbaglantisi))
            {
                connection.Open();
                string query = "SELECT " + aranansutun + " FROM " + aranantablo + " WHERE masaTag = @masaTag";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@masaTag", this.MasaTag);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            deger = Convert.ToInt32(reader[aranansutun]);
                        }
                    }
                }
            }

            return deger;
        }

        public virtual void AcKapat()
        {
            bool isAcik = this.IsAcik;

            if (isAcik == false)
            {
                Ac();
            }
            else
            {
                Kapat(this.saatlikUcret);
            }

            LogKaydet();
        }

        public void setUcret(double saatlikUcret,int segment)
        {
            DataBaseKaydet("masaSegment", segment);
            this.segment = segment;
            this.saatlikUcret = saatlikUcret;
            
        }
        public double getUcret()
        {
            return this.saatlikUcret;
        }

        public virtual void Kapat(double SaatlikUcret)
        {
            DataBaseKaydet("masaDurum", 0);
            this.IsAcik = false;
            this.ToplamBorc += SureBitir() * (getUcret() / 60) ;

        }
        public virtual void Ac()
        {
            if (Kronometre.IsRunning == false)
            {
                DataBaseKaydet("masaDurum", 1);
                this.ToplamBorc = 0;
                this.IsAcik = true;
                SureBaslat();
            }
        }

        public virtual void SureBaslat()
        {
            Kronometre.Reset();
            this.GecenSure = "";
            this.Kronometre.Start();
        }

        public virtual double SureBitir()
        {
            this.Kronometre.Stop();
            TimeSpan elapsedTime = Kronometre.Elapsed;
            //Minutes yap sonra.
            double mySure = elapsedTime.TotalSeconds;
            this.GecenSure = string.Format("{0:D2}:{1:D2}", (int)elapsedTime.Minutes, elapsedTime.Seconds);
            return mySure;
        }


        public virtual void LogKaydet()
        {

            using (SqlConnection connection = new SqlConnection(sqlbaglantisi))
            {
                connection.Open();
                string insertQuery = @"
                                     INSERT INTO LogDefteri (Tarih, Saat, Durum, MasaTag, GecenSure, ToplamBorc)
                                     VALUES (@tarih, @saat, @durum, @masaTag, @gecenSure, @toplamBorc)";
                
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@tarih", DateTime.Today.ToShortDateString());
                    command.Parameters.AddWithValue("@saat", DateTime.Now.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@durum", this.IsAcik ? "Açıldı" : "Kapatıldı");
                    command.Parameters.AddWithValue("@masaTag", this.MasaTag);
                    command.Parameters.AddWithValue("@gecenSure", this.GecenSure);
                    command.Parameters.AddWithValue("@toplamBorc", this.ToplamBorc);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UrunSat()
        {
            Form5 form = new Form5(this.MasaTag);
            form.ShowDialog();
            ToplamBorc += form.totalFiyat;

        }

        public void DataBaseKaydet(string sutun,int deger)
        {
            using (SqlConnection connection = new SqlConnection(sqlbaglantisi))
            {
                connection.Open();
                string updateQuery = @"
                    UPDATE masaDurum 
                    SET " + sutun + " =@deger WHERE MasaTag = @masaTag";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@deger", deger);
                    command.Parameters.AddWithValue("@masaTag", this.MasaTag);

                    command.ExecuteNonQuery();
                }
            }
        }


        //eski method dosyaya işliyor
        /*public virtual void LogIsle()
        {
            string today = DateTime.Today.ToShortDateString();
            string hour = String.Format("{0:HH}:{0:mm}:{0:ss}", DateTime.Now);

            using (StreamWriter myYazici = new StreamWriter(myDosya, true))
            {

                if (this.IsAcik == true)
                {
                    myYazici.WriteLine("[" + today + "]" + " " + "[" + hour + "]" + " - " + this.IsAcik +" "+this.MasaTag);
                } else
                {
                    myYazici.WriteLine("[" + today + "]" + " " + "[" +hour + "]" +" - " + this.IsAcik + " " + this.MasaTag);
                    myYazici.WriteLine("                         Geçen Süre : " + this.GecenSure + " Masa Borcu : " + this.ToplamBorc);
                }
            }
        }*/


    }
}
