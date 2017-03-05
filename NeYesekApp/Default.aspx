<%@ Page Title="" Language="C#" MasterPageFile="~/NeYesekBootstrap.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NeYesekApp.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <br/>
    <br/>
    <br/>
    <asp:TextBox runat="server" placeholder="Sender Email" ID="sender_address" TextMode="Email" />
    <asp:TextBox runat="server" placeholder="Receiver Email" ID="receiver_address" TextMode="Email" />
    <asp:Button CssClass="btn btn-default" runat="server" Text="Send Email" ID="email_send" OnClick="email_send_Click"/>
</asp:Content>
