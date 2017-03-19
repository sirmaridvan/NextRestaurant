<%@ Page Title="" Language="C#" MasterPageFile="~/NeYesekBootstrap.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NeYesekApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="container">
            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                <!-- Wrapper for slides -->
                <div class="carousel-inner">
                    <div class="item active">
                        <img src="http://placehold.it/1200x400/16a085/ffffff&text=NeYesek">
                        <div class="carousel-caption">
                            <h3>Every Day, A New Place!</h3>
                            <p>
                                Are you bored with your monotonous lunchs? Try NeYesek for free!
                            </p>
                        </div>
                    </div>
                    <!-- End Item -->
                    <div class="item">
                        <img src="http://placehold.it/1200x400/e67e22/ffffff&text=Who Are We">
                        <div class="carousel-caption">
                            <h3>Restaurants</h3>
                            <p>
                                With its large set of restaurants, NeYesek helps you to try a new place every day!
                                Also it is free!
                            </p>
                        </div>
                    </div>
                    <!-- End Item -->
                    <div class="item">
                        <img src="http://placehold.it/1200x400/2980b9/ffffff&text=Try Our New Algorithm!">
                        <div class="carousel-caption">
                            <h3>Best Algorithm Ever</h3>
                            <p>
                                NeYesek guarantees for you to select best restaurants for you! Also it is free!
                            </p>
                        </div>
                    </div>
                    <!-- End Item -->
                    <!--<div class="item">
                        <img src="http://placehold.it/1200x400/8e44ad/ffffff&text=Services">
                        <div class="carousel-caption">
                            <h3>Headline</h3>
                            <p>
                                Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod
                        tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. Lorem
                        ipsum dolor sit amet, consetetur sadipscing elitr.
                            </p>
                        </div>
                    </div>-->
                    <!-- End Item -->
                </div>
                <!-- End Carousel Inner -->
                <ul class="nav nav-pills nav-justified">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"><a href="#"><small>About</small></a></li>
                    <li data-target="#myCarousel" data-slide-to="1"><a href="#"><small>Restuarants</small></a></li>
                    <li data-target="#myCarousel" data-slide-to="2"><a href="#"><small>Best Algorithm Ever</small></a></li>
                    <!--<li data-target="#myCarousel" data-slide-to="3"><a href="#">Services<small>Lorem ipsum
                dolor sit</small></a></li>-->
                </ul>
            </div>
            <!-- End Carousel -->
        </div>
    </div>


    <div class="container">
        <div class="jumbotron">
            <h1>Do you care?</h1>
            <p><a href="/Register.aspx">Sign Up for Free!</a></p>
        </div>
    </div>

    <div class="page-header">
        <h1>How Its Works</h1>
    </div>

    <div class="container">
        <div class="row pricing">
            <div class="col-md-6">
                <div class="well">
                    <h3><b>Looking For RESTAURANTS?</b></h3>
                    <hr>
                    <p><b>Create a user account. It's easy and free.</b></p>
                    <hr>
                    <p><b>Choose your habits and district</b></p>
                    <hr>
                    <p><b>Tell us the tastes you’re looking for.</b></p>
                    <hr>
                    <p><b>Tell us if you have a car.</b></p>
                    <hr>
                    <p><b>Only pay when you go to the restaurant. No more waste.</b></p>
                    <hr>
                    <a href="/Register.aspx" class="btn btn-default btn-block">Sign up</a>
                </div>
            </div>
            <div class="col-md-6">
                <div class="well">
                    <h3><b>Looking For CUSTOMERS?</b></h3>
                    <hr>
                    <p><b>Create your restaurant account. It’s easy.</b></p>
                    <hr>
                    <p><b>Join the millions of Restaurant Seekers using NeYesek.</b></p>
                    <hr>
                    <p><b>Tell us about your company.</b></p>
                    <hr>
                    <p><b>Tell us how you want to learn about new customers.</b></p>
                    <hr>
                    <p><b>One Click Apply to restaurant offers anytime. Anywhere.</b></p>
                    <hr>
                    <a href="#" class="btn btn-default btn-block">Join Now!</a>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#myCarousel').carousel({
                interval: 4000
            });

            var clickEvent = false;
            $('#myCarousel').on('click', '.nav a', function () {
                clickEvent = true;
                $('.nav li').removeClass('active');
                $(this).parent().addClass('active');
            }).on('slid.bs.carousel', function (e) {
                if (!clickEvent) {
                    var count = $('.nav').children().length - 1;
                    var current = $('.nav li.active');
                    current.removeClass('active').next().addClass('active');
                    var id = parseInt(current.data('slide-to'));
                    if (count == id) {
                        $('.nav li').first().addClass('active');
                    }
                }
                clickEvent = false;
            });
        });
    </script>
</asp:Content>
