<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="NeYesekApp.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NeYesek - Register</title>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/Register.css" rel="stylesheet" type="text/css" />
    
    <link rel="shortcut icon" type="image/x-icon" href="/icon.png" />
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row vertical-offset-100">
                <div class="col-md-4 col-md-offset-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Sign Up</h3>
                        </div>
                        <div class="panel-body">
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="E-mail" name="email" TextMode="Email" runat="server" ID="register_email" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Name" name="name" TextMode="SingleLine" runat="server" ID="register_name" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" TextMode="Password" placeholder="Password" name="password" value="" runat="server" ID="register_password" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" TextMode="Password" placeholder="Password" name="password" value="" runat="server" ID="register_password_again" />
                                </div>
                                <asp:Button class="btn btn-lg btn-success btn-block" type="submit" Text="Register" runat="server" ID="register_button" OnClick="register_button_Click" />
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
