using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ketnetmanager
{
    public partial class Form1 : Form
    {
            public static Dictionary<string, double> fiyatTarifeleri = new Dictionary<string, double>()
                        {

                            { "Normal Fiyat", 33.0 },
                            { "Orta Fiyat", 50.0 },
                            { "V.I.P Fiyat", 90.0 }
                        };

            readonly Masalar masa1 = new Masalar("masa1", fiyatTarifeleri);
            readonly Masalar masa2 = new Masalar("masa2", fiyatTarifeleri);
            readonly Masalar masa3 = new Masalar("masa3", fiyatTarifeleri);
            readonly Masalar masa4 = new Masalar("masa4", fiyatTarifeleri);
            readonly Masalar masa5 = new Masalar("masa5", fiyatTarifeleri);
            readonly Masalar masa6 = new Masalar("masa6", fiyatTarifeleri);
            readonly Masalar masa7 = new Masalar("masa7", fiyatTarifeleri);
            readonly Masalar masa8 = new Masalar("masa8", fiyatTarifeleri);
            readonly Masalar masa9 = new Masalar("masa9", fiyatTarifeleri);
            readonly Masalar masa10 = new Masalar("masa10", fiyatTarifeleri);
            readonly Masalar masa11 = new Masalar("masa11", fiyatTarifeleri);
            readonly Masalar masa12 = new Masalar("masa12", fiyatTarifeleri);
            readonly Masalar masa13 = new Masalar("masa13", fiyatTarifeleri);
            readonly Masalar masa14 = new Masalar("masa14", fiyatTarifeleri);
            readonly Masalar masa15 = new Masalar("masa15", fiyatTarifeleri);
            readonly Masalar masa16 = new Masalar("masa16", fiyatTarifeleri);
            readonly Masalar masa17 = new Masalar("masa17", fiyatTarifeleri);
            readonly Masalar masa18 = new Masalar("masa18", fiyatTarifeleri);
            readonly Masalar masa19 = new Masalar("masa19", fiyatTarifeleri);
            readonly Masalar masa20 = new Masalar("masa20", fiyatTarifeleri);
            readonly Masalar masa21 = new Masalar("masa21", fiyatTarifeleri);
            readonly Masalar masa22 = new Masalar("masa22", fiyatTarifeleri);
            readonly Masalar masa23 = new Masalar("masa23", fiyatTarifeleri);
            readonly Masalar masa24 = new Masalar("masa24", fiyatTarifeleri);

            //sql linkini resources kısmında tutuyoruz
            readonly string sqlbaglantisi = Properties.Resources.sqllink;
            readonly Image offmonitor = ketnetmanager.Resource1.offmonitor;
            readonly Image onmonitor = ketnetmanager.Resource1.onmonitor;
            readonly string myDosya = Resource1.masalogs;
            
            
            //statler için değişkenler
            public double kazanc;
            public bool isAcik = false;
            public bool isLocked = false;
            public int normalCount = 0;
            public int ortCount = 0;
            public int vipCount = 0;
            public int acikMasa = 0;
            

        public Form1()
        {
            InitializeComponent();

            comboBox2.DataSource = new BindingSource(fiyatTarifeleri, null);
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Value";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FiyatTarifeleriGuncelle();
            masalarUpdate();
            kafeteryaGuncelle();

        }

        private void masalarUpdate() // serverdan bilgisayar durumunu öğren ve program başlarken işler.
        {
            List<PictureBox> pictureBoxes = panel5.Controls.OfType<PictureBox>().ToList();

            foreach (PictureBox pictureBox in pictureBoxes) //segmenti öğrenir
            {
                Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

                switch (seciliMasa.getMasaData("masaDurum", "masaSegment"))
                {
                    case 0:
                        seciliMasa.setUcret(fiyatTarifeleri["Normal Fiyat"], 0);
                        pictureBox.BackgroundImage = null;
                        normalCount++;
                        break;
                    case 1:
                        seciliMasa.setUcret(fiyatTarifeleri["Orta Fiyat"], 1);
                        pictureBox.BackgroundImage = Resource1.ortasegmentbgimg;
                        ortCount++;
                        break;
                    case 2:
                        seciliMasa.setUcret(fiyatTarifeleri["V.I.P Fiyat"], 2);
                        pictureBox.BackgroundImage = Resource1.vipmasabgimg;
                        vipCount++;
                        break;
                }
            }

            foreach (PictureBox pictureBox in pictureBoxes) //on or off
            {
                Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());
                if (seciliMasa.getMasaData("masaDurum", "masaDurum") == 1)
                {
                    pictureBox.Image = Resource1.onmonitor;
                    acikMasa++;
                    seciliMasa.AcKapat();
                }
                else
                {
                    pictureBox.Image = Resource1.offmonitor;
                }
            }

        }

        private void FiyatTarifeleriGuncelle() // serverdaki tarife fiyatlarını günceller
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(sqlbaglantisi))
                {
                    baglanti.Open();

                    string query = "SELECT TOP 1 normal, orta, vip FROM fiyattarifeler";
                    using (SqlCommand command = new SqlCommand(query, baglanti))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double normalFiyat = reader.GetDouble(reader.GetOrdinal("normal"));
                                double ortaFiyat = reader.GetDouble(reader.GetOrdinal("orta"));
                                double vipFiyat = reader.GetDouble(reader.GetOrdinal("vip"));

                                fiyatTarifeleri["Normal Fiyat"] = normalFiyat;
                                fiyatTarifeleri["Orta Fiyat"] = ortaFiyat;
                                fiyatTarifeleri["V.I.P Fiyat"] = vipFiyat;
                            }
                            else
                            {
                                Console.WriteLine("No data found in the table.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata." + ex.Message);
            }
        }



        private void masaBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void açKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolstripin aç/kapat methodu. nesne dönüşümleriyle seçili pictureboxun tag etiketiyle nesneye ulaşır.
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)clickedItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            label1.Text = pictureBox.Tag.ToString();
            seciliMasa.AcKapat();
            isAcik = seciliMasa.IsAcik;
            KazancEkle(seciliMasa.ToplamBorc);
            label27.Text = seciliMasa.saatlikUcret.ToString(); 

            if (isAcik == true)
            {
                pictureBox.Image = Resource1.onmonitor;
                acikMasa++;
            } else
            {
                pictureBox.Image = Resource1.offmonitor;
                acikMasa--;
            }

        }

        private void KazancEkle(double gelenPara)
        {
            this.kazanc += Math.Round(gelenPara,2);
        }

        private void SayfaDegistir(Panel acilacakPanel) //sayfa değişimini yönetir. Methoda gelen panel açılacak panel.
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel && panel != acilacakPanel && panel.Tag != null && panel.Tag.ToString() == "mainpanel")
                {
                    panel.Visible = false;
                }
            }
            acilacakPanel.Visible = true;
        }

        private Object myMasa(string tag) //tag değerinden hangi nesne olduğunu bulur.
        {

            switch (tag)
            {
                case "masa1": return masa1;
                case "masa2": return masa2;
                case "masa3": return masa3;
                case "masa4": return masa4;
                case "masa5": return masa5;
                case "masa6": return masa6;
                case "masa7": return masa7;
                case "masa8": return masa8;
                case "masa9": return masa9;
                case "masa10": return masa10;
                case "masa11": return masa11;
                case "masa12": return masa12;
                case "masa13": return masa13;
                case "masa14": return masa14;
                case "masa15": return masa15;
                case "masa16": return masa16;
                case "masa17": return masa17;
                case "masa18": return masa18;
                case "masa19": return masa19;
                case "masa20": return masa20;
                case "masa21": return masa21;
                case "masa22": return masa22;
                case "masa23": return masa23;
                case "masa24": return masa24;
                default: return null;
            }
        }


        private void LogGoster(string tarih1) //combobox indexine göre logları işlenecek tabloyu seçer. 
        {                                     // methoda gelen tarih değerinden itibaren gösterir. null gelirse hepsini gösterir.
            string selectedList = "LogDefteri";
            switch(comboBox3.SelectedIndex)
            {
                case 0:
                    selectedList = "LogDefteri";
                    break;
                case 1:
                    selectedList = "adminLogs";
                    break;
                case 2:
                    selectedList = "satisLogs";
                    break;
            }

            string bugun = DateTime.Today.ToString("yyyy-MM-dd");

            using (SqlConnection baglanti = new SqlConnection(sqlbaglantisi))
            {
                string komut;
                if (string.IsNullOrEmpty(tarih1)) //tarih1 null ise bugüne kadarki hepsini göster
                {
                    komut = "SELECT * FROM "+ selectedList +" WHERE Tarih <= @bugun";
                }
                else //değilse aralıktakileri göster
                {
                    komut = "SELECT * FROM " + selectedList + " WHERE Tarih BETWEEN @tarih1 AND @bugun";
                }

                SqlCommand sqlCommand = new SqlCommand(komut, baglanti);
                sqlCommand.Parameters.AddWithValue("@bugun", bugun); //bugünün tarihini al

                if (!string.IsNullOrEmpty(tarih1)) // Tarih1 null değilse ekle.
                {
                    sqlCommand.Parameters.AddWithValue("@tarih1", tarih1); 
                }

                //dataadatper nesnesiyle oluşturduğumuz tabloyu datagridview1 gönderir.
                SqlDataAdapter myLogs = new SqlDataAdapter(sqlCommand);
                DataTable table = new DataTable();
                myLogs.Fill(table);

                dataGridView1.DataSource = table;
            }

        }


        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(46, 52, 72);

            button7.BackColor = Color.FromArgb(21, 31, 52);
            button10.BackColor = Color.FromArgb(21, 31, 52);
            button6.BackColor = Color.FromArgb(21, 31, 52);

            pictureBox29.Visible = true;

            pictureBox1.Visible = false;
            pictureBox27.Visible = false;
            pictureBox30.Visible = false;

            LogGuncelle();
            SayfaDegistir(panel1);



        } 

        private void LogGuncelle()
        {
            LogGoster(null);
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 5;
            label25.Text = fiyatTarifeleri[comboBox2.Text].ToString()+ "₺";

            //datagridview1 görsel değişiklikler
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(37, 42, 64);
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Verdana", 10);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* if (double.TryParse(textBox1.Text, out saatlikUcret))
            {
                saatlikUcret = Convert.ToDouble(saatlikUcret);
                label25.Text = textBox1.Text;
                textBox1.Text = "";
            } else
            {
                MessageBox.Show("Lütfen geçerli bir ücret değeri girin.");
            } */
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            kafeteryaGuncelle();
            SayfaDegistir(panel3);
        }
        private void textBox1_TextChanged_2(object sender, EventArgs e) //textboxtaki texte göre kafeterya eşyalarını getir.
        {
            try
            {
                if (dataGridView2.DataSource != null && dataGridView2.Rows.Count > 0)
                {
                    string searchText = textBox1.Text;
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"urunIsim LIKE '{searchText}%'";
                }
                else
                {
                    MessageBox.Show("Ürün bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Ürün bulunamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün bulunamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void kafeteryaGuncelle() //serverdan kafeterya datasının son halini al. 
        {
            using (SqlConnection baglanti = new SqlConnection(sqlbaglantisi))
            {
                string komut = "SELECT id,urunImg,urunIsim,urunAciklama, urunFiyat FROM kafeterya";

                SqlCommand sqlCommand = new SqlCommand(komut, baglanti);

                SqlDataAdapter myItems = new SqlDataAdapter(sqlCommand);
                DataTable table = new DataTable();
                myItems.Fill(table);
                dataGridView2.DataSource = table;

            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //görsel ayarlamalar
            dataGridView2.RowTemplate.Height = 70;
            dataGridView2.Columns["urunIsim"].HeaderText = "Ürün İsmi";
            dataGridView2.Columns["urunFiyat"].HeaderText = "Fiyat";
            dataGridView2.Columns["urunAciklama"].HeaderText = "Açıklama";
            dataGridView2.Columns["urunImg"].HeaderText = "Ürün Resmi";
            dataGridView2.Columns["id"].HeaderText = "ID";
            dataGridView2.Columns["urunImg"].Width = 70;
            dataGridView2.Columns["urunFiyat"].Width = 60;
            dataGridView2.Columns["urunIsim"].Width = 120;
            dataGridView2.Columns["id"].Width = 30;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(37, 42, 64);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AllowUserToOrderColumns = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.RowHeadersVisible = false;
        }


        private void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.FromArgb(46, 52, 72);
            button10.BackColor = Color.FromArgb(21, 31, 52);
            button1.BackColor = Color.FromArgb(21, 31, 52);
            button6.BackColor = Color.FromArgb(21, 31, 52);

            pictureBox1.Visible = true;
            pictureBox27.Visible = false;
            pictureBox29.Visible = false;
            pictureBox30.Visible = false;

            SayfaDegistir(panel2);
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
            LogGoster(null);
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //secili indexe göre LogGoster() methoduna gidecek değeri bul.
            DateTime bugun = DateTime.Today;
            DateTime baslangicTarih;

            switch (comboBox1.SelectedIndex)
            {
                case 0: // Bugun
                    baslangicTarih = bugun;
                    break;
                case 1: // Son 7 Gün
                    baslangicTarih = bugun.AddDays(-7);
                    break;
                case 2: // Son 30 gün
                    baslangicTarih = bugun.AddDays(-30);
                    break;
                case 3: // Son 90 gün
                    baslangicTarih = bugun.AddDays(-90);
                    break;
                case 4: // Son 360 gün
                    baslangicTarih = bugun.AddDays(-360);
                    break;
                default:
                    LogGoster(null);
                    return;
            }

            string myTarih = baslangicTarih.ToString("yyyy-MM-dd");
            LogGoster(myTarih);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            double tarifegirdi;
            bool isTrue = false;
            string degisecektarife = comboBox2.Text;

            while (!isTrue)
            {
                string kullaniciGirdisi = Interaction.InputBox("Fiyat tarifesinin yeni fiyatını giriniz:", "");

                if (double.TryParse(kullaniciGirdisi, out tarifegirdi))
                {
                    isTrue = true;
                    MessageBox.Show(degisecektarife + " Fiyat tarifesi " + tarifegirdi+ "₺ olarak güncellendi.");
                    fiyatTarifeleri[degisecektarife] = tarifegirdi;
                    label25.Text = fiyatTarifeleri[degisecektarife].ToString() + "₺";
                    int mysegment = Array.IndexOf(fiyatTarifeleri.Values.ToArray(), degisecektarife);

                    List <PictureBox> pictureBoxes = panel5.Controls.OfType<PictureBox>().ToList();
                    foreach (PictureBox pictureBox in pictureBoxes)
                    {
                        
                        Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

                        switch(seciliMasa.segment)
                        {
                            case 0:
                                seciliMasa.setUcret(fiyatTarifeleri["Normal Fiyat"], seciliMasa.segment);
                                break;

                            case 1:
                                seciliMasa.setUcret(fiyatTarifeleri["Orta Fiyat"], seciliMasa.segment);
                                break;

                            case 2:
                                seciliMasa.setUcret(fiyatTarifeleri["V.I.P Fiyat"], seciliMasa.segment);
                                break;
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Geçersiz değer! Lütfen geçerli bir değer giriniz.");
                }
            }


            /* try
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
            } */
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.ShowDialog();
            kafeteryaGuncelle();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label25.Text = fiyatTarifeleri[comboBox2.Text].ToString() + "₺";
        }


        //tarife değişiklikleri

        //tarifesi değişecek masayı nesne dönüşümleriyle bul ve değiştir.
        private void normalTarifeToolStripMenuItem_Click(object sender, EventArgs e)
        {


            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)clickedItem.Owner;
            ContextMenuStrip menu = (ContextMenuStrip)dropDownMenu.OwnerItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            switch (seciliMasa.getMasaData("masaDurum", "masaSegment"))
            {
                case 0:
                    normalCount--;
                    break;
                case 1:
                    ortCount--;
                    break;
                case 2:
                    vipCount--;
                    break;
            }

            seciliMasa.setUcret(fiyatTarifeleri["Normal Fiyat"],0);
            pictureBox.BackgroundImage =null;
            normalCount++;
        }

        private void ortaSegmenTarifeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)clickedItem.Owner;
            ContextMenuStrip menu = (ContextMenuStrip)dropDownMenu.OwnerItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            switch (seciliMasa.getMasaData("masaDurum", "masaSegment"))
            {
                case 0:
                    normalCount--;
                    break;
                case 1:
                    ortCount--;
                    break;
                case 2:
                    vipCount--;
                    break;
            }


            seciliMasa.setUcret(fiyatTarifeleri["Orta Fiyat"],1);
            pictureBox.BackgroundImage = Resource1.ortasegmentbgimg;
            ortCount++;
        }

        private void vIPTarifeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)clickedItem.Owner;
            ContextMenuStrip menu = (ContextMenuStrip)dropDownMenu.OwnerItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            switch (seciliMasa.getMasaData("masaDurum", "masaSegment"))
            {
                case 0:
                    normalCount--;
                    break;
                case 1:
                    ortCount--;
                    break;
                case 2:
                    vipCount--;
                    break;
            }

            seciliMasa.setUcret(fiyatTarifeleri["V.I.P Fiyat"],2);
            pictureBox.BackgroundImage = Resource1.vipmasabgimg;
            vipCount++;
        }



        //seçili ürünü ürün kartına getir.
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null && dataGridView2.CurrentRow.Cells["urunImg"] != null)
            {
                if (dataGridView2.CurrentRow.Cells["urunImg"].Value is byte[] resimVerisi && resimVerisi.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(resimVerisi))
                    {
                        pictureBox25.Image = Image.FromStream(ms);
                    }
                }
            }
            label30.Text = dataGridView2.CurrentRow.Cells["urunIsim"].Value.ToString();
            label31.Text = dataGridView2.CurrentRow.Cells["urunAciklama"].Value.ToString();
            label32.Text = dataGridView2.CurrentRow.Cells["urunFiyat"].Value.ToString();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogGoster(null);
            comboBox1.SelectedIndex = 5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SayfaDegistir(panel7);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4(Convert.ToInt32(dataGridView2.CurrentRow.Cells["id"].Value));
            form.ShowDialog();
            kafeteryaGuncelle();
        }

        private void button9_Click_1(object sender, EventArgs e) //ürün silme butonu
        {
            if (dataGridView2.SelectedRows.Count > 0) 
            {
                DialogResult result = MessageBox.Show("Seçili ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int selectedRowIndex = dataGridView2.SelectedRows[0].Index;

                    using (SqlConnection connection = new SqlConnection(sqlbaglantisi)) 
                    {
                        connection.Open();

                        int idToDelete = Convert.ToInt32(dataGridView2.Rows[selectedRowIndex].Cells["id"].Value); 

                        string deleteQuery = "DELETE FROM kafeterya WHERE id = @id"; 
                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@id", idToDelete);
                            command.ExecuteNonQuery();
                        }
                    }
                    dataGridView2.Rows.RemoveAt(selectedRowIndex);

                    MessageBox.Show("Ürün sistemden başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_Sorted(object sender, EventArgs e)
        {
        
        }

        private void ürünEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //seçili masayı bulur. UrunSat() methodunu çalıştırtırır.
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)clickedItem.Owner;
            ContextMenuStrip menu = (ContextMenuStrip)dropDownMenu.OwnerItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            seciliMasa.UrunSat();
        }

        private void ürünSilToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Form5 form = new Form5("");
            form.ShowDialog();
            KazancEkle(form.totalFiyat);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //form kapanırken verilecek uyarı
            if (e.CloseReason == CloseReason.UserClosing)
            {
                
                DialogResult result = MessageBox.Show("Formu kapatmak istediğinize emin misiniz? Arkaplanda devam eden işlemler olabilir. Giriş ekranına geri döneceksiniz.", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {   
                }
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            
            }


        private void pictureBox28_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            //açık bilgisayarları kapat.

            //panel5 içerisindeki bütün pictureboxlarla bir nesne oluşturur. foreach ile içerisinde gezerek açık bilgisayarları bulur.
            List<PictureBox> pictureBoxes = panel5.Controls.OfType<PictureBox>().ToList();
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());
                if (seciliMasa.IsAcik == true)
                {
                    seciliMasa.AcKapat();
                    pictureBox.Image = Resource1.offmonitor;
                }
            }
            MessageBox.Show("Açık bilgisayarlar başarıyla kapatıldı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            button10.BackColor = Color.FromArgb(46, 52, 72);

            button7.BackColor = Color.FromArgb(21, 31, 52);
            button1.BackColor = Color.FromArgb(21, 31, 52);
            button6.BackColor = Color.FromArgb(21, 31, 52);

            pictureBox27.Visible = true;

            pictureBox1.Visible = false;
            pictureBox29.Visible = false;
            pictureBox30.Visible = false;

            kafeteryaGuncelle();
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            SayfaDegistir(panel3);


        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            button6.BackColor = Color.FromArgb(46, 52, 72);

            button7.BackColor = Color.FromArgb(21, 31, 52);
            button10.BackColor = Color.FromArgb(21, 31, 52);
            button1.BackColor = Color.FromArgb(21, 31, 52);

            pictureBox30.Visible = true;
            pictureBox1.Visible = false;
            pictureBox27.Visible = false;
            pictureBox29.Visible = false;

            comboBox4.SelectedIndex = 1;
            SetKazancTablosu(30);
            SetSegmentTablosu();
            setGelirTablosu();

            SayfaDegistir(panel7);


        }


        //kazançtablosunu girilen güne göre ayarlar
        private void SetKazancTablosu(int gun)
        {

            chart1.Series["Kazanç"].Points.Clear();

            for (int i = 0;i >-gun;i--)
            {
                string bugün = DateTime.Today.AddDays(i).ToString("yyyy-MM-dd");
                chart1.Series["Kazanç"].Points.AddXY(bugün, KazancHesaplaStat("ToplamBorc", "LogDefteri", bugün) + KazancHesaplaStat("kazanc", "satisLogs",bugün));
            }


        }

        private void SetSegmentTablosu()
        {
            chart2.Series["MasaDagilimi"].Points.Clear();

            chart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White; // X ekseni etiketlerini beyaza çevir
            chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White; // Y ekseni etiketlerini beyaza çevir

            chart2.Series["MasaDagilimi"].Points.AddXY("Normal", normalCount);
            chart2.Series["MasaDagilimi"].Points.AddXY("Orta", ortCount);
            chart2.Series["MasaDagilimi"].Points.AddXY("V.I.P.", vipCount);
        }

        private void setGelirTablosu()
        {
            label53.Text = KazancHesaplaStat("ToplamBorc", "LogDefteri").ToString() + "₺";
            label50.Text = KazancHesaplaStat("kazanc", "satisLogs").ToString() + "₺";
            label54.Text = kazanc.ToString();
            label56.Text = (KazancHesaplaStat("ToplamBorc", "LogDefteri") + KazancHesaplaStat("kazanc", "satisLogs")).ToString() + "₺";

            chart3.Series["Gelir Dağılımı"].Points.Clear();

            chart3.Series["Gelir Dağılımı"].Points.AddXY("Bilgisayar", KazancHesaplaStat("ToplamBorc", "LogDefteri"));
            chart3.Series["Gelir Dağılımı"].Points.AddXY("Kafeterya", KazancHesaplaStat("kazanc", "satisLogs"));
        }



        //girilen tarihteki toplam kazancı hesaplar 
        private double KazancHesaplaStat(string sutun,string tablo,string tarih)
        {
                double toplamBorcSum = 0;

                try
                {
                    using (SqlConnection connection = new SqlConnection(sqlbaglantisi))
                    {
                        connection.Open();

                        string query = $"SELECT SUM([{sutun}]) FROM {tablo} WHERE Tarih = @tarih";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@tarih", tarih);

                            object result = command.ExecuteScalar();

                            if (result != DBNull.Value)
                            {
                                toplamBorcSum = Convert.ToDouble(result);
                            }
                    }
                }
                }
                catch (Exception ex)
                {
                MessageBox.Show("Kazanç hesaplarken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return toplamBorcSum;
        }

        // tarih girilmediğinde bütün dataların kazancını hesaplayacak şekilde override ettim
        private double KazancHesaplaStat(string sutun, string tablo)
        {
            double toplamBorcSum = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlbaglantisi))
                {
                    connection.Open();

                    string query = $"SELECT SUM([{sutun}]) FROM {tablo}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            toplamBorcSum = Convert.ToDouble(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kazanç hesaplarken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return toplamBorcSum;
        }


        private void button13_Click(object sender, EventArgs e)
        {
            masalarUpdate();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox28_Click_1(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {


        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/efeketket/ketnetmanager");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            switch(isLocked) //kilitleme switchi
            {
                case true:
                    foreach (Control control in this.Controls)
                    {
                        if (control.Tag != "locker")
                        {
                            control.Enabled = true;
                        }
                    }
                    this.isLocked = false;
                    break;

                case false:
                    foreach (Control control in this.Controls)
                    {
                        if (control.Tag != "locker")
                        {
                            control.Enabled = false;
                        }
                    }
                    this.isLocked = true;
                    break;
            }
                

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    SetKazancTablosu(7);
                    break;

                case 1:
                    SetKazancTablosu(30);
                    break;

                case 2:
                    SetKazancTablosu(90);
                    break;
                case 3:
                    SetKazancTablosu(360);
                    break;
            }
        }
    }
}
