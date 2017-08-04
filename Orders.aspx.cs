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
    public partial class Orders : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection("server=.;Database=NORTHWND;Integrated Security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("Select OrderID,OrderDate from Orders", cnn);

                if (cnn.State == ConnectionState.Closed) /*eğer veritabanım kapalıysa aç dicez */
                {
                    cnn.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader(); /*gelen veriler birden çok satırlı ve sütunlu olcağı için atabileceğimiz sınıf */
                if (rd.HasRows)
                {
                    drpSiparisIdleri.DataSource = rd; /*aldığımız verileri datasource a attık */
                    drpSiparisIdleri.DataTextField = "OrderID"; /*hangi ögelerin görüneceğini belirtiyoruz */
                    drpSiparisIdleri.DataValueField = "OrderID"; /*ID olarak saklıcak */
                    drpSiparisIdleri.DataBind();  /*belirttiğimiz görünecek ögeleri belirtilen yere atar */
                }
                rd.Close();
                cnn.Close();
                TextBoxaYaz();
            }
        }

        private void TextBoxaYaz()
        {
            txtSiparisTarihi.Text = "";
            SqlCommand cmd = new SqlCommand("Select OrderID,OrderDate from Orders where OrderID=@orderId", cnn);
            cmd.Parameters.AddWithValue("@orderId", drpSiparisIdleri.SelectedValue);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                txtSiparisTarihi.Text = rd["OrderDate"].ToString();
            }
            rd.Close();
            cnn.Close();
        }

        protected void drpSiparisIdleri_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxaYaz();
        }

        protected void btnDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Orders set OrderDate=@tarih where OrderID=@siparis",cnn);
            cmd.Parameters.AddWithValue("@siparis", drpSiparisIdleri.SelectedValue);
            cmd.Parameters.AddWithValue("@tarih", txtSiparisTarihi.Text);
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