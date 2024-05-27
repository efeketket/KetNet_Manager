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
        readonly string connectionString = Properties.Resources.sqllink;

        public string myDosya = Resource1.masalogs;

        public string MasaTag { get; set; }
        public string GecenSure { get; set; }
        public double ToplamBorc { get; set; }
        public bool IsAcik = false;

        private Stopwatch Kronometre = new Stopwatch();

        public Masalar(string tag)
        {

            this.MasaTag = tag;
        }

        public virtual void AcKapat(double SaatlikUcret)
        {
            bool isAcik = this.IsAcik;

            if (isAcik == false)
            {
                Ac();
            }
            else
            {
                Kapat(SaatlikUcret);
            }

            LogKaydet();
        }

        public virtual void Kapat(double SaatlikUcret)
        {
            this.IsAcik = false;
            this.ToplamBorc += SureBitir() * (SaatlikUcret / 60) ;

        }
        public virtual void Ac()
        {
            this.IsAcik = true;
            SureBaslat();
        }

        public virtual void SureBaslat()
        {
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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
