<%@ Page Title="" Language="C#" MasterPageFile="~/NeYesekBootstrap.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="NeYesekApp.RestaurantDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $(document).mousemove(function (e) {
                TweenLite.to($('body'),
                   .5,
                   {
                       css:
                         {
                             backgroundPosition: "" + parseInt(event.pageX / 8) + "px " + parseInt(event.pageY / '12') + "px, " + parseInt(event.pageX / '15') + "px " + parseInt(event.pageY / '15') + "px, " + parseInt(event.pageX / '30') + "px " + parseInt(event.pageY / '30') + "px"
                         }
                   });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div class="row vertical-offset-100">
        <div class="col-md-4 col-md-offset-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Add Restaurant</h3>
                </div>
                <div class="panel-body">
                    <fieldset>
                        <div class="form-group">
                            <asp:TextBox class="form-control" placeholder="Name of Restaurant" name="name" TextMode="SingleLine" runat="server" ID="restaurant_name" />
                        </div>
                        <div class="form-group">
                            <asp:FileUpload class="form-control" placeholder="Restaurant Placeholder" name="picture" value="" runat="server" ID="restaurant_picture" />
                        </div>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox name="isOpen" runat="server" type="checkbox" ID="restaurant_isopen" />
                                Is the restaurant active currently?
                            </label>
                        </div>

                        <div class="checkbox">
                            <label>
                                <asp:CheckBox name="walking" runat="server" type="checkbox" ID="restaurant_iswalking" />
                                Is the restaurant available for walking?
                            </label>
                        </div>
                        <asp:Button class="btn btn-lg btn-success btn-block" type="submit" Text="Update" runat="server" ID="update_button" OnClick="update_button_Click" />
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
