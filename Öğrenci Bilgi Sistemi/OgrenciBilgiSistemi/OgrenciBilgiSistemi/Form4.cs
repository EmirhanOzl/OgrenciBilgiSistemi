using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciBilgiSistemi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)     // Bilgileri burada topladım.
        {
            label2.AutoSize = false;

            label2.Width = 933;

            label2.Height = 2;

            label2.BorderStyle = BorderStyle.Fixed3D;

            label21.AutoSize = false;

            label21.Width = 933;

            label21.Height = 2;

            label21.BorderStyle = BorderStyle.Fixed3D;


            label1.Text = Form3.ogrenciIsim + " " + Form3.ogrenciSoyisim;
            label4.Text = Form3.ogrenciNo;
            label5.Text = Form3.ogrenciKimlikNo;
            label7.Text = Form3.ogrenciDonem;
            label9.Text = Form3.ogrenciBolum;
            label11.Text = Form3.ogrenciSehir;
            label13.Text = Form3.ogrenciFakulte;
            label15.Text = Form3.ogrenciAgno;
            label17.Text = Form3.ogrenciMail;
            label19.Text = Form3.ogrenciTel;
            textBox1.Text = Form3.ogrenciDgmTrhi;
            
        }
    }
}
