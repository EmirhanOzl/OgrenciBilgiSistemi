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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            label2.AutoSize = false;

            label2.Width = 933;

            label2.Height = 2;

            label2.BorderStyle = BorderStyle.Fixed3D;

            label21.AutoSize = false;

            label21.Width = 933;

            label21.Height = 2;

            label21.BorderStyle = BorderStyle.Fixed3D;


            label1.Text = Form11.ogretmenIsim + " " + Form11.ogretmenSoyisim;
            label5.Text = Form11.ogretmenKimlikNo;
            label9.Text = Form11.ogretmenFakulte;
            label7.Text = Form11.ogretmenBolum;
            label11.Text = Form11.ogretmenSehir;
            label17.Text = Form11.ogretmenMail;
            label19.Text = Form11.ogretmenTel;
            textBox1.Text = Form11.ogretmenDgmTrhi;
        }
    }
}
