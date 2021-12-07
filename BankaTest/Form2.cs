using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string hesap;
        SqlConnection baglanti = new SqlConnection(@"Data Source = DESKTOP-704QU67;Initial Catalog=DbBankaTest;Integrated Security = True");


        private void Form2_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = hesap;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from tbl_kisiler where hesapno=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", hesap);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
                LblTc.Text = dr[3].ToString();
                LblTelefon.Text = dr[4].ToString();
                
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Gönderilen Hesabın para artışı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tbl_hesap set Bakiye=Bakiye+@p1 where HesapNo=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1",decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@p2",TxtHesapNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();


            //Gonderen hesaptan para düşmesilazım 
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Update tbl_hesap set Bakiye=Bakiye-@k1 where HesapnNo=@k2", baglanti);
            komut.Parameters.AddWithValue("@k1", decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@k2", hesap);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Havale Gerçekleşti.Mebla Hesabınızdan Düşüldü.");
            
        }
    }
}
