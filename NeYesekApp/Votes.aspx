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
                    <asp:Repeater ID="rptVotes" runat="server" OnItemCommand="rptVotes_ItemCommand" OnItemDataBound="rptVotes_ItemDataBound">
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
                                    <h4 class="list-group-item-heading"><%# Eval("Name")%></h4>
                                    <p class="list-group-item-text">
                                        Qui diam libris ei, vidisse incorrupte at mel. His euismod salutandi dissentiunt eu. Habeo offendit ea mea. Nostro blandit sea ea, viris timeam molestiae an has. At nisl platonem eum. 
                        Vel et nonumy gubergren, ad has tota facilis probatus. Ea legere legimus tibique cum, sale tantas vim ea, eu vivendo expetendis vim. Voluptua vituperatoribus et mel, ius no elitr deserunt mediocrem. Mea facilisi torquatos ad.
                                    </p>
                                </div>
                                <div class="col-md-3 text-center">
                                    <h2><%# Eval("Votes.Count")%> <small>votes </small></h2>
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <button type="button" class="btn btn-danger decreaseButton">-</button>
                                        </span>
                                        <asp:Label ID="RestaurantID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                        <asp:TextBox ID="Vote" runat="server" CssClass="form-control vote" Style="text-align: center" />
                                        <span class="input-group-btn">
                                            <button type="button" class="btn btn-success increaseButton">+</button>
                                        </span>
                                    </div>
                                    <%if (global_asax.IsVotingEnabled)
                                        { %>
                                        <asp:Button ID="SaveVote" class="btn btn-default btn-lg btn-block" CommandName="Save" runat="server" Text="Vote" />
                                    <% } %>
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
        // Create a click handler for your increment button
        $(".increaseButton").click(function (event) {
            var value = parseInt($(event.target).parent().parent().find(".form-control.vote").val());
            if (isNaN(value))
                value = 0;
            var newValue = value + 1;
            if (newValue > 100)
                newValue = 100;
            $(event.target).parent().parent().find(".form-control.vote").val(newValue);
        });
        // .. and your decrement button
        $(".decreaseButton").click(function () {
            var value = parseInt($(event.target).parent().parent().find(".form-control.vote").val());
            if (isNaN(value))
                value = 0;
            var newValue = value - 1;
            if (newValue < 0)
                newValue = 0;
            $(event.target).parent().parent().find(".form-control.vote").val(newValue);
        });
    </script>
</asp:Content>
