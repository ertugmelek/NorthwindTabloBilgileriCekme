<%@ Page Title="" Language="C#" MasterPageFile="~/sitemMaster.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="_20170512_Odev.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Govde" runat="server">
    <div style="margin-right:40px">
<div class="form-group">
        <div class="input-group col-xs-12">
            <span class="input-group-addon" style="width:111px">Sipariş ID:</span>
            <asp:DropDownList ID="drpSiparisIdleri" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpSiparisIdleri_SelectedIndexChanged"></asp:DropDownList>
        </div>
        </div>
        <div class="form-group">
        <div class="input-group col-xs-12" style="margin-top:30px">
            <span class="input-group-addon";>Sipariş Tarihi:</span>
            <asp:TextBox ID="txtSiparisTarihi" runat="server" CssClass="form-control"></asp:TextBox>
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
