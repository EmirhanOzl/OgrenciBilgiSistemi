using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;

namespace OgrenciBilgiSistemi
{
    internal class Baglan
    {
        public SqlConnection Baglanti()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-G8QPFMR;Initial Catalog=OBS;Integrated Security=True");
            con.Open();
            return con;

        }
    }
}
