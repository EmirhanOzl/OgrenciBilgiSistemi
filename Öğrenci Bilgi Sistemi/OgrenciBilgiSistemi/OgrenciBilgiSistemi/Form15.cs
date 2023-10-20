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
    public partial class Form15 : Form
    {
        Baglan con = new Baglan();
        public Form15()
        {
            InitializeComponent();
        }
        
        private void Form15_Load(object sender, EventArgs e)
        {                                                          // Görsel değişiklikler.
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

            label12.AutoSize = false;
            label12.Width = 1100;
            label12.Height = 2;
            label12.BorderStyle = BorderStyle.Fixed3D;
        }

        private void VeriGetir(DataGridView dgv, string bolum)      // Derslerin verilerini bu fonksiyon getiriyor.
        {

            SqlCommand komut = new SqlCommand("select [DersID] AS [ID],[Bolum],[DersKodu] AS [Ders Kodu],[DersAdi] AS [Ders Adı],[DersiVeren] AS [Dersi Veren]," +
                "[DersKredi] AS [Kredi],[Donem]" +
                " from Tbl_Ders where Bolum='" + bolum + "'", con.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = komut;
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dgv.DataSource = tablo;
            dgv.Columns[0].Width = 28;
            dgv.Columns[1].Width = 210;
            dgv.Columns[2].Width = 120;
            dgv.Columns[3].Width = 280;
            dgv.Columns[4].Width = 200;
            dgv.Columns[5].Width = 50;
            dgv.Columns[6].Width = 50;

        }
        bool calistiMi = true;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bolum = comboBox1.SelectedItem.ToString();
            VeriGetir(dataGridView1, bolum);
            
            if(calistiMi)
            {
                OgretmenGetir(bolum);
                calistiMi = false;
            }
        }

        string dersID;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)      // Derslerden birine çift tıklanınca verileri textbox lara aktarılıyor.
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox14.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox12.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox15.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBox16.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox13.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            dersID = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); 
        }
        private void OgretmenGetir(string bolum)        // ComboBox a Öğretmenlerin getirilmesi için bir fonksiyon.
        {
            SqlCommand komut = new SqlCommand("select [OgretmenID],[Isim],[Soyisim] from Tbl_Ogretmen where Bolum='" + bolum + "'", con.Baglanti());
            SqlDataReader reader = komut.ExecuteReader();

            while(reader.Read())
            {
                comboBox4.Items.Add(reader["Isim"].ToString() + " " + reader["Soyisim"].ToString() + " "  + reader["OgretmenID"] );
                comboBox7.Items.Add(reader["Isim"].ToString() + " " + reader["Soyisim"].ToString() + " " + reader["OgretmenID"]);
            }
            con.Baglanti().Close();

        }
        
        private void button1_Click(object sender, EventArgs e)      // Bu butona tıklanınca girilen tüm verilerle veritabanında yeni bir ders oluşturuluyor.
        {
            try
            {
                if(textBox1.Text == "" || comboBox2.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox4.Text == "" ||
                    comboBox6.Text == "" || comboBox5.Text == "")
                {
                    MessageBox.Show("Lütfen bütün kutuları doldurunuz.");
                }
                else
                {
                    //SP
                    SqlCommand komut = new SqlCommand("DersEkle", con.Baglanti());
                    komut.CommandType = CommandType.StoredProcedure;

                    komut.Parameters.AddWithValue("@OgretmenID", textBox1.Text);
                    komut.Parameters.AddWithValue("@Bolum", comboBox2.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@DersKodu", textBox2.Text);
                    komut.Parameters.AddWithValue("@DersAdi", textBox3.Text);
                    komut.Parameters.AddWithValue("@DersiVeren", ogrtmnIsim);
                    komut.Parameters.AddWithValue("@DersKredi", comboBox6.SelectedItem);
                    komut.Parameters.AddWithValue("@Donem", comboBox5.SelectedItem);

                    if (comboBox3.SelectedItem.ToString() == "Evet")
                    {
                        komut.Parameters.AddWithValue("@Secmeli", comboBox3.SelectedItem.ToString());
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@Secmeli", "");
                    }

                    komut.ExecuteNonQuery();

                    con.Baglanti().Close();

                    MessageBox.Show("Ders Eklendi.");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        string ogrtmnID;
        string ogrtmnIsim;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ogrtmnID = comboBox4.Text.Split(' ').Last().ToString();
            textBox1.Text = ogrtmnID.ToString();
            string isim = comboBox4.SelectedItem.ToString();
            string[] ayir = isim.Split(' ');
            ogrtmnIsim = "";
            for (int i = 0;i<ayir.Length - 1;i++)
            {
                ogrtmnIsim = ogrtmnIsim + ayir[i] + " ";
            }
        }

        private void button2_Click(object sender, EventArgs e)          //  Bu butona tıklanınca ders veritabanından kaldırılıypr.
        {
            try
            {
                SqlCommand komut1 = new SqlCommand("Delete from Tbl_Ders where DersID = @g2", con.Baglanti());
                komut1.Parameters.AddWithValue("@g2", dersID);
                komut1.ExecuteNonQuery();
                con.Baglanti().Close();

                MessageBox.Show("Ders Kaldırıldı.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        string ogrtmnID2;
        string ogrtmnIsim2;
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ogrtmnID2 = comboBox7.Text.Split(' ').Last().ToString();
            textBox4.Text = ogrtmnID2.ToString();
            string isim = comboBox7.SelectedItem.ToString();
            string[] ayir = isim.Split(' ');
            ogrtmnIsim2 = "";
            for (int i = 0; i < ayir.Length - 1; i++)
            {
                ogrtmnIsim2 = ogrtmnIsim2 + ayir[i] + " ";
            }
        }

        private void button3_Click(object sender, EventArgs e)      // Bu butona tıklanınca derse öğretmen atanıyor yada dersin öğretmeni güncelleniyor.
        {
            try
            {
                if(textBox4.Text == "")
                {
                    MessageBox.Show("Lütfen bütün kutuları doldurunuz.");
                }
                else
                {
                    SqlCommand komut = new SqlCommand("Update Tbl_Ders Set OgretmenID = @g1, DersiVeren = @g2 where DersID = @g3", con.Baglanti());
                    komut.Parameters.AddWithValue("@g1", textBox4.Text);
                    komut.Parameters.AddWithValue("@g2", ogrtmnIsim2);
                    komut.Parameters.AddWithValue("@g3", dersID);
                    komut.ExecuteNonQuery();
                    con.Baglanti().Close();

                    MessageBox.Show("Ders veren öğretmen güncellendi.");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }


        }
    }
}
