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
    public partial class Form9 : Form
    {
        Baglan con = new Baglan();
        public Form9()
        {
            InitializeComponent();
            
        }

        private void veriGetir(DataGridView dgv, int donem)
        {
            SqlCommand komut = new SqlCommand("Select [DersKodu] AS [D.KOD.], [DersAdi] AS [DERSIN ADI], [DersiVeren] AS [DERSI VEREN], [DersKredi] AS [AKTS] " +
                "from Tbl_Ders inner join Tbl_Ogretmen on Tbl_Ders.OgretmenID=Tbl_Ogretmen.OgretmenID  where Tbl_Ogretmen.OgretmenID = " + Form1.ogretmenID + "and Donem = " + donem, con.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = komut;

            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dgv.DataSource = tablo;
            dgv.Columns[0].Width = 70;
            dgv.Columns[1].Width = 195;
            dgv.Columns[2].Width = 140;
            dgv.Columns[3].Width = 82;

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            veriGetir(dataGridView1, 1);
            veriGetir(dataGridView2, 2);
            veriGetir(dataGridView3, 3);
            veriGetir(dataGridView4, 4);
            veriGetir(dataGridView5, 5);
            veriGetir(dataGridView6, 6);
            veriGetir(dataGridView7, 7);
            veriGetir(dataGridView8, 8);
        }
    }
}
