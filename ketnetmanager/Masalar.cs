using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ketnetmanager
{
    internal class Masalar
    {
        public int GuncelDakikalıkUcret { get; set; }
        public string MasaTag { get; set; }
        public int baslangic { get; set; }
        public int bitis { get; set; }
        public int GecenSure { get; set; }
        public int ToplamBorc { get; set; }
        public bool IsAcik = false;

        private Stopwatch Kronometre = new Stopwatch();


        public Masalar(string tag)
        {

            this.MasaTag = tag;
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
                Kapat();
            }
        }

        public virtual void Kapat()
        {
            this.IsAcik = false;
            this.ToplamBorc += SureBitir() * this.GuncelDakikalıkUcret;
            LogIsle();
        }
        public virtual void Ac()
        {
            this.IsAcik = true;
            SureBaslat();
            LogIsle();
        }

        public virtual void SureBaslat()
        {
            this.Kronometre.Start();
        }

        public virtual int SureBitir()
        {
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
                    myYazici.WriteLine(today+" " + hour+ " - "+ this.IsAcik +"  " +this.MasaTag);
                } else
                {
                    myYazici.WriteLine(today + " " +hour + " - " + this.IsAcik + " " + this.MasaTag +" "+this.GecenSure);
                }
            }
        }
    }
}
