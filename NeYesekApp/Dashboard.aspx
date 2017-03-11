<%@ Page Title="" Language="C#" MasterPageFile="~/NeYesekBootstrap.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="NeYesekApp.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://oap.accuweather.com/launch.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <a href="https://www.accuweather.com/en/tr/istanbul/318251/weather-forecast/318251" class="aw-widget-legal"></a>
    <div id="awcc1489251032289" class="aw-widget-current" data-locationkey="" data-unit="c" data-language="en-us" data-useip="true" data-uid="awcc1489251032289"></div>
</asp:Content>
