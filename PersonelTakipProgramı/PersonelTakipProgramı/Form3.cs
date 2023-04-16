using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;

namespace PersonelTakipProgramı
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        //veri tabanı dosya yolu ve  provider nesnesinin belirlenmesi
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
        Form1 form1 = new Form1(); // form1 class ından nesne aldım
        private void personelleri_goster() //SAYFA3 datagridwiev1 nesnesine elemanları atama METODU
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],dogumtarihi AS[DOĞUM TARİHİ],cinsiyet AS[CİNSİYETİ],mezuniyet AS[MEZUNİYET YILI],gorevi AS[GÖREVİ],gorevyeri AS[GÖREV YERİ],maasi AS[MAAŞI] from personeller Order By ad ASC", baglanti);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)// FORM ÖZELLİKLERİNİN BELİRLENMESİ
        {
            personelleri_goster();
            label19.Text = Form1.adi + " " + Form1.soyadi;
            try
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + Form1.tcno + ".jpeg");
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.png");
            }

        }

        private void button1_Click(object sender, EventArgs e) //ARAMA İŞLEMİNİN YAPILMASI
        {
            bool kayit_arama_durumu = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();

                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;

                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0).ToString() + ".jpeg");
                    }
                    catch
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.png");
                    }
                    label10.Text = kayitokuma.GetValue(1).ToString();
                    label11.Text = kayitokuma.GetValue(2).ToString(); 
                    label14.Text = kayitokuma.GetValue(3).ToString(); 
                    label13.Text = kayitokuma.GetValue(5).ToString(); 
                    if (kayitokuma.GetValue(4).ToString() == "Erkek")
                        label12.Text = "Erkek";
                    else
                        label12.Text = "Kadın";

                    label15.Text = kayitokuma.GetValue(6).ToString();
                    label16.Text = kayitokuma.GetValue(7).ToString();
                    label17.Text = kayitokuma.GetValue(8).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Aranan Kayıt Bulunamadı", "Eyüp DENİZ Personel Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneli TC Kimlik Numarası Giriniz!", "Eyüp DENİZ Personel Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // ÇIKIŞ YAPMA BUTONU AKTTİFLEŞTİRME
        {
            form1.cikisyap();
        }
    }
}
