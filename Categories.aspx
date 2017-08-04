<%@ Page Title="" Language="C#" MasterPageFile="~/sitemMaster.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="_20170512_Odev.Categories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Govde" runat="server">
    <div style="margin-right:40px">
<div class="form-group">
        <div class="input-group">
            <span class="input-group-addon">Kategori Adı:</span>
            <asp:DropDownList ID="drpKategoriAdlari" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpKategoriAdlari_SelectedIndexChanged"></asp:DropDownList>
        </div>
        </div>
        <div class="form-group">
        <div class="input-group col-xs-12" style="margin-top:30px">
            <span class="input-group-addon" style="width:107px">Açıklama:</span>
            <asp:TextBox ID="txtAciklama" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
            </div>
    <div class="form-group">
        <div class="input-group">
            <asp:Button ID="btnDuzenle" runat="server" Text="Düzenle" style="margin-top:30px" CssClass="btn btn-primary" OnClick="btnDuzenle_Click" />
        </div>
    </div>
        <asp:Label ID="lblSonuc" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    
</asp:Content>


