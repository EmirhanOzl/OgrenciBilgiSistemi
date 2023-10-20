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
    public partial class Form2 : Form
    {

        Baglan con = new Baglan();

        Form1 form1 = new Form1();
        string text = "T.C Kimlik No";                        // Form 1 deki gibi textbox ın içinde değilken üstünde yazı yazması için
        string text1 = "Kullanıcı Adı";                       // hepsine değer atadım.
        string text2 = "Adınız";
        string text3 = "Soyadınız";
        string text4 = "Doğum Tarihi";
        string text5 = "E-Posta";
        string text7 = "Şifre";
        string text8 = "Şifre Tekrar";
        string text9 = "Cep No";
        public Form2()
        {
            InitializeComponent();
            textBox1.Text = text;       // Bu değerleri içine yazdırdım.
            textBox2.Text = text1;
            textBox3.Text = text2;
            textBox4.Text = text3;
            textBox6.Text = text4;
            textBox5.Text = text5;
            textBox7.Text = text7;
            textBox8.Text = text8;
            textBox9.Text = text9;
            this.Visible = true;

        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = true;

            if (false)
            {
                dateTimePicker1.Visible = false;
            }

            if (textBox6.Text == text4)
            {
                textBox6.Text = "";
            }

        }             // Enter ve Leave ler içine girip çıktığımızda yazı yazıp kaldırmasını sağlıyor.

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == text)
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = text;
            }
        }

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

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == text2)
            {
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = text2;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == text3)
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = text3;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = text4;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == text5)
            {
                textBox5.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = text5;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == text7)
            {
                textBox7.Text = "";
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = text7;
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == text8)
            {
                textBox8.Text = "";
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = text8;
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)     // Doğum tarihinialıp textboxa yazdırdım.
        {
            dateTimePicker1.Visible = false;
            textBox6.Text = dateTimePicker1.Value.Year.ToString() +"-"+ dateTimePicker1.Value.Month.ToString() +"-"+ dateTimePicker1.Value.Day.ToString();
        }

        private void button2_Click(object sender, EventArgs e)             // Yeniden giriş menüsüne geçme butonu
        {
            
            this.Visible = false;
            form1.Visible = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == text9)
            {
                textBox9.Text = "";
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = text9;
            }
        }
                                                                            // Bu kısımda veirlen bütün değerler veri tabanında yerlerine oturuyor
        private void button1_Click(object sender, EventArgs e)              // ve yeni bir öğrenci yada öğretmen oluşturuluyor.
        {
            if ((textBox1.Text == "" || textBox1.Text == text) || (textBox2.Text == "" || textBox2.Text == text1) ||
                (textBox3.Text == "" || textBox3.Text == text2) || (textBox4.Text == "" || textBox4.Text == text3) ||
                (textBox6.Text == "" || textBox6.Text == text4) || (comboBox1.Text == "Cinsiyet") && (comboBox2.Text == "Şehir") ||
                (textBox5.Text == "" || textBox5.Text == text5) || (textBox9.Text == "" || textBox9.Text == text9) ||
                (textBox7.Text == "" || textBox7.Text == text7) || (textBox8.Text == "" || textBox8.Text == text8) ||
                (comboBox4.Text == "Bölüm" || comboBox5.Text == "Fakülte") || (comboBox3.Text == "Ünvan"))
            {
                MessageBox.Show("Bütün Bilgileri Doğru Bir Şekilde Giriniz.");
            }
            else if (textBox7.Text != textBox8.Text)
            {
                MessageBox.Show("Lütfen Şifrelerin Aynı Olduğunda Emin Olun.");  
            }
            else
            {
                if (comboBox3.Text == "Öğrenci")
                {
                    try
                    {
                        //con.Baglanti().Open();
                        //SP
                        SqlCommand komut = new SqlCommand("OgrenciKayit", con.Baglanti());
                        komut.CommandType = CommandType.StoredProcedure;
                        string tck = textBox1.Text;
                        komut.Parameters.AddWithValue("@KimlikNo", textBox1.Text);
                        komut.Parameters.AddWithValue("@Isim", textBox3.Text);
                        komut.Parameters.AddWithValue("@Soyisim", textBox4.Text);
                        komut.Parameters.AddWithValue("@Bolum", comboBox4.Text);
                        komut.Parameters.AddWithValue("@Sehir", comboBox2.Text);
                        komut.Parameters.AddWithValue("@Fakülte", comboBox5.Text);
                        komut.Parameters.AddWithValue("@Mail", textBox5.Text);
                        komut.Parameters.AddWithValue("@Telefon", textBox9.Text);
                        komut.Parameters.AddWithValue("@DogumTarihi", textBox6.Text);
                        komut.Parameters.AddWithValue("@Donem", 1);

                        komut.ExecuteNonQuery();

                        con.Baglanti().Close();
                        string ogrID = "";
                        //con.Baglanti().Open();
                        SqlCommand komut1 = new SqlCommand("select * from Tbl_Ogrenci where KimlikNo = " + tck, con.Baglanti());
                        SqlDataReader reader = komut1.ExecuteReader();

                        while (reader.Read())
                        {
                            ogrID = reader["OgrenciID"].ToString();
                        }
                        con.Baglanti().Close();

                        string veri = (Convert.ToInt32(ogrID) + 99).ToString();


                        //con.Baglanti().Open();
                        SqlCommand komut2 = new SqlCommand("Update Tbl_Ogrenci Set OgrenciNo = @o1 Where KimlikNo = " + tck, con.Baglanti());
                        komut2.Parameters.AddWithValue("@o1", veri);
                        komut2.ExecuteNonQuery();
                        con.Baglanti().Close();

                        //con.Baglanti().Open();
                        //SP
                        SqlCommand komut3 = new SqlCommand("OgrenciGirisKayit", con.Baglanti());
                        komut3.CommandType = CommandType.StoredProcedure;
                        komut3.Parameters.AddWithValue("@OgrenciID", Convert.ToInt32(ogrID));
                        komut3.Parameters.AddWithValue("@KimlikNo", tck);
                        komut3.Parameters.AddWithValue("@KullaniciAdi", textBox2.Text);
                        komut3.Parameters.AddWithValue("@Sifre", textBox7.Text);
                        komut3.Parameters.AddWithValue("@Unvan", comboBox3.Text);
                        komut3.ExecuteNonQuery();
                        con.Baglanti().Close();

                        //con.Baglanti().Open();
                        SqlCommand komut10 = new SqlCommand("insert into Tbl_Not(OgrenciID,DersID) VALUES (" + ogrID + "," + 1 + ")", con.Baglanti());
                        komut10.ExecuteNonQuery();
                        con.Baglanti().Close();
                        //con.Baglanti().Open();
                        SqlCommand komut11 = new SqlCommand("insert into Tbl_Not(OgrenciID,DersID) VALUES (" + ogrID + "," + 4 + ")", con.Baglanti());
                        komut11.ExecuteNonQuery();
                        con.Baglanti().Close();
                        //con.Baglanti().Open();
                        SqlCommand komut12 = new SqlCommand("insert into Tbl_Not(OgrenciID,DersID) VALUES (" + ogrID + "," + 5 + ")", con.Baglanti());
                        komut12.ExecuteNonQuery();
                        con.Baglanti().Close();
                        //con.Baglanti().Open();
                        SqlCommand komut13 = new SqlCommand("insert into Tbl_Not(OgrenciID,DersID) VALUES (" + ogrID + "," + 6 + ")", con.Baglanti());
                        komut13.ExecuteNonQuery();
                        con.Baglanti().Close();
                        //con.Baglanti().Open();
                        SqlCommand komut14 = new SqlCommand("insert into Tbl_Not(OgrenciID,DersID) VALUES (" + ogrID + "," + 7 + ")", con.Baglanti());
                        komut14.ExecuteNonQuery();
                        con.Baglanti().Close();
                        //con.Baglanti().Open();
                        SqlCommand komut15 = new SqlCommand("insert into Tbl_Not(OgrenciID,DersID) VALUES (" + ogrID + "," + 8 + ")", con.Baglanti());
                        komut15.ExecuteNonQuery();
                        con.Baglanti().Close();



                        MessageBox.Show("Öğrenci Kayıt İşlemi Gerçekleşti.");


                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
                    }
                }
                else if (comboBox3.Text == "Öğretmen")
                {
                    try
                    {
                        //con.Baglanti().Open();
                        //SP
                        SqlCommand komut = new SqlCommand("OgretmenKayit", con.Baglanti());
                        komut.CommandType = CommandType.StoredProcedure;
                        string tck = textBox1.Text;
                        komut.Parameters.AddWithValue("@KimlikNo", textBox1.Text);
                        komut.Parameters.AddWithValue("@Isim", textBox3.Text);
                        komut.Parameters.AddWithValue("@Soyisim", textBox4.Text);
                        komut.Parameters.AddWithValue("@Bolum", comboBox4.Text);
                        komut.Parameters.AddWithValue("@Sehir", comboBox2.Text);
                        komut.Parameters.AddWithValue("@Fakülte", comboBox5.Text);
                        komut.Parameters.AddWithValue("@Mail", textBox5.Text);
                        komut.Parameters.AddWithValue("@Telefon", textBox9.Text);
                        komut.Parameters.AddWithValue("@DogumTarihi", textBox6.Text);

                        komut.ExecuteNonQuery();

                        con.Baglanti().Close();

                        string ogrtmnID = "";
                        //con.Baglanti().Open();
                        SqlCommand komut1 = new SqlCommand("select * from Tbl_Ogretmen where KimlikNo = " + tck, con.Baglanti());
                        SqlDataReader reader = komut1.ExecuteReader();

                        while (reader.Read())
                        {
                            ogrtmnID = reader["OgretmenID"].ToString();
                        }
                        con.Baglanti().Close();


                        //con.Baglanti().Open();
                        //SP
                        SqlCommand komut3 = new SqlCommand("OgretmenGirisKayit", con.Baglanti());
                        komut3.CommandType = CommandType.StoredProcedure;
                        komut3.Parameters.AddWithValue("@OgretmenID", Convert.ToInt32(ogrtmnID));
                        komut3.Parameters.AddWithValue("@KimlikNo", tck);
                        komut3.Parameters.AddWithValue("@KullaniciAdi", textBox2.Text);
                        komut3.Parameters.AddWithValue("@Sifre", textBox7.Text);
                        komut3.Parameters.AddWithValue("@Unvan", comboBox3.Text);
                        komut3.ExecuteNonQuery();
                        con.Baglanti().Close();


                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
                    }
                }
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
