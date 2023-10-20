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
    public partial class Form10 : Form
    {
        Baglan con = new Baglan();
        public Form10()
        {
            InitializeComponent();
        }
        string dersID = "";
        string drsID = "";
        public static double agno = 0;
        private void Form10_Load(object sender, EventArgs e)
        {
            label2.AutoSize = false;

            label2.Width = 1100;

            label2.Height = 2;

            label2.BorderStyle = BorderStyle.Fixed3D;

            SqlCommand komut = new SqlCommand("select * from Tbl_Ders where OgretmenID="+ Form1.ogretmenID, con.Baglanti());
            SqlDataReader reader = komut.ExecuteReader();

            while(reader.Read())
            {
                dersID = reader["DersID"].ToString();
                comboBox1.Items.Add(reader["DersAdi"].ToString() + " " + "[" + reader["DersKodu"].ToString()+ "]" + " "+ dersID);
            }
            con.Baglanti().Close();

        }
        string tblkomut;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            drsID = comboBox1.Text.Split(' ').Last().ToString();

            VeriGetir(dataGridView1,drsID);
        }
        private void VeriGetir(DataGridView dgv,string drsID)               // Öğrencini verilerini bu fonksiyonla getirdim.
        {

            tblkomut = "select Tbl_Not.OgrenciID AS [ID], [OgrenciNo] AS [Öğrenci Numarası],[Isim] + ' ' + [Soyisim] AS [Isim Soyisim],[DersAdi] AS [Ders Adı],[Vize],[Final],[HarfNotu] AS [Harf Notu] " +
                "from Tbl_Ders join Tbl_Not on Tbl_Ders.DersID = Tbl_Not.DersID join Tbl_Ogrenci on Tbl_Not.OgrenciID = Tbl_Ogrenci.OgrenciID where Tbl_Ders.DersID =" + drsID;

            SqlCommand komut = new SqlCommand(tblkomut, con.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = komut;

            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dgv.DataSource = tablo;
            dgv.Columns[0].Width = 25;
            dgv.Columns[1].Width = 100;
            dgv.Columns[2].Width = 360;
            dgv.Columns[3].Width = 307;
            dgv.Columns[4].Width = 55;
            dgv.Columns[5].Width = 55;
            dgv.Columns[6].Width = 55;
        }
        string ogrID;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)          // Tablodan bir öğrenciye çift tıklanınca öğrencini verilerini
        {                                                                                               // textbox lara aktardım.
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textAdSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textDersAdi.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textVize.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textFinal.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            textHN.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            ogrID = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)            // Öğretmenin Öğrencinin notlarını değiştirebilmesi yada not verebilmesi için yazdığım kısım.
        {
            try
            {
                if (textFinal.Text == "" && textHN.Text == "" && textHN.Text != "")
                {
                    SqlCommand komutGuncelle = new SqlCommand("Update Tbl_Not Set Vize = @g1 where OgrenciID = @g4 and DersID =" + drsID, con.Baglanti());
                    komutGuncelle.Parameters.AddWithValue("@g1", textVize.Text);
                    komutGuncelle.Parameters.AddWithValue("@g4", ogrID);
                    komutGuncelle.ExecuteNonQuery();
                    MessageBox.Show("Güncelleme Başarılı !");
                    con.Baglanti().Close();
                }
                else if (textFinal.Text == "" && textHN.Text != "")
                {
                    MessageBox.Show("Lütfen final notunu giriniz.");
                }
                else if(textFinal.Text == "" && textVize.Text == "" && textHN.Text == "")
                {
                    MessageBox.Show("Değer girmeden önce güncelleme yapamazsınız.");
                }
                else if(textVize.Text == "" && (textFinal.Text != "" || textHN.Text != ""))
                {
                    MessageBox.Show("Vize notu girmeden final ve harf notu girilimez.");
                }
                else
                {
                    if(textHN.Text == "AA" || textHN.Text == "BA" || textHN.Text == "BB" || textHN.Text == "CB" || textHN.Text == "CC" || textHN.Text == "DC" || textHN.Text == "DD" ||
                        textHN.Text == "FD" || textHN.Text == "FF")
                    {
                        SqlCommand komutGuncelle = new SqlCommand("Update Tbl_Not Set Vize = @g1, Final = @g2, HarfNotu = @g3 where OgrenciID = @g4 and DersID =" + drsID, con.Baglanti());
                        komutGuncelle.Parameters.AddWithValue("@g1", textVize.Text);
                        komutGuncelle.Parameters.AddWithValue("@g2", textFinal.Text);
                        komutGuncelle.Parameters.AddWithValue("@g3", textHN.Text);
                        komutGuncelle.Parameters.AddWithValue("@g4", ogrID);
                        komutGuncelle.ExecuteNonQuery();
                        MessageBox.Show("Güncelleme Başarılı !");
                        con.Baglanti().Close();
                    }
                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir harf notu giriniz.");
                    }
                }
                
            }
            catch (Exception hata)
            {
                MessageBox.Show("Lütfen bir öğrenci seçiniz." + hata.Message);
            }
            AgnoHesapla();
            SqlCommand komutGuncelle1 = new SqlCommand("Update Tbl_Ogrenci Set Agno = @g1 where OgrenciID=" + ogrID, con.Baglanti());
            komutGuncelle1.Parameters.AddWithValue("@g1", agno);
            komutGuncelle1.ExecuteNonQuery();
            con.Baglanti().Close();

        }
        private void AgnoHesapla()         // Öğrencinin agnosunu hesaplayıp yeniden veritabanında güncellemesini sağladım.
        {

            SqlCommand komut = new SqlCommand("select [DersKredi], [HarfNotu] from Tbl_Ders join Tbl_Not on Tbl_Ders.DersID=Tbl_Not.DersID where OgrenciID=" + ogrID , con.Baglanti());
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
            if (agno == 0)
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
