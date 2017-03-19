<%@ Page Title="" Language="C#" MasterPageFile="~/NeYesekBootstrap.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NeYesekApp.Restaurants" %>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">Restaurants</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/Restaurants.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:HyperLink CssClass="btn btn-info pull-right" Text="Add Restaurant" runat="server" NavigateUrl="Add.aspx" />

    <div class="row">
        <div class="main_portfolio_content">
            <asp:Repeater ID="rptRestaurants" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-md-3 col-sm-4 col-xs-12 single_portfolio_text">
                        <img src="<%# "/Data/" + Eval("PictureUrl") %>" alt="" />
                        <div class="portfolio_images_overlay text-center">
                            <h6><%# Eval("Name")%></h6>
                            <p class="product_price"><%# Eval("Score")%></p>
                            <a href="<%# "/Restaurants/" + Eval("Id") %>" class="btn btn-primary">Details</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
