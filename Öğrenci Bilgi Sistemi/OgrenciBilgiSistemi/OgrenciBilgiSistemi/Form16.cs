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
    public partial class Form16 : Form
    {
        Baglan con = new Baglan();
        public Form16()
        {
            InitializeComponent();
        }

        private void Form16_Load(object sender, EventArgs e)
        {                                                       // Görsel değişiklikler.
            label2.AutoSize = false;
            label2.Width = 1100;
            label2.Height = 2;
            label2.BorderStyle = BorderStyle.Fixed3D;
        }

        private void VeriGetir(DataGridView dgv, string ogrenci)        // ComboBoxtan seçilen öğrencini dersleri bu fonksiyonla getiriliyor.
        {

            SqlCommand komut = new SqlCommand("select Tbl_Ders.DersID AS [DID],[DersKodu] AS [Ders Kodu],[DersAdi] AS [Ders Adı],[DersiVeren] AS [Dersi Veren]," +
                " [DersKredi] AS [Kredi],Tbl_Ders.Donem from Tbl_Ogrenci " +
                " join Tbl_Not on Tbl_Ogrenci.OgrenciID=Tbl_Not.OgrenciID " +
                " join Tbl_Ders on Tbl_Not.DersID=Tbl_Ders.DersID " +
                " where Tbl_Not.OgrenciID='" + ogrenci + "'", con.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = komut;
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dgv.DataSource = tablo;
            dgv.Columns[0].Width = 37;
            dgv.Columns[1].Width = 200;
            dgv.Columns[2].Width = 300;
            dgv.Columns[3].Width = 300;
            dgv.Columns[4].Width = 40;
            dgv.Columns[5].Width = 40;


        }
        private void OgrenciGetir(string bolum)         // Bu fonksiyonla ComboBox a veritabanındaki öğrenciler getiriliyor.
        {
            SqlCommand komut = new SqlCommand("select [OgrenciID],[Isim],[Soyisim] " +
                "from Tbl_Ogrenci where Bolum='" + bolum + "'", con.Baglanti());
            SqlDataReader reader = komut.ExecuteReader();

            while (reader.Read())
            {
                comboBox3.Items.Add(reader["Isim"].ToString() + " " + reader["Soyisim"] + " " + reader["OgrenciID"].ToString());
            }
            con.Baglanti().Close();
        }
        bool calistiMi = true;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bolum = comboBox1.SelectedItem.ToString();
            OgrenciGetir(bolum);
            if(calistiMi)
            {
                DersGetir(bolum);
                calistiMi = false;
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)      // Derslerden birine çift tıklanınca verileri textbox a aktarılıyor.
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox3.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void DersGetir(string bolum)            // Öğrenciye yeni Deres verebilmek için bu fonksiyonla ComboBox a veritabanındaki dersler getiriliyor.
        {
            SqlCommand komut = new SqlCommand("select [DersID],[DersAdi] from Tbl_Ders where Bolum='" + bolum + "'", con.Baglanti());
            SqlDataReader reader = komut.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(reader["DersAdi"].ToString() + " " + reader["DersID"]);
            }
            con.Baglanti().Close();

        }
        string dersID;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dersID = comboBox2.Text.Split(' ').Last().ToString();
            textBox2.Text = dersID.ToString();
        }

        private void button1_Click(object sender, EventArgs e)          // Bu butona tıklanınca textbox daki ders veritabanında seçiilen öğrenciye giriliyor.
        {
            try
            {
                
                bool esitMi = false;
                for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
                {
                    if(textBox2.Text == dataGridView1.Rows[rows].Cells[0].Value.ToString())
                    {
                        esitMi = true;
                    }
                }

                if(esitMi)
                {
                    MessageBox.Show("HATA: Bir öğrenci aldığı dersi yeniden alamaz.");
                }
                else
                {
                    SqlCommand komut = new SqlCommand("insert into Tbl_Not(OgrenciID,DersID) values(@OgrenciID,@DersID)", con.Baglanti());
                    komut.Parameters.AddWithValue("@OgrenciID", textBox4.Text);
                    komut.Parameters.AddWithValue("@DersID", textBox2.Text);
                    MessageBox.Show("Ders başarıyla eklendi.");
                    komut.ExecuteNonQuery();
                }

                con.Baglanti().Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ogrnID = comboBox3.Text.Split(' ').Last().ToString();
            textBox4.Text = ogrnID;
            VeriGetir(dataGridView1,ogrnID);
        }

        private void button2_Click(object sender, EventArgs e)      // Bu butona tıklanınca seçilen ders veritabanında öğrenciden kaldırılıyor.
        {
            try
            {
                if(textBox4.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("Lütfen bütün kutuları doldurunuz.");
                }
                else
                {
                    SqlCommand komut = new SqlCommand("Delete from Tbl_Not where OgrenciID=@OgrenciID and DersID=@DersID", con.Baglanti());
                    komut.Parameters.AddWithValue("@OgrenciID", textBox4.Text);
                    komut.Parameters.AddWithValue("@DersID", textBox1.Text);
                    komut.ExecuteNonQuery();
                    con.Baglanti().Close();

                    MessageBox.Show("Ders başarıyla çıkarıldı.");
                }
            }
            catch(Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
    }
}
