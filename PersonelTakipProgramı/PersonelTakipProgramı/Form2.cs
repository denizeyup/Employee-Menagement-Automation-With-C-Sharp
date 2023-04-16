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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        //veri tabanı dosya yolu ve  provider nesnesinin belirlenmesi
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");

        Form1 form1 = new Form1();

        /*********************** METODLAR***************************/
        private void kullanicilari_goster() // datagridwiev1 nesnesine elemanları atama form2 sayfa1
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter kullannicilari_listele =new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],yetki AS[YETKİ],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from kullanicilar Order By ad ASC",baglanti);
                DataSet dshafiza = new DataSet();
                kullannicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch(Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message,"Eyüp DENİZ Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                baglanti.Close();
            }
        }
        private void personelleri_goster() // datagridwiev2 nesnesine elemanları atama form2 sayfa 2
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],dogumtarihi AS[DOĞUM TARİHİ],cinsiyet AS[CİNSİYETİ],mezuniyet AS[MEZUNİYET YILI],gorevi AS[GÖREVİ],gorevyeri AS[GÖREV YERİ],maasi AS[MAAŞI] from personeller Order By ad ASC", baglanti);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }
        private void topPage1_temizle() // sayfa1 temizle metodu
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
        }
        private void topPage2_temizle() // sayfa2 temizle metodu
        {
            maskedTextBox1.Clear(); maskedTextBox2.Clear(); maskedTextBox3.Clear(); maskedTextBox4.Clear(); pictureBox2.Image = null;
            comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1; comboBox3.SelectedIndex = -1;
        }


        private void Form2_Load(object sender, EventArgs e) // KULLANICI VE PERSONEL İŞLEMLERİ FORM AYARLARI
        {
            pictureBox1.SizeMode=PictureBoxSizeMode.StretchImage;
            kullanicilari_goster();
            personelleri_goster();
            //kullanıcı işlemleri ayarları
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpeg");
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.png");
            }
            label11.Text = Form1.adi + " " + Form1.soyadi;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik Numarası 11 Karakter Olmak Zorunda!");
            radioButton1.Checked = true;
            textBox2.CharacterCasing=CharacterCasing.Upper;
            textBox3.CharacterCasing=CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;


            //personel işlemleri ayarları
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();
            radioButton3.Checked= true; 
        }

       
        /********************* KULLANICI İŞLEMLERİ METODLARI VE KODLARI ********************************/
        private void button1_Click(object sender, EventArgs e) // SAYFA1 ELEMAN EKLEME
        {
            string yetki = "";
            bool kayitkontrol = false;

            baglanti.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglanti);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }

            baglanti.Close();

            if (kayitkontrol == false)
            {
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                if (textBox2.Text.Length < 2 || textBox1.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                if (textBox3.Text.Length < 2 || textBox1.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                if (textBox4.Text.Length != 8 || textBox1.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                if (textBox5.Text == "" || textBox1.Text == "")
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                if (textBox6.Text == "" || textBox1.Text == "")
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;


                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 &&
                    textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox4.Text.Length > 1 &&
                    textBox5.Text != "" && textBox5.Text.Length > 1 && textBox6.Text != "" && textBox6.Text.Length > 1 &&
                    textBox5.Text == textBox6.Text)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";

                    try
                    {
                        baglanti.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values( '" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')",baglanti);
                        eklekomutu.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Yeni Kaydınız Oluşturuldu!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        kullanicilari_goster();
                        topPage1_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden Geçiriniz", "Eyüp DENİZ Personel Takip Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Aynı TC Kimlik Numarasına Ait Kayıt Bulunmaktadır!", "Eyüp DENİZ Personel Takip Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e) // SAYFA1 ELEMAN ARAMA
        {  
            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglanti);
                selectsorgu.Connection = baglanti;
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Aradığınız Kayıt Listede Bulunmamaktadır!", "Benzin İstasyonu Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneli Bir TC Kimlik No Giriniz!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }


        }

        private void button3_Click(object sender, EventArgs e) // SAYFA1 ELEMAN GÜNCELLEME
        {
            string yetki = "";
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                if (textBox2.Text.Length < 2 || textBox1.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                if (textBox3.Text.Length < 2 || textBox1.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                if (textBox4.Text.Length != 8 || textBox1.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                if (textBox5.Text == "" || textBox1.Text == "")
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                if (textBox6.Text == "" || textBox1.Text == "")
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;


                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 &&
                    textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox4.Text.Length > 1 &&
                    textBox5.Text != "" && textBox5.Text.Length > 1 && textBox6.Text != "" && textBox6.Text.Length > 1 &&
                    textBox5.Text == textBox6.Text)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";

                    try
                    {
                        baglanti.Open();
                        OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',yetki='" + yetki + "',kullaniciadi='" + textBox4.Text + "',parola='" + textBox5.Text + "' where tcno='" + textBox1.Text + "'", baglanti);
                        guncellekomutu.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kullanıcı Bilgileri Güncellendi!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_goster();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden Geçiriniz", "Eyüp DENİZ Personel Takip Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            
           
        }

        private void button4_Click(object sender, EventArgs e) // SAYFA1 ELEMAN SİLME
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglanti.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglanti);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete * from kullanicilar where tcno='" + textBox1.Text + "'", baglanti);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kaydınız Silindi!","Eyüp DENİZ Personel Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    kullanicilari_goster();
                    topPage1_temizle();
                    break;
                }
                if(kayit_arama_durumu==false)
                    MessageBox.Show("Silinecek Kayıt Bulunamadı!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
                topPage1_temizle();
            }
            else
            {
                MessageBox.Show("Lüthen 11 Karakterden Oluşan Bir TC Kimlik Numarası Giriniz!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                topPage1_temizle();
            }
        }

        private void button5_Click(object sender, EventArgs e) // SAYFA1 SAYFA TEMİZLEME
        {
            topPage1_temizle();
        }


        /********************* PERSONEL İŞLEMLERİ METODLARI VE KODLARI ********************************/
        private void button6_Click(object sender, EventArgs e) //SAYFA2 RESİM EKLEME
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Personel Resmi Seçiniz";
            resimsec.Filter = "JPEG Dosyalar (*.jpeg) | *.jpeg";
            if(resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());
            }
        }

        private void button8_Click(object sender, EventArgs e) //SAYFA2 ELEMAN EKLEME
        {
            maskedTextBox3.Text.ToUpper();
            maskedTextBox2.Text.ToUpper();

            string cinsiyet = "";
            bool kayitkontrol = false;

            baglanti.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text+ "'",baglanti);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();

            while (kayitokuma.Read())
            {
                kayitkontrol=true;
                break;
            }
            baglanti.Close();
            if (kayitkontrol == false)
            {
                if (pictureBox2.Image == null)
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;
                if (maskedTextBox1.MaskCompleted==false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;
                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;
                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;
                if (comboBox1.Text== "")
                    label17.ForeColor = Color.Red;  
                else
                    label17.ForeColor = Color.Black;
                if (comboBox2.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;
                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (Convert.ToInt32(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image!=null && maskedTextBox1.MaskCompleted!=false && maskedTextBox2.MaskCompleted != false&&
                    maskedTextBox3.MaskCompleted != false&& comboBox1.Text!=""&& comboBox2.Text != "" &&
                    comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton3.Checked == true)
                        cinsiyet = "Erkek";
                    else
                        cinsiyet = "Kadın";
                    try     
                    {
                        baglanti.Open();

                        OleDbCommand komutekle = new OleDbCommand("insert into personeller values('" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + dateTimePicker1.Text + "','" + cinsiyet + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + maskedTextBox4.Text + "')", baglanti);
                        komutekle.ExecuteNonQuery();
                        baglanti.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler\\"))
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler\\");
                            pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\"+maskedTextBox1.Text+".jpeg");
                        MessageBox.Show("Yeni Personel Kaydı Oluşturuldu!","Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        baglanti.Close();
                        personelleri_goster();
                        topPage2_temizle();


                    }
                    catch(Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Alanları Tekrar Gözden Geçiriniz!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarısı Daha Önceden Kayıtlıdır!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button7_Click(object sender, EventArgs e)//SAYFA2 ELEMAN ARAMA-
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
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0).ToString() + ".jpeg");
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.png");


                    }
                    maskedTextBox2.Text = kayitokuma.GetValue(1).ToString();
                    maskedTextBox3.Text = kayitokuma.GetValue(2).ToString();
                    dateTimePicker1.Text = kayitokuma.GetValue(3).ToString();
                    if (kayitokuma.GetValue(4).ToString() == "Erkek")
                        radioButton3.Checked = true;
                    else
                        radioButton4.Checked = true;
                    comboBox1.Text = kayitokuma.GetValue(5).ToString();
                    comboBox2.Text = kayitokuma.GetValue(6).ToString();
                    comboBox3.Text = kayitokuma.GetValue(7).ToString();
                    maskedTextBox4.Text = kayitokuma.GetValue(8).ToString();
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

        private void button9_Click(object sender, EventArgs e) //SAYFA2 ELEMAN GÜNCELLEME
        {
            string cinsiyet = "";

                if (pictureBox2.Image == null)
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;
                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;
                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;
                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;
                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;
                if (comboBox2.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;
                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (Convert.ToInt32(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false &&
                    maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" &&
                    comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton3.Checked == true)
                        cinsiyet = "Erkek";
                    else
                        cinsiyet = "Kadın";
                    try
                    {
                        baglanti.Open();

                        OleDbCommand guncellekomut = new OleDbCommand("update personeller set ad='" + maskedTextBox2.Text + "',soyad='" + maskedTextBox3.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',cinsiyet='" + cinsiyet + "',mezuniyet='" + comboBox1.Text + "',gorevi='" + comboBox2.Text + "',gorevyeri='" + comboBox3.Text + "',maasi='" + maskedTextBox4.Text + "' where tcno='" + maskedTextBox1.Text + "' ", baglanti);
                        guncellekomut.ExecuteNonQuery();
                        baglanti.Close();

                        personelleri_goster();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Alanları Tekrar Gözden Geçiriniz!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            
        }

        private void button10_Click(object sender, EventArgs e)//SAYFA2 ELEMAN SİLME
        {
            if (maskedTextBox1.MaskCompleted==true)
            {
                bool kayit_arama_durumu = false;
                baglanti.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete  from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kaydınız Silindi!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    personelleri_goster();
                    topPage2_temizle();
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Silinecek Kayıt Bulunamadı!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
                topPage2_temizle();
            }
            else
            {
                MessageBox.Show("Lüthen 11 Karakterden Oluşan Bir TC Kimlik Numarası Giriniz!", "Eyüp DENİZ Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                topPage2_temizle();
            }
        }

        private void button11_Click(object sender, EventArgs e)// SAYFA2 TEMİZLEME
        {
            topPage2_temizle();
        }

        private void button12_Click(object sender, EventArgs e)//SAYFA2 DEN ÇIKIŞ YAPMA
        {
            form1.cikisyap();
        }
    }
}
