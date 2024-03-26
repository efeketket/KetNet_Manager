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

        Stopwatch Kronometre = new Stopwatch();


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

        }
        public virtual void Ac()
        {
            this.IsAcik = true;
            SureBaslat();
            LogIsle();


        }

        public virtual void SureBaslat()
        {
            Kronometre.Start();
        }

        public virtual int SureBitir()
        {
            Kronometre.Stop();
            TimeSpan elapsedTime = Kronometre.Elapsed;
            int mySure = elapsedTime.Minutes;
            return mySure;
        }

        public virtual void LogIsle()
        {
            string today = DateTime.Today.ToString().Replace('\\','_');
            string myDosya = Path.Combine("C:\\Users\\Efe\\Desktop\\Calismalar v0.1\\ketnetmanager\\masalogs", today + ".txt");

            using (StreamWriter sw = new StreamWriter(myDosya, true))
            {
                sw.WriteLine(this.MasaTag + " " + today + " " + this.IsAcik);
            }
        }
    }
}
