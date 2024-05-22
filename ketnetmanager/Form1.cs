using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ketnetmanager
{
    public partial class Form1 : Form
    {
            readonly Masalar masa1 = new Masalar("masa1");
            readonly Masalar masa2 = new Masalar("masa2");
            readonly Masalar masa3 = new Masalar("masa3");
            readonly Masalar masa4 = new Masalar("masa4");
            readonly Masalar masa5 = new Masalar("masa5");
            readonly Masalar masa6 = new Masalar("masa6");
            readonly Masalar masa7 = new Masalar("masa7");
            readonly Masalar masa8 = new Masalar("masa8");
            readonly Masalar masa9 = new Masalar("masa9");
            readonly Masalar masa10 = new Masalar("masa10");
            readonly Masalar masa11 = new Masalar("masa11");
            readonly Masalar masa12 = new Masalar("masa12");
            readonly Masalar masa13 = new Masalar("masa13");
            readonly Masalar masa14 = new Masalar("masa14");
            readonly Masalar masa15 = new Masalar("masa15");
            readonly Masalar masa16 = new Masalar("masa16");
            readonly Masalar masa17 = new Masalar("masa17");
            readonly Masalar masa18 = new Masalar("masa18");
            readonly Masalar masa19 = new Masalar("masa19");
            readonly Masalar masa20 = new Masalar("masa20");
            readonly Masalar masa21 = new Masalar("masa21");
            readonly Image offmonitor = ketnetmanager.Resource1.offmonitor;
            readonly Image onmonitor = ketnetmanager.Resource1.onmonitor;
            readonly string myDosya = Resource1.masalogs;
            public double saatlikUcret;
            public double kazanc;


        public Form1()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void masaBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void açKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAcik = false;   

            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)clickedItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            string tag = pictureBox.Tag.ToString();

            switch (tag)
                {
                    case "masa1":
                        label1.Text = tag;
                        masa1.AcKapat(saatlikUcret);
                        isAcik = masa1.IsAcik;
                        kazanc += masa1.ToplamBorc;
                    break;
                    case "masa2":
                        label1.Text = tag;
                        masa2.AcKapat(saatlikUcret);
                        isAcik = masa2.IsAcik;
                        kazanc += masa2.ToplamBorc;
                    break;
                    case "masa3":
                        label1.Text = tag;
                        masa3.AcKapat(saatlikUcret);
                        isAcik = masa3.IsAcik;
                        kazanc += masa3.ToplamBorc;
                    break;
                    case "masa4":
                        label1.Text = tag;
                        masa4.AcKapat(saatlikUcret);
                        isAcik = masa4.IsAcik;
                        kazanc += masa4.ToplamBorc;
                    break;
                    case "masa5":
                        label1.Text = tag;
                        masa5.AcKapat(saatlikUcret);
                        isAcik = masa5.IsAcik;
                        kazanc += masa5.ToplamBorc;
                    break;
                    case "masa6":
                        label1.Text = tag;
                        masa6.AcKapat(saatlikUcret);
                        isAcik = masa6.IsAcik;
                        kazanc += masa6.ToplamBorc;
                    break;
                    case "masa7":
                        label1.Text = tag;
                        masa7.AcKapat(saatlikUcret);
                        isAcik = masa7.IsAcik;
                        kazanc += masa7.ToplamBorc;
                    break;
                    case "masa8":
                        label1.Text = tag;
                        masa8.AcKapat(saatlikUcret);
                        isAcik = masa8.IsAcik;
                        kazanc += masa8.ToplamBorc;
                    break;
                    case "masa9":
                        label1.Text = tag;
                        masa9.AcKapat(saatlikUcret);
                        isAcik = masa9.IsAcik;
                        kazanc += masa9.ToplamBorc;
                    break;
                    case "masa10":
                        label1.Text = tag;
                        masa10.AcKapat(saatlikUcret);
                        isAcik = masa10.IsAcik;
                        kazanc += masa10.ToplamBorc;
                    break;
                    case "masa11":
                        label1.Text = tag;
                        masa11.AcKapat(saatlikUcret);
                        isAcik = masa11.IsAcik;
                        kazanc += masa11.ToplamBorc;
                    break;
                    case "masa12":
                        label1.Text = tag;
                        masa12.AcKapat(saatlikUcret);
                        isAcik = masa12.IsAcik;
                        kazanc += masa12.ToplamBorc;
                    break;
                    case "masa13":
                        label1.Text = tag;
                        masa13.AcKapat(saatlikUcret);
                        isAcik = masa13.IsAcik;
                        kazanc += masa13.ToplamBorc;
                    break;
                    case "masa14":
                        label1.Text = tag;
                        masa14.AcKapat(saatlikUcret);
                        isAcik = masa14.IsAcik;
                        kazanc += masa14.ToplamBorc;
                    break;
                    case "masa15":
                        label1.Text = tag;
                        masa15.AcKapat(saatlikUcret);
                        isAcik = masa15.IsAcik;
                        kazanc += masa15.ToplamBorc;
                    break;
                    case "masa16":
                        label1.Text = tag;
                        masa16.AcKapat(saatlikUcret);
                        isAcik = masa16.IsAcik;
                        kazanc += masa16.ToplamBorc;
                    break;
                    case "masa17":
                        label1.Text = tag;
                        masa17.AcKapat(saatlikUcret);
                        isAcik = masa17.IsAcik;
                        kazanc += masa17.ToplamBorc;
                    break;
                    case "masa18":
                        label1.Text = tag;
                        masa18.AcKapat(saatlikUcret);
                        isAcik = masa18.IsAcik;
                        kazanc += masa18.ToplamBorc;
                    break;
                    case "masa19":
                        label1.Text = tag;
                        masa19.AcKapat(saatlikUcret);
                        isAcik = masa19.IsAcik;
                        kazanc += masa19.ToplamBorc;
                    break;
                    case "masa20":
                        label1.Text = tag;
                        masa20.AcKapat(saatlikUcret);
                        isAcik = masa20.IsAcik;
                        kazanc += masa20.ToplamBorc;
                    break;
                    case "masa21":
                        label1.Text = tag;
                        masa21.AcKapat(saatlikUcret);
                        isAcik = masa21.IsAcik;
                        kazanc += masa21.ToplamBorc;
                    break;
                }

            if (isAcik == true)
            {
                pictureBox.Image = onmonitor;
            } else
            {
                pictureBox.Image = offmonitor;
            }

            label27.Text = Convert.ToString(Math.Round(kazanc,2));

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string resource_data = Resource1.masalogs;
            List<string> logList = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            label27.Text = logList.Count.ToString();
            textBox2.Lines = logList.ToArray();
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.ReadOnly = true;

            groupBox1.Hide();
            groupBox4.Show();
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out saatlikUcret))
            {
                saatlikUcret = Convert.ToDouble(saatlikUcret);
                label25.Text = textBox1.Text;
                textBox1.Text = "";
            } else
            {
                MessageBox.Show("Lütfen geçerli bir ücret değeri girin.");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox3.Show();
            groupBox1.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
            groupBox3.Hide();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Ürünler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {

            using (StreamWriter myYazici = new StreamWriter(myDosya, true))
            {
                myYazici.Write(myDosya);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
            groupBox4.Hide();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                saatlikUcret = Convert.ToInt32(textBox1.Text);
                label25.Text = Convert.ToString(saatlikUcret);
                MessageBox.Show("Saatlik ücret " + saatlikUcret + "₺ olarak ayarlandı.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.", "Geçersiz Değer!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Girilen sayı çok büyük veya çok küçük.", "Geçersiz Değer!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
