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
    public partial class Form11 : Form
    {
        Baglan con = new Baglan();
        public Form11()
        {
            InitializeComponent();
            Design();
        }

        private void Design()
        {
            panel3.Visible = false;
        }

        private void MenuGizle(Panel panelMenu)
        {
            if (panelMenu.Visible == true)
            {
                panelMenu.Visible = false;
            }
        }

        private void MenuGoster(Panel panelMenu)
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


        private Form aktifForm = null;

        private void AltFormAc(Form altForm)         // yeni form oluşturma fonksiyonu.
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
            AltFormAc(new Form12());
        }         // Butonlar menü gizlemek yada yeni formu açmak için var.

        private void button3_Click(object sender, EventArgs e)
        {
            MenuGoster(panel3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AltFormAc(new Form13());


            MenuGizle(panel3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AltFormAc(new Form14());

            MenuGizle(panel3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AltFormAc(new Form15());
            MenuGizle(panel3);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.Visible = true;

        }

        public static string ogretmenIsim;                                   // Yine gerekli olan değişkenleri tanımladım
        public static string ogretmenSoyisim;
        public static string ogretmenBolum;
        public static string ogretmenSehir;
        public static string ogretmenFakulte;
        public static string ogretmenMail;
        public static string ogretmenTel;
        public static string ogretmenKimlikNo;
        public static string ogretmenNo;
        public static string ogretmenDgmTrhi;

        private void Form11_Load(object sender, EventArgs e)
        {
            label2.AutoSize = false;

            label2.Width = 220;

            label2.Height = 2;

            label2.BorderStyle = BorderStyle.Fixed3D;

            if (Form1.controlOgretmen)                                  // Kullanıcı adı ve şifrede girilen değerlere göre veritabanında tarama yapıp 
            {                                                           // admin i bulup değerlerini getirmesini sağladım.

                // SP
                SqlCommand komut = new SqlCommand("OgretmenKontrol", con.Baglanti());
                SqlDataReader reader = komut.ExecuteReader();
                komut.CommandType = CommandType.StoredProcedure;
                while (reader.Read())
                {
                    if (reader["OgretmenID"].ToString() == Form1.ogretmenID)
                    {
                        label3.Text = reader["Unvan"].ToString();
                        label1.Text = reader["Isim"].ToString() + " " + reader["Soyisim"].ToString();

                        ogretmenIsim = reader["Isim"].ToString();
                        ogretmenSoyisim = reader["Soyisim"].ToString();
                        ogretmenBolum = reader["Bolum"].ToString();
                        ogretmenFakulte = reader["Fakulte"].ToString();
                        ogretmenMail = reader["Mail"].ToString();
                        ogretmenSehir = reader["Sehir"].ToString();
                        ogretmenTel = reader["Telefon"].ToString();
                        ogretmenKimlikNo = reader["KimlikNo"].ToString();
                        ogretmenDgmTrhi = reader["DogumTarihi"].ToString();
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

        private void Form11_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AltFormAc(new Form16());

            MenuGizle(panel3);
        }
    }
}
