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
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ketnetmanager
{
    public partial class Form1 : Form
    {
            public static Dictionary<string, double> fiyatTarifeleri = new Dictionary<string, double>()
                        {
                            { "Normal Fiyat", 10.0 },
                            { "Orta Fiyat", 20.0 },
                            { "V.I.P Fiyat", 30.0 }
                        };
            readonly Masalar masa1 = new Masalar("masa1", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa2 = new Masalar("masa2", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa3 = new Masalar("masa3", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa4 = new Masalar("masa4", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa5 = new Masalar("masa5", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa6 = new Masalar("masa6", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa7 = new Masalar("masa7", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa8 = new Masalar("masa8", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa9 = new Masalar("masa9", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa10 = new Masalar("masa10", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa11 = new Masalar("masa11", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa12 = new Masalar("masa12", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa13 = new Masalar("masa13", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa14 = new Masalar("masa14", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa15 = new Masalar("masa15", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa16 = new Masalar("masa16", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa17 = new Masalar("masa17", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa18 = new Masalar("masa18", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa19 = new Masalar("masa19", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa20 = new Masalar("masa20", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa21 = new Masalar("masa21", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa22 = new Masalar("masa22", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa23 = new Masalar("masa23", fiyatTarifeleri["Normal Fiyat"]);
            readonly Masalar masa24 = new Masalar("masa24", fiyatTarifeleri["Normal Fiyat"]);

            readonly string sqlbaglantisi = Properties.Resources.sqllink;
            readonly Image offmonitor = ketnetmanager.Resource1.offmonitor;
            readonly Image onmonitor = ketnetmanager.Resource1.onmonitor;
            readonly string myDosya = Resource1.masalogs;
            
            public double kazanc;


            public bool isAcik = false;


        public Form1()
        {
            InitializeComponent();

            pictureBox1.Tag = masa1;


            comboBox2.DataSource = new BindingSource(fiyatTarifeleri, null);
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Value";

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
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)clickedItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            label1.Text = pictureBox.Tag.ToString();
            seciliMasa.AcKapat();
            isAcik = seciliMasa.IsAcik;
            KazancEkle(seciliMasa.ToplamBorc);
            label27.Text = kazanc.ToString();

            if (isAcik == true)
            {
                pictureBox.Image = Resource1.onmonitor;
            } else
            {
                pictureBox.Image = Resource1.offmonitor;
            }

        }

        private void KazancEkle(double gelenPara)
        {
            this.kazanc += Math.Round(gelenPara,2);
        }

        private void SayfaDegistir(Panel acilacakPanel)
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
        private Object myMasa(string tag)
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


        private void LogGoster(string tarih1)
        {
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            LogGoster(null);
            SayfaDegistir(panel1);

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 5;
            label25.Text = fiyatTarifeleri[comboBox2.Text].ToString();

            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.GridColor = Color.Gray;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
        private void textBox1_TextChanged_2(object sender, EventArgs e)
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

        private void kafeteryaGuncelle()
        {


            using (SqlConnection baglanti = new SqlConnection(sqlbaglantisi))
            {
                string komut = "SELECT urunIsim, urunFiyat, urunAciklama, urunImg,id FROM kafeterya";

                SqlCommand sqlCommand = new SqlCommand(komut, baglanti);

                SqlDataAdapter myItems = new SqlDataAdapter(sqlCommand);
                DataTable table = new DataTable();
                myItems.Fill(table);
                dataGridView2.DataSource = table;
            }
            dataGridView2.Columns["urunIsim"].HeaderText = "Ürün İsmi";
            dataGridView2.Columns["urunFiyat"].HeaderText = "Fiyat";
            dataGridView2.Columns["urunAciklama"].HeaderText = "Açıklama";

            dataGridView2.Columns["id"].HeaderText = "ID";
            dataGridView2.AllowUserToOrderColumns = false;

            dataGridView2.Columns["urunFiyat"].Width = 40;
            dataGridView2.Columns["id"].Width = 25;

            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.GridColor = Color.Gray;
            dataGridView2.AllowUserToOrderColumns = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.RowHeadersVisible = false;


        }


        private void button7_Click(object sender, EventArgs e)
        {
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

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
                    label25.Text = fiyatTarifeleri[degisecektarife].ToString();
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
            label25.Text = fiyatTarifeleri[comboBox2.Text].ToString();
        }

        private void normalTarifeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)clickedItem.Owner;
            ContextMenuStrip menu = (ContextMenuStrip)dropDownMenu.OwnerItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            seciliMasa.setUcret(fiyatTarifeleri["Normal Fiyat"]);
        }

        private void ortaSegmenTarifeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)clickedItem.Owner;
            ContextMenuStrip menu = (ContextMenuStrip)dropDownMenu.OwnerItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            seciliMasa.setUcret(fiyatTarifeleri["Orta Fiyat"]);
        }

        private void vIPTarifeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)clickedItem.Owner;
            ContextMenuStrip menu = (ContextMenuStrip)dropDownMenu.OwnerItem.Owner;
            PictureBox pictureBox = (PictureBox)menu.SourceControl;

            Masalar seciliMasa = (Masalar)myMasa(pictureBox.Tag.ToString());

            seciliMasa.setUcret(fiyatTarifeleri["V.I.P Fiyat"]);

        }

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

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0) 
            {
                DialogResult result = MessageBox.Show("Seçili satırı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int selectedRowIndex = dataGridView2.SelectedRows[0].Index;

                    using (SqlConnection connection = new SqlConnection(sqlbaglantisi)) 
                    {
                        connection.Open();

                        int idToDelete = Convert.ToInt32(dataGridView2.Rows[selectedRowIndex].Cells["id"].Value); 

                        string deleteQuery = "DELETE FROM adminLogs WHERE id = @id"; 
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
    }
}
