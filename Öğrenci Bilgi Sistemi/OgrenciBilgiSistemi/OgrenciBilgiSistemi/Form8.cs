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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            label2.AutoSize = false;

            label2.Width = 933;

            label2.Height = 2;

            label2.BorderStyle = BorderStyle.Fixed3D;

            label21.AutoSize = false;

            label21.Width = 933;

            label21.Height = 2;

            label21.BorderStyle = BorderStyle.Fixed3D;


            label1.Text = Form7.ogretmenIsim + " " + Form7.ogretmenSoyisim;
            label5.Text = Form7.ogretmenKimlikNo;
            label9.Text = Form7.ogretmenFakulte;
            label7.Text = Form7.ogretmenBolum;
            label11.Text = Form7.ogretmenSehir;
            label17.Text = Form7.ogretmenMail;
            label19.Text = Form7.ogretmenTel;
            textBox1.Text = Form7.ogretmenDgmTrhi;
        }
    }
}
