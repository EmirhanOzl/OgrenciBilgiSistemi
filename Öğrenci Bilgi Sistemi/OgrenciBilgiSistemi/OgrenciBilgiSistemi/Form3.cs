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
    public partial class Form3 : Form
    {
        Baglan con = new Baglan();

        public Form3()
        {
            InitializeComponent();
            Design();
        }

        private void Design()          // Güzel görünmesi için yaptığım küçük bir şey.
        {
            panel3.Visible = false;
        }

        private void MenuGizle(Panel panelMenu)       // Menüleri gizlemek için.
        {
            if (panelMenu.Visible == true)
            {
                panelMenu.Visible = false;
            }
        }

        private void MenuGoster(Panel panelMenu)      // Menüyü yeniden göstermek için.
        {
            if (panelMenu.Visible == false)
            {
                MenuGizle(panelMenu);
                panelMenu.Visible = true;
            }
            else
            {
                panelMenu.Visible = false;
            }
        }
        
                                                      // Bu kısımdada ve ileridede lazı olacak değişkenleri oluşturdum.                              
        public static string ogrenciIsim;
        public static string ogrenciSoyisim;
        public static string ogrenciBolum;
        public static string ogrenciSehir;
        public static string ogrenciFakulte;
        public static string ogrenciAgno;
        public static string ogrenciMail;
        public static string ogrenciTel;
        public static string ogrenciDonem;
        public static string ogrenciKimlikNo;
        public static string ogrenciNo;
        public static string ogrenciDgmTrhi;

        public static double agno = 0;
        private void Form3_Load(object sender, EventArgs e)     // Bazı küçük görsel değişiklikler ve
        {                                                       // Bu forma gelindiğinde girilen kullanıcı adı şifresini veritabanında tarayıp
            label2.AutoSize = false;                            // Onun bilgilerini buraya aktarıyor.

            label2.Width = 220;

            label2.Height = 2;

            label2.BorderStyle = BorderStyle.Fixed3D;

            AgnoHesapla();
            SqlCommand komutGuncelle = new SqlCommand("Update Tbl_Ogrenci Set Agno = @g1 where OgrenciID=" + Form1.ogrenciID, con.Baglanti());
            komutGuncelle.Parameters.AddWithValue("@g1", agno);
            komutGuncelle.ExecuteNonQuery();
            con.Baglanti().Close();


            if (Form1.controlOgrenci)
            {

                // SP
                SqlCommand komut = new SqlCommand("OgrenciKontrol", con.Baglanti());
                SqlDataReader reader = komut.ExecuteReader();
                komut.CommandType = CommandType.StoredProcedure;
                while (reader.Read())
                {
                    if (reader["OgrenciID"].ToString() == Form1.ogrenciID)
                    {
                        label3.Text = reader["Unvan"].ToString();
                        label1.Text = reader["Isim"].ToString() + " " + reader["Soyisim"].ToString();

                        ogrenciIsim = reader["Isim"].ToString();
                        ogrenciSoyisim = reader["Soyisim"].ToString();
                        ogrenciAgno = reader["Agno"].ToString();
                        ogrenciBolum = reader["Bolum"].ToString();
                        ogrenciDonem = reader["Donem"].ToString();
                        ogrenciFakulte = reader["Fakülte"].ToString();
                        ogrenciMail = reader["Mail"].ToString();
                        ogrenciSehir = reader["Sehir"].ToString();
                        ogrenciTel = reader["Telefon"].ToString();
                        ogrenciKimlikNo = reader["KimlikNo"].ToString();
                        ogrenciNo = reader["OgrenciNo"].ToString();
                        ogrenciDgmTrhi = reader["DogumTarihi"].ToString();
                    }
                }
                con.Baglanti().Close();

            }    
            else if (Form1.controlOgretmen)
            {

                SqlCommand komut1 = new SqlCommand("Select * From Tbl_OgretmenGiris inner join Tbl_Ogretmen on Tbl_OgretmenGiris.OgretmenID=Tbl_Ogretmen.OgretmenID", con.Baglanti());
                SqlDataReader reader1 = komut1.ExecuteReader();
                while (reader1.Read())
                {
                    if (reader1["OgretmenID"].ToString() == Form1.ogretmenID)
                    {
                        label3.Text = reader1["Unvan"].ToString();
                        label1.Text = reader1["Isim"].ToString() + " " + reader1["Soyisim"].ToString();
                    }
                }
                con.Baglanti().Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MenuGoster(panel3);
        }   // Bu butonlar menüleri gizlemek için ve yeni forma geçmek için yazılmış.

        private void button2_Click(object sender, EventArgs e)
        {
            AltFormAc(new Form5());


            MenuGizle(panel3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AltFormAc(new Form6());

            MenuGizle(panel3);
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.Visible = true;
            
        }

        private Form aktifForm = null;
        private void AltFormAc(Form altForm)        // Daha sonra kullanmak için yeni form oluşturdum ve özelliklerini atadım.
        {
            if (aktifForm != null)
                aktifForm.Close();
            aktifForm = altForm;
            altForm.TopLevel = false;
            altForm.FormBorderStyle = FormBorderStyle.None;
            altForm.Dock = DockStyle.Fill;
            panel7.Controls.Add(altForm);
            panel7.Tag = altForm;
            altForm.BringToFront();
            altForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AltFormAc(new Form4());
        }

        
        private void AgnoHesapla()         // Öğrencinin agnosunu hesaplayıp yeniden veritabanında güncellemesini sağladım.
        {

            SqlCommand komut = new SqlCommand("select [DersKredi], [HarfNotu] from Tbl_Ders join Tbl_Not on Tbl_Ders.DersID=Tbl_Not.DersID where OgrenciID=" + Form1.ogrenciID, con.Baglanti());
            SqlDataReader reader = komut.ExecuteReader();

            double AA = 4.0;
            double BA = 3.5;
            double BB = 3.0;
            double CB = 2.5;
            double CC = 2.0;
            double DC = 1.5;
            double DD = 1.0;
            double FD = 0.5;
            double FF = 0.0;
            agno = 0;

            int i = 0;
            double kreditoplam = 0;

            while (reader.Read())
            {
                string derskredi = reader["DersKredi"].ToString();


                if (reader["HarfNotu"].ToString() == "AA")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * AA);
                    kreditoplam += Convert.ToDouble(derskredi);
                    i++;
                }
                else if (reader["HarfNotu"].ToString() == "BA")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * BA);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else if (reader["HarfNotu"].ToString() == "BB")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * BB);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else if (reader["HarfNotu"].ToString() == "CB")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * CB);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else if (reader["HarfNotu"].ToString() == "CC")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * CC);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else if (reader["HarfNotu"].ToString() == "DC")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * DC);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else if (reader["HarfNotu"].ToString() == "DD")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * DD);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else if (reader["HarfNotu"].ToString() == "FD")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * FD);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else if (reader["HarfNotu"].ToString() == "FF")
                {
                    agno = agno + (Convert.ToDouble(derskredi) * FF);
                    kreditoplam += Convert.ToDouble(derskredi);
                }
                else
                {
                    continue;
                }
            }
            if(agno == 0)
            {
                agno = 0;
            }
            else
            {
                agno = agno / kreditoplam;
            }
            
            agno = Math.Round(agno, 2);
            con.Baglanti().Close();
        }
    }
}
