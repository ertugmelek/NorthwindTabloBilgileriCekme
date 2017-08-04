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
    public partial class Categories : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection("server=.;Database=NORTHWND;Integrated Security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("Select CategoryID,CategoryName from Categories", cnn);

                if (cnn.State == ConnectionState.Closed) /*eğer veritabanım kapalıysa aç dicez */
                {
                    cnn.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader(); /*gelen veriler birden çok satırlı ve sütunlu olcağı için atabileceğimiz sınıf */
                if (rd.HasRows)
                {
                    drpKategoriAdlari.DataSource = rd; /*aldığımız verileri datasource a attık */
                    drpKategoriAdlari.DataTextField = "CategoryName"; /*hangi ögelerin görüneceğini belirtiyoruz */
                    drpKategoriAdlari.DataValueField = "CategoryID"; /*ID olarak saklıcak */
                    drpKategoriAdlari.DataBind();  /*belirttiğimiz görünecek ögeleri belirtilen yere atar */
                }
                rd.Close();
                cnn.Close();
                TextBoxaYaz();
            }
        }

        private void TextBoxaYaz()
        {
            txtAciklama.Text = "";
            SqlCommand cmd = new SqlCommand("Select Description from Categories where CategoryID=@CategoryName", cnn);
            cmd.Parameters.AddWithValue("@CategoryName", drpKategoriAdlari.SelectedValue);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                txtAciklama.Text = rd["Description"].ToString();
            }
            rd.Close();
            cnn.Close();
        }

        protected void drpKategoriAdlari_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxaYaz();
        }

        protected void btnDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Categories set Description=@Aciklama where CategoryID=@CategoryName",cnn);
            cmd.Parameters.AddWithValue("@CategoryName", drpKategoriAdlari.SelectedValue);
            cmd.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);
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