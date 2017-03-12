<%@ Page Title="" Language="C#" MasterPageFile="~/NeYesekBootstrap.Master" AutoEventWireup="true" CodeBehind="Votes.aspx.cs" Inherits="NeYesekApp.Votes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/Votes.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <div class="container">
        <div class="row">
            <div class="well">
                <h1 class="text-center">Vote for your restaurant</h1>
                <div class="list-group">
                    <asp:Repeater ID="rptVotes" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="list-group-item">
                                <div class="media col-md-3">
                                    <figure class="pull-left">
                                        <img class="media-object img-rounded img-responsive" src="<%# "/Data/" + Eval("PictureUrl") %>" alt="">
                                    </figure>
                                </div>
                                <div class="col-md-6">
                                    <h4 class="list-group-item-heading"></h4>
                                    <p class="list-group-item-text">
                                        Qui diam libris ei, vidisse incorrupte at mel. His euismod salutandi dissentiunt eu. Habeo offendit ea mea. Nostro blandit sea ea, viris timeam molestiae an has. At nisl platonem eum. 
                        Vel et nonumy gubergren, ad has tota facilis probatus. Ea legere legimus tibique cum, sale tantas vim ea, eu vivendo expetendis vim. Voluptua vituperatoribus et mel, ius no elitr deserunt mediocrem. Mea facilisi torquatos ad.
                                    </p>
                                </div>
                                <div class="col-md-3 text-center">
                                    <h2><%# Eval("Votes.Count")%> <small>votes </small></h2>
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <button type="button" id="decreaseButton" class="btn btn-danger">-</button>
                                        </span>
                                        <input type="text" class="form-control" id="vote" placeholder="Vote" />
                                        <span class="input-group-btn">
                                            <button type="button" id="increaseButton" class="btn btn-success">+</button>
                                        </span>

                                    </div>
                                    <button type="button" class="btn btn-default btn-lg btn-block">Vote Now! </button>
                                    <div class="stars">
                                        <span class="glyphicon glyphicon-star"></span>
                                        <span class="glyphicon glyphicon-star"></span>
                                        <span class="glyphicon glyphicon-star"></span>
                                        <span class="glyphicon glyphicon-star"></span>
                                        <span class="glyphicon glyphicon-star-empty"></span>
                                    </div>
                                    <p>Average <%# Eval("Score")%><small>/ </small>100 </p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

    <script>
        $("#vote").val('0');
        // Create a click handler for your increment button
        $("#increaseButton").click(function () {
            var newValue = 1 + parseInt($("#vote").val());
            if (newValue > 100)
                newValue = 100;
            $("#vote").val(newValue);
        });
        // .. and your decrement button
        $("#decreaseButton").click(function () {
            var newValue = parseInt($("#vote").val()) - 1;
            if (newValue < 0)
                newValue = 0;
            $("#vote").val(newValue);
        });
    </script>
</asp:Content>
