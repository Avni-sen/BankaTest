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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source = DESKTOP-704QU67;Initial Catalog=DbBankaTest;Integrated Security = True");


        private void LnkKayit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from tbl_kisiler where hesapno=@p1 and sifre=@p2" , baglanti);
            komut.Parameters.AddWithValue("@p1", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 fr2 = new Form2();
                fr2.hesap = TxtHesapNo.Text;
                fr2.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Bilgileri Kullandınız.");
            }
            baglanti.Close();

        }
    }
}
