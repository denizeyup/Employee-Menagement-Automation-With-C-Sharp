using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; // Kütüphane eklendi

namespace PersonelTakipProgramı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //veri tabanı dosya yolu ve  provider nesnesinin belirlenmesi
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");

        //fromlar arası veri aktarımlarında kullanılacak depişkenler
        public static string tcno, adi, soyadi, yetki;
       
        //yerel değişknelerin tanımlanması
        int hak = 3; bool durum = false;

        public void cikisyap() // ÇIKIŞ YAPMA METODU
        {
            DialogResult sonuc;
            sonuc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.No)
            {
                //herhangi bir işlem yok
            }
            if (sonuc == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e) //FORM ÖZELLİKLERİNİN BELİRLENMESİ
        {
            
            this.Text = "Kullanıcı Girişi";
            label5.Text = Convert.ToString(hak);                         
            radioButton1.Checked = true;
            AcceptButton = button1; CancelButton = button2;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow; // simge durumu tam ekran pasif hale getirdim
        }

        private void button2_Click(object sender, EventArgs e) // BUTTON2 E BASILDIĞINDA ÇIKIŞ YAPMA
        {
            cikisyap();         
        }

        private void button1_Click(object sender, EventArgs e) //KULLANICI GİRİŞ BUTONU
        {
            if (hak != 0)
            {
                baglanti.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglanti);
                selectsorgu.Connection = baglanti;
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();

                while (kayitokuma.Read()) // data readerde kayıt var ise
                {
                    if (radioButton1.Checked)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break;

                        }
                    }
                    if (radioButton2.Checked)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;

                        }
                    }
                }
                if (durum == false)
                    hak--;
                baglanti.Close();
            }
            label5.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı" + MessageBoxButtons.OK + MessageBoxIcon.Error);
                this.Close();
            }
        }

       
 

    


        
    }
}
