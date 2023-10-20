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
    public partial class Form14 : Form
    {
        Baglan con = new Baglan();
        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {                                                       // Görsel değişiklikler.
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

        private void VeriGetir(DataGridView dgv, string bolum)      // Öğretmenlerin verilerini bu fonksiyon getiriyor.
        {

            SqlCommand komut = new SqlCommand("select [OgretmenID] AS [ID],[KimlikNo] AS [Kimlik Numarası],[Isim] + ' ' + [Soyisim] AS [Isim Soyisim],[Bolum]," +
                "[Sehir],[Fakulte],[Mail] AS [E-Posta],[Telefon],[DogumTarihi] AS [Doğum Tarihi]" +
                " from Tbl_Ogretmen where Bolum='" + bolum + "'", con.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = komut;
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dgv.DataSource = tablo;
            dgv.Columns[0].Width = 27;
            dgv.Columns[1].Width = 100;
            dgv.Columns[2].Width = 160;
            dgv.Columns[3].Width = 180;
            dgv.Columns[4].Width = 80;
            dgv.Columns[5].Width = 130;
            dgv.Columns[6].Width = 100;
            dgv.Columns[7].Width = 80;
            dgv.Columns[8].Width = 100;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bolum = comboBox1.SelectedItem.ToString();
            VeriGetir(dataGridView1, bolum);
        }

        private void button1_Click(object sender, EventArgs e)          // Kayıt ol kısmındaki gibi girilen bütün veriler butona tıklanınca
        {                                                               // yeni bir öğretmen oluşturulup yerine yazılıyor.
            try
            {
                
                
                if(textBox3.Text == "" || textBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "" ||
                    textBox7.Text == "" || textBox8.Text == "" || textBox4.Text == "" || textBox10.Text == "" || textBox11.Text == "")
                {
                    MessageBox.Show("Lütfen bütün kutuları doldurun.");
                }
                else
                {
                    //SP
                    SqlCommand komut = new SqlCommand("OgretmenKayit", con.Baglanti());
                    komut.CommandType = CommandType.StoredProcedure;
                    string tck = textBox3.Text;
                    komut.Parameters.AddWithValue("@KimlikNo", textBox3.Text);
                    komut.Parameters.AddWithValue("@Isim", textBox1.Text);
                    komut.Parameters.AddWithValue("@Soyisim", textBox2.Text);
                    komut.Parameters.AddWithValue("@Bolum", comboBox2.SelectedItem);
                    komut.Parameters.AddWithValue("@Sehir", comboBox3.SelectedItem);
                    komut.Parameters.AddWithValue("@Fakulte", comboBox4.SelectedItem);
                    komut.Parameters.AddWithValue("@Mail", textBox7.Text);
                    komut.Parameters.AddWithValue("@Telefon", textBox8.Text);
                    komut.Parameters.AddWithValue("@DogumTarihi", textBox4.Text);

                    komut.ExecuteNonQuery();

                    con.Baglanti().Close();

                    string ogrtmnID = "";
                    SqlCommand komut1 = new SqlCommand("select * from Tbl_Ogretmen where KimlikNo = " + tck, con.Baglanti());
                    SqlDataReader reader = komut1.ExecuteReader();

                    while (reader.Read())
                    {
                        ogrtmnID = reader["OgretmenID"].ToString();
                    }
                    con.Baglanti().Close();


                    //SP
                    SqlCommand komut3 = new SqlCommand("OgretmenGirisKayit", con.Baglanti());
                    komut3.CommandType = CommandType.StoredProcedure;
                    komut3.Parameters.AddWithValue("@OgretmenID", Convert.ToInt32(ogrtmnID));
                    komut3.Parameters.AddWithValue("@KimlikNo", tck);
                    komut3.Parameters.AddWithValue("@KullaniciAdi", textBox10.Text);
                    komut3.Parameters.AddWithValue("@Sifre", textBox11.Text);
                    komut3.Parameters.AddWithValue("@Unvan", "Öğretmen");
                    komut3.ExecuteNonQuery();
                    con.Baglanti().Close();

                    MessageBox.Show("Öğretmen Eklendi.");
                }

                


            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = false;
            textBox4.Text = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
        }
        
        string ogrtmnID;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)      // Öğretmenlerden birine çift tıklanınca verileri textboxlara aktarılıyor.
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox14.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox12.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox16.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox13.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            ogrtmnID = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }
        private void button2_Click(object sender, EventArgs e)          // Bu butona tıklandığında Öğretmen veritabanından kaldırılıyor.
        {
            try
            {
                SqlCommand komut2 = new SqlCommand("Update Tbl_Ders Set OgretmenID = NULL,DersiVeren = NULL where OgretmenID = @g2", con.Baglanti());
                komut2.Parameters.AddWithValue("@g2", ogrtmnID);
                komut2.ExecuteNonQuery();
                con.Baglanti().Close();

                SqlCommand komut1 = new SqlCommand("Delete from Tbl_OgretmenGiris where OgretmenID = @g2", con.Baglanti());
                komut1.Parameters.AddWithValue("@g2", ogrtmnID);
                komut1.ExecuteNonQuery();
                con.Baglanti().Close();

                MessageBox.Show("Öğretmen Kaldırıldı.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
        
    }
}
