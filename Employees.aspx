<%@ Page Title="" Language="C#" MasterPageFile="~/sitemMaster.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="_20170512_Odev.Employees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Govde" runat="server">
    <div style="margin-right:40px">
<div class="form-group ">
        <div class="input-group " style="width:152px">
            <span class="input-group-addon">Adı:</span>
            <asp:DropDownList ID="drpCalisanAdlari" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCalisanAdlari_SelectedIndexChanged"></asp:DropDownList>
        </div>
        </div>
        <div class="form-group ">
        <div class="input-group" style="margin-top:30px">
            <span class="input-group-addon";>Soyadı:</span>
            <asp:TextBox ID="txtSoyadi" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
            </div>
        <div class="form-group">
        <div class="input-group" style="margin-top:30px">
            <span class="input-group-addon";>Sistem kullanıcı adı:</span>
            <asp:TextBox ID="txtSistemKullanici" runat="server" CssClass="form-control"></asp:TextBox>
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

