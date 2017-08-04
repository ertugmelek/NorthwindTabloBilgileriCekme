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
    public partial class Customers : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection("server=.;Database=NORTHWND;Integrated Security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("Select CustomerID,CompanyName from Customers", cnn);

                if (cnn.State == ConnectionState.Closed) /*eğer veritabanım kapalıysa aç dicez */
                {
                    cnn.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader(); /*gelen veriler birden çok satırlı ve sütunlu olcağı için atabileceğimiz sınıf */
                if (rd.HasRows)
                {
                    drpSirketAdlari.DataSource = rd;/*aldığımız verileri datasource a attık */
                    drpSirketAdlari.DataTextField = "CompanyName";/*hangi ögelerin görüneceğini belirtiyoruz */
                    drpSirketAdlari.DataValueField = "CustomerID";/*ID olarak saklıcak */
                    drpSirketAdlari.DataBind();/*belirttiğimiz görünecek ögeleri belirtilen yere atar */
                }
                rd.Close();
                cnn.Close();
                TextBoxaYaz();
            }
        }
        private void TextBoxaYaz()
        {
            txtSehir.Text = "";
            SqlCommand cmd = new SqlCommand("Select City from Customers where CustomerID=@CompanyName", cnn);
            cmd.Parameters.AddWithValue("@CompanyName", drpSirketAdlari.SelectedValue);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                txtSehir.Text = rd["City"].ToString();
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
            SqlCommand cmd = new SqlCommand("Update Customers set CompanyName=@SirketAdi where CustomerID=@MusteriNo",cnn);
            cmd.Parameters.AddWithValue("@MusteriNo", drpSirketAdlari.SelectedValue);
            cmd.Parameters.AddWithValue("@SirketAdi", txtSehir.Text);
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