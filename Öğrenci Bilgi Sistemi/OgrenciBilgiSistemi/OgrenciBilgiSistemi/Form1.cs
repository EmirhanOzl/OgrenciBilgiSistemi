using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OgrenciBilgiSistemi
{
       
    public partial class Form1 : Form
    {

        Baglan con = new Baglan();

        string text = "T.C. / Kullanıcı Adı";
        string text1 = "Şifreniz";
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = text;              //Bu kısımda kullanıcı adı ve şifresini aldım.
            textBox2.Text = text1;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == text)
            {
                textBox1.Text = "";
            }
        }       // Bu bölümde enter ve leave lerin tamamı içinde değilken üstünde 'Kullanıcı Adı' gibi yazması
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = text;
            }
        }       // için yazdığım birşey.
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == text1)
            {
                textBox2.Text = "";
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = text1;
            }
        }
        
        bool tamamMı;                                                   // İleri kısımda bana lazım olacak çoğu değişkeni public olarak oluşturdum.
        bool tamamMı1;
        public static bool controlOgrenci = true;
        public static bool controlOgretmen = true;
        public static string kullaniciAdi;
        public static string sifre;
        public static string ogrenciID;
        
        public static string ogretmenID;
        public static string ogretmenIsim;
        public static string ogretmenSoyisim;
        public static string ogretmenBolum;
        public static string ogretmenSehir;
        public static string ogretmenFakulte;
        public static string ogretmenMail;
        public static string ogretmenTel;
                                                                   // Butona tıklandığında veri tabanınını kontrol ediyor ve erişilen değerlere göre hangi kullanıcıya
        private void button1_Click(object sender, EventArgs e)     // girileceği belirleniyor.
        {
            
            if (textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == text || textBox2.Text == text1)
            {
                MessageBox.Show("Giriş yapmak için lütfen boş alanları doldurunuz!");
            }
            else
            {
                kullaniciAdi = textBox1.Text;
                sifre = textBox2.Text;
                //con.Baglanti().Open();

                SqlCommand komut = new SqlCommand("Select * From Tbl_OgrenciGiris",con.Baglanti());
                SqlDataReader reader = komut.ExecuteReader();

                while (reader.Read())
                {
                    
                    if ((kullaniciAdi == reader["KimlikNo"].ToString() && sifre == reader["Sifre"].ToString()) || (kullaniciAdi == reader["KullaniciAdi"].ToString() && sifre == reader["Sifre"].ToString()))
                    {
                        ogrenciID = reader["OgrenciID"].ToString();
                        tamamMı = true;
                        controlOgrenci = true;
                        controlOgretmen = false;
                        break;
                    }
                    else
                    {
                        tamamMı = false;
                    }

                }

                con.Baglanti().Close();
                string ogretmenUnvan = "";
                //con.Baglanti().Open();

                SqlCommand komut1= new SqlCommand("Select * From Tbl_OgretmenGiris", con.Baglanti());
                SqlDataReader reader1 = komut1.ExecuteReader();

                while (reader1.Read())
                {
                    
                    if ((kullaniciAdi == reader1["KimlikNo"].ToString() && sifre == reader1["Sifre"].ToString()) || (kullaniciAdi == reader1["KullaniciAdi"].ToString() && sifre == reader1["Sifre"].ToString()))
                    {
                        ogretmenID = reader1["OgretmenID"].ToString();
                        ogretmenUnvan = reader1["Unvan"].ToString();
                        
                        tamamMı1 = true;
                        controlOgretmen = true;
                        controlOgrenci = false;
                        break;
                    }
                    else
                    {
                        tamamMı1 = false;
                    }

                }

                con.Baglanti().Close();

                if (controlOgretmen)
                { 
                    if (tamamMı1)
                    {
                        if (ogretmenUnvan == "Yönetici")
                        {
                            MessageBox.Show("Giriş başarılı!");
                            Form11 form11 = new Form11();
                            this.Visible = false;
                            form11.Show();
                        }
                        else if (ogretmenUnvan == "Öğretmen")
                        {
                            MessageBox.Show("Giriş başarılı!");
                            Form7 form7 = new Form7();
                            this.Visible = false;
                            form7.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hatalı kullanıcı adı veya şifre!");
                    }
                }
                else if (controlOgrenci)
                {
                    if (tamamMı)
                    {
                        MessageBox.Show("Giriş başarılı!");
                        Form3 form3 = new Form3();
                        this.Visible = false;
                        form3.Show();
                    }
                    else
                    {
                        MessageBox.Show("Hatalı kullanıcı adı veya şifre!");
                    }
                }
            }


           
        }

        private void button3_Click(object sender, EventArgs e)     // Kayıt ol fromuna geçme butonu
        {
            Form2 form2 = new Form2();
            this.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
