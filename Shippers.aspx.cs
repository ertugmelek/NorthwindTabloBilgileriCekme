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
    public partial class Shippers : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection("server=.;Database=NORTHWND;integrated security=true"); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                SqlCommand cmd = new SqlCommand("Select ShipperID,CompanyName from Shippers",cnn);
                if (cnn.State == ConnectionState.Closed) 
                {
                    cnn.Open();
                }
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows) 
                { 
                drpSirketAdlari.DataSource = rd;
                drpSirketAdlari.DataTextField = "CompanyName";
                drpSirketAdlari.DataValueField = "ShipperID";
                drpSirketAdlari.DataBind();
                }
                rd.Close();
                cnn.Close();
                TextBoxaYaz();
            }
        }

        private void TextBoxaYaz()
        {
            txtTelefonNumarasi.Text = "";
            SqlCommand cmd = new SqlCommand("Select CompanyName,Phone from Shippers where ShipperID=@CompanyName", cnn);
            cmd.Parameters.AddWithValue("@CompanyName", drpSirketAdlari.SelectedValue);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                txtTelefonNumarasi.Text = rd["Phone"].ToString();
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
            SqlCommand cmd = new SqlCommand("Update Shippers set Phone=@telefon where ShipperID=@CompanyName", cnn);
            cmd.Parameters.AddWithValue("@CompanyName", drpSirketAdlari.SelectedValue);
            cmd.Parameters.AddWithValue("@telefon", txtTelefonNumarasi.Text);
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