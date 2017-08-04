<%@ Page Title="" Language="C#" MasterPageFile="~/sitemMaster.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="_20170512_Odev.Suppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Govde" runat="server">
    <div style="margin-right:40px">
<div class="form-group">
        <div class="input-group">
            <span class="input-group-addon">Şirket Adı:</span>
            <asp:DropDownList ID="drpSirketAdlari" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpSirketAdlari_SelectedIndexChanged"></asp:DropDownList>
        </div>
        </div>
        <div class="form-group">
        <div class="input-group" style="margin-top:30px">
            <span class="input-group-addon";>İletişim Adı:</span>
            <asp:TextBox ID="txtIletisimAdi" runat="server" CssClass="form-control"></asp:TextBox>
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