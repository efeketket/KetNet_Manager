using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ketnetmanager
{
    public partial class Form4 : Form
    {
        private Image urunImg;
        private string urunIsim;
        private string urunAciklama;
        private double urunFiyat;

        public Form4(Image urunImg, string urunIsim, string urunAciklama, double urunFiyat)
        {
            InitializeComponent();
            this.urunImg = urunImg;
            this.urunIsim = urunIsim;
            this.urunAciklama = urunAciklama;
            this.urunFiyat = urunFiyat;

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
