using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ketnetmanager
{
    public partial class Form1 : Form
    {
            Masalar masa1 = new Masalar("masa1");
            Masalar masa2 = new Masalar("masa2");
            Masalar masa3 = new Masalar("masa3");
            Masalar masa4 = new Masalar("masa4");
            Masalar masa5 = new Masalar("masa5");
            Masalar masa6 = new Masalar("masa6");
            Masalar masa7 = new Masalar("masa7");
            Masalar masa8 = new Masalar("masa8");
            Masalar masa9 = new Masalar("masa9");
            Masalar masa10 = new Masalar("masa10");
            Masalar masa11 = new Masalar("masa11");
            Masalar masa12 = new Masalar("masa12");
            Masalar masa13 = new Masalar("masa13");
            Masalar masa14 = new Masalar("masa14");
            Masalar masa15 = new Masalar("masa15");
            Masalar masa16 = new Masalar("masa16");
            Masalar masa17 = new Masalar("masa17");
            Masalar masa18 = new Masalar("masa18");
            Masalar masa19 = new Masalar("masa19");
            Masalar masa20 = new Masalar("masa20");
            Masalar masa21 = new Masalar("masa21");

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void masaBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void açKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Image offmonitor = ketnetmanager.Resource1.offmonitor;
                Image onmonitor = ketnetmanager.Resource1.onmonitor;
                bool isAcik = false;    

                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                ContextMenuStrip menu = (ContextMenuStrip)clickedItem.Owner;
                PictureBox pictureBox = (PictureBox)menu.SourceControl;

                string tag = pictureBox.Tag.ToString();


            switch (tag)
                {
                    case "masa1":
                        label1.Text = tag;
                        masa1.AcKapat();
                    isAcik = masa1.IsAcik;
                    break;
                    case "masa2":
                        label1.Text = tag;
                        masa2.AcKapat();
                    isAcik = masa2.IsAcik;
                    break;
                    case "masa3":
                        label1.Text = tag;
                        masa3.AcKapat();
                    isAcik = masa3.IsAcik;
                    break;
                    case "masa4":
                        label1.Text = tag;
                        masa4.AcKapat();
                    isAcik = masa4.IsAcik;
                    break;
                    case "masa5":
                        label1.Text = tag;
                        masa5.AcKapat();
                        isAcik = masa5.IsAcik;
                    break;
                    case "masa6":
                        label1.Text = tag;
                        masa6.AcKapat();
                    isAcik = masa1.IsAcik;
                    break;
                    case "masa7":
                        label1.Text = tag;
                        masa7.AcKapat();
                    isAcik = masa7.IsAcik;
                    break;
                    case "masa8":
                        label1.Text = tag;
                        masa8.AcKapat();
                    isAcik = masa8.IsAcik;
                    break;
                    case "masa9":
                        label1.Text = tag;
                        masa9.AcKapat();
                    isAcik = masa9.IsAcik;
                    break;
                    case "masa10":
                        label1.Text = tag;
                        masa10.AcKapat();
                    isAcik = masa10.IsAcik;
                    break;
                    case "masa11":
                        label1.Text = tag;
                        masa11.AcKapat();
                    isAcik = masa11.IsAcik;
                    break;
                    case "masa12":
                        label1.Text = tag;
                        masa12.AcKapat();
                    isAcik = masa12.IsAcik;
                    break;
                    case "masa13":
                        label1.Text = tag;
                        masa13.AcKapat();
                    isAcik = masa13.IsAcik;
                    break;
                    case "masa14":
                        label1.Text = tag;
                        masa14.AcKapat();
                    isAcik = masa14.IsAcik;
                    break;
                    case "masa15":
                        label1.Text = tag;
                        masa15.AcKapat();
                    isAcik = masa15.IsAcik;
                    break;
                    case "masa16":
                        label1.Text = tag;
                        masa16.AcKapat();
                    isAcik = masa16.IsAcik;
                    break;
                    case "masa17":
                        label1.Text = tag;
                        masa17.AcKapat();
                    isAcik = masa17.IsAcik;
                    break;
                    case "masa18":
                        label1.Text = tag;
                        masa18.AcKapat();
                    isAcik = masa18.IsAcik;
                    break;
                    case "masa19":
                        label1.Text = tag;
                        masa19.AcKapat();
                    isAcik = masa19.IsAcik;
                    break;
                    case "masa20":
                        label1.Text = tag;
                        masa20.AcKapat();
                    isAcik = masa20.IsAcik;
                    break;
                    case "masa21":
                        label1.Text = tag;
                        masa21.AcKapat();
                        isAcik = masa21.IsAcik;
                    break;
            }
                if (isAcik == true)
                {
                pictureBox.Image = onmonitor;
                } else
                {
                pictureBox.Image = offmonitor;
                }



        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
