using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _20170512_Odev
{
    public partial class Employees : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection("server=.;Database=NORTHWND;Integrated Security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("Select EmployeeID,FirstName from Employees", cnn);

                if (cnn.State == ConnectionState.Closed) /*eğer veritabanım kapalıysa aç dicez */
                {
                    cnn.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader(); /*gelen veriler birden çok satırlı ve sütunlu olcağı için atabileceğimiz sınıf */
                if (rd.HasRows)
                {
                    drpCalisanAdlari.DataSource = rd; /*aldığımız verileri datasource a attık */
                    drpCalisanAdlari.DataTextField = "FirstName"; /*hangi ögelerin görüneceğini belirtiyoruz */
                    drpCalisanAdlari.DataValueField = "EmployeeID"; /*ID olarak saklıcak */
                    drpCalisanAdlari.DataBind();  /*belirttiğimiz görünecek ögeleri belirtilen yere atar */
                }
                rd.Close();
                cnn.Close();
                TextBoxaYaz();
            }
        }

        private void TextBoxaYaz()
        {
            txtSoyadi.Text = "";
            txtSistemKullanici.Text = "";
            SqlCommand cmd = new SqlCommand("Select LastName,SistemKullaniciAdi from Employees where EmployeeID=@FirstName", cnn);
            cmd.Parameters.AddWithValue("@FirstName", drpCalisanAdlari.SelectedValue);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                txtSoyadi.Text = rd["LastName"].ToString();
                txtSistemKullanici.Text = rd["SistemKullaniciAdi"].ToString();
            }
            rd.Close();
            cnn.Close();
        }

        protected void drpCalisanAdlari_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxaYaz();
        }

        protected void btnDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Employees set LastName=@Soyadi,SistemKullaniciAdi=@SistemKullanici where EmployeeID=@FirstName", cnn);
            cmd.Parameters.AddWithValue("@FirstName", drpCalisanAdlari.SelectedValue);
            cmd.Parameters.AddWithValue("@Soyadi", txtSoyadi.Text);
            cmd.Parameters.AddWithValue("@SistemKullanici", txtSistemKullanici.Text);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int etkilenenSatirSayisi = -1;
            try
            {
                etkilenenSatirSayisi = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
            if (etkilenenSatirSayisi < 0)
            {
                throw new Exception("Hataa");
            }
            else
            {
                lblSonuc.Visible = true;
                lblSonuc.Text = "Düzenleme işlemi gerçekleştirildi";
            }
        }
    }
}