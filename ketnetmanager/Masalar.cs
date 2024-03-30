using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;

namespace ketnetmanager
{
    internal class Masalar
    {
        public string MasaTag { get; set; }
        public int GecenSure { get; set; }
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

            LogIsle();
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
            this.Kronometre.Start();
        }

        public virtual int SureBitir()
        {

            //Minutes yap sonra.
            this.Kronometre.Stop();
            TimeSpan elapsedTime = Kronometre.Elapsed;
            int mySure = elapsedTime.Seconds;
            this.GecenSure = mySure; 
            return mySure;
        }

        public virtual void LogIsle()
        {
            string today = DateTime.Today.ToShortDateString();
            string hour = String.Format("{0:HH}:{0:mm}:{0:ss}", DateTime.Now);
            string myDosya = Path.Combine("C:\\Users\\Efe\\Desktop\\Calismalar v0.1\\ketnetmanager\\masalogs\\masalogs.txt");

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
        }
    }
}
