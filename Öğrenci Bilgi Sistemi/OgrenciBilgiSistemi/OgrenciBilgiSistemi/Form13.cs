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
    public partial class Form13 : Form
    {
        Baglan con = new Baglan();
        public Form13()
        {
            InitializeComponent();
        }

        private void Form13_Load(object sender, EventArgs e)
        {                                                                   // Görsel değişiklikler.
            label2.AutoSize = false;
            label2.Width = 1100;
            label2.Height = 2;
            label2.BorderStyle = BorderStyle.Fixed3D;

            label17.AutoSize = false;
            label17.Width = 1100;
            label17.Height = 2;
            label17.BorderStyle = BorderStyle.Fixed3D;

            label23.AutoSize = false;
            label23.Width = 1100;
            label23.Height = 2;
            label23.BorderStyle = BorderStyle.Fixed3D;

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bolum = comboBox1.SelectedItem.ToString();
            VeriGetir(dataGridView1,bolum);
        }
        string ogrID;
        private void VeriGetir(DataGridView dgv, string bolum)            // Öğrencilerin verilerini bu fonksiyonla getirdim.
        {

            SqlCommand komut = new SqlCommand("select [OgrenciID] AS [ID], [OgrenciNo] AS [Ögr. Numarası], [KimlikNo] AS [Kimlik Numarası],[Isim] + ' ' + [Soyisim] AS [Isim Soyisim],[Bolum]," +
                "[Sehir],[Fakülte],[Agno],[Mail] AS [E-Posta],[Telefon],[DogumTarihi] AS [Doğum Tarihi],[Donem]" +
                " from Tbl_Ogrenci where Bolum='"+ bolum + "'", con.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = komut;
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dgv.DataSource = tablo;
            dgv.Columns[0].Width = 27;
            dgv.Columns[1].Width = 60;
            dgv.Columns[2].Width = 80;
            dgv.Columns[3].Width = 100;
            dgv.Columns[4].Width = 130;
            dgv.Columns[5].Width = 70;
            dgv.Columns[6].Width = 120;
            dgv.Columns[7].Width = 50;
            dgv.Columns[8].Width = 100;
            dgv.Columns[9].Width = 100;
            dgv.Columns[10].Width = 70;
            dgv.Columns[11].Width = 50;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)   // çift tıklanınca veriler textboxlara aktarılıyor.
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox14.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox12.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox15.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox16.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBox13.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            ogrID = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)          // textboxa gelen öğrencinin verileri bu butona tıklanınca veritabanından siliniyor
        {
            try
            {
                SqlCommand komut1 = new SqlCommand("Delete from Tbl_OgrenciGiris where OgrenciID = @g2", con.Baglanti());
                komut1.Parameters.AddWithValue("@g2", ogrID);
                komut1.ExecuteNonQuery();
                con.Baglanti().Close();

                MessageBox.Show("Öğrenci Kaldırıldı.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)              // Kayıt ol kısmındaki gibi bu butona tıklanınca girilen bütün veriler 
        {                                                                   // Yeni bir öğrenci oluşturulup yerine yazılıyor.
            try
            {
                if(textBox3.Text == "" || textBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "" ||
                    textBox7.Text == "" || textBox8.Text == "" || textBox4.Text == "" || textBox10.Text == "" || textBox11.Text == "" || comboBox6.Text == "")
                {
                    MessageBox.Show("Lütfen bütün kutuları doldurunuz.");
                }
                else
                {
                    //SP
                    SqlCommand komut = new SqlCommand("OgrenciKayit", con.Baglanti());
                    komut.CommandType = CommandType.StoredProcedure;
                    string tck = textBox3.Text;
                    komut.Parameters.AddWithValue("@KimlikNo", textBox3.Text);
                    komut.Parameters.AddWithValue("@Isim", textBox1.Text);
                    komut.Parameters.AddWithValue("@Soyisim", textBox2.Text);
                    komut.Parameters.AddWithValue("@Bolum", comboBox2.Text);
                    komut.Parameters.AddWithValue("@Sehir", comboBox3.Text);
                    komut.Parameters.AddWithValue("@Fakülte", comboBox4.Text);
                    komut.Parameters.AddWithValue("@Mail", textBox7.Text);
                    komut.Parameters.AddWithValue("@Telefon", textBox8.Text);
                    komut.Parameters.AddWithValue("@DogumTarihi", textBox4.Text);
                    komut.Parameters.AddWithValue("@Donem", comboBox6.SelectedItem);

                    komut.ExecuteNonQuery();

                    con.Baglanti().Close();
                    string ogrID = "";
                    SqlCommand komut1 = new SqlCommand("select * from Tbl_Ogrenci where KimlikNo = " + tck, con.Baglanti());
                    SqlDataReader reader = komut1.ExecuteReader();

                    while (reader.Read())
                    {
                        ogrID = reader["OgrenciID"].ToString();
                    }
                    con.Baglanti().Close();

                    string veri = (Convert.ToInt32(ogrID) + 99).ToString();


                    SqlCommand komut2 = new SqlCommand("Update Tbl_Ogrenci Set OgrenciNo = @o1 Where KimlikNo = " + tck, con.Baglanti());
                    komut2.Parameters.AddWithValue("@o1", veri);
                    komut2.ExecuteNonQuery();
                    con.Baglanti().Close();

                    //SP
                    SqlCommand komut3 = new SqlCommand("OgrenciGirisKayit", con.Baglanti());
                    komut3.CommandType = CommandType.StoredProcedure;
                    komut3.Parameters.AddWithValue("@OgrenciID", Convert.ToInt32(ogrID));
                    komut3.Parameters.AddWithValue("@KimlikNo", tck);
                    komut3.Parameters.AddWithValue("@KullaniciAdi", textBox10.Text);
                    komut3.Parameters.AddWithValue("@Sifre", textBox11.Text);
                    komut3.Parameters.AddWithValue("@Unvan", "Öğrenci");
                    komut3.ExecuteNonQuery();
                    con.Baglanti().Close();

                    MessageBox.Show("Öğrenci Eklendi.");
                }
            }
            catch(Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = false;
            textBox4.Text = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
