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
    public partial class Form6 : Form
    {

        Baglan con = new Baglan();
        public Form6()
        {
            InitializeComponent();
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            label2.AutoSize = false;

            label2.Width = 1100;

            label2.Height = 2;

            label2.BorderStyle = BorderStyle.Fixed3D;

            comboBox1.Text = Form3.ogrenciDonem + ". Yarıyıl";

            veriGetir(dataGridView1, Convert.ToInt32(Form3.ogrenciDonem));

            textBox1.Text = Form3.agno.ToString();


        }
        string tblkomut;
        private void veriGetir(DataGridView dgv, int donem)             // Öğrencinin aldığı dersleri ve notlarını gösteren bir tablo oluşturdum
        {

            tblkomut = "select [DersKodu] AS [Ders Kodu], [DersAdi] AS [Ders Adı], 'Ara Sınav' AS [Sınav Türü],'40.0' AS [Etki Oranı],[Vize] AS [Not], [HarfNotu] AS [Harf Notu] " +
                "from Tbl_Ders join Tbl_Not on Tbl_Ders.DersID = Tbl_Not.DersID where OgrenciID =" + Form1.ogrenciID + "AND Donem =" + donem +
                "UNION " +
                "select [DersKodu] AS[Ders Kodu], [DersAdi] AS [Ders Adı], 'Yarıyıl Sonu' AS [Sınav Türü], '60.0' AS [Etki Oranı],[Final] AS [Not], [HarfNotu] AS [Harf Notu]" +
                "from Tbl_Ders join Tbl_Not on Tbl_Ders.DersID = Tbl_Not.DersID where OgrenciID =" + Form1.ogrenciID + "AND Donem =" + donem;

            SqlCommand komut = new SqlCommand(tblkomut, con.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = komut;

            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dgv.DataSource = tablo;
            dgv.Columns[0].Width = 100;
            dgv.Columns[1].Width = 360;
            dgv.Columns[2].Width = 287;
            dgv.Columns[3].Width = 100;
            dgv.Columns[4].Width = 55;
            dgv.Columns[5].Width = 55;
            

            for (int i = 0;i < dgv.Rows.Count;i++)
            {
                dgv.Rows[i].Height = 32;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string donem = comboBox1.Text.Split('.')[0].ToString();

            veriGetir(dataGridView1, Convert.ToInt32(donem));
        }
        
       
    }
}
