<%@ Page Title="History" Language="C#" MasterPageFile="~/NeYesekBootstrap.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="NeYesekApp.History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Members.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="container bootstrap snippet">
        <div class="row">
            <div class="col-lg-12">
                <div class="main-box no-header clearfix">
                    <div class="main-box-body clearfix">
                        <div class="table-responsive">
                            <table class="table user-list">
                                <thead>
                                    <tr>
                                        <th><span>Restaurant</span></th>
                                        <th><span>Day</span></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptHistories" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("RestaurantName")%></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "DateAdded", "{0:dd/MM/yyyy}") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                 </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>