using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace _20170512_Odev
{
    public partial class Products : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection("server=.;Database=NORTHWND;integrated security=true");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                SqlCommand cmd = new SqlCommand("Select ProductID,ProductName from Products",cnn);
                if (cnn.State == ConnectionState.Closed) 
                {
                    cnn.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows) 
                {
                    drpUrunAdlari.DataSource = rd;
                    drpUrunAdlari.DataTextField = "ProductName";
                    drpUrunAdlari.DataValueField = "ProductID";
                    drpUrunAdlari.DataBind();
                }
                rd.Close();
                cnn.Close();
                TextBoxaYaz();
            }
            

        }

        private void TextBoxaYaz()
        {
            txtUrunDevami.Text = "";
            SqlCommand cmd = new SqlCommand("Select Discontinued from Products where ProductID=@UrunAdi",cnn);
            cmd.Parameters.AddWithValue("UrunAdi", drpUrunAdlari.SelectedValue);
            if (cnn.State == ConnectionState.Closed) 
            {
                cnn.Open();
            }
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows) 
            { 
                rd.Read();
                txtUrunDevami.Text = rd["Discontinued"].ToString();
            }
            rd.Close();
            cnn.Close();
        }

        protected void drpUrunAdlari_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxaYaz();
        }

        protected void btnDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Products set Discontinued=@UrunDevami where ProductID=@UrunAdi",cnn);
            cmd.Parameters.AddWithValue("@UrunAdi", drpUrunAdlari.SelectedValue);
            cmd.Parameters.AddWithValue("@UrunDevami", txtUrunDevami.Text);
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