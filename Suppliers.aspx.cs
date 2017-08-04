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
    public partial class Suppliers : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection("server=.;Database=NORTHWND;Integrated Security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("Select SupplierID,CompanyName from Suppliers", cnn);

                if (cnn.State == ConnectionState.Closed) /*eğer veritabanım kapalıysa aç dicez */
                {
                    cnn.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader(); /*gelen veriler birden çok satırlı ve sütunlu olcağı için atabileceğimiz sınıf */
                if (rd.HasRows)
                {
                    drpSirketAdlari.DataSource = rd; /*aldığımız verileri datasource a attık */
                    drpSirketAdlari.DataTextField = "CompanyName"; /*hangi ögelerin görüneceğini belirtiyoruz */
                    drpSirketAdlari.DataValueField = "SupplierID"; /*ID olarak saklıcak */
                    drpSirketAdlari.DataBind();  /*belirttiğimiz görünecek ögeleri belirtilen yere atar */
                }
                rd.Close();
                cnn.Close();
                TextBoxaYaz();
            }
        }
        private void TextBoxaYaz()
        {
            txtIletisimAdi.Text = "";
            SqlCommand cmd = new SqlCommand("Select CompanyName,ContactName from Suppliers where SupplierID=@tedarikci", cnn);
            cmd.Parameters.AddWithValue("@tedarikci", drpSirketAdlari.SelectedValue);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                txtIletisimAdi.Text = rd["ContactName"].ToString();
            }
            rd.Close();
            cnn.Close();
        }

        protected void drpSirketAdlari_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxaYaz();
        }

        protected void btnDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Suppliers set ContactName=@IletisimAdi where SupplierID=@tedarikci", cnn);
            cmd.Parameters.AddWithValue("@tedarikci", drpSirketAdlari.SelectedValue);
            cmd.Parameters.AddWithValue("@IletisimAdi", txtIletisimAdi.Text);
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