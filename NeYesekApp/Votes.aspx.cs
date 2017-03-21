using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Votes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["IsLoggedIn"] == null || Session["IsLoggedIn"] is bool == false)
                {
                    Response.Redirect("/Default.aspx");
                    return;
                }

                using (var ctx = new NeYesekAppContext())
                {
                    rptVotes.DataSource = ctx.Restaurants.ToList();
                    rptVotes.DataBind();
                }

            }
        }
        protected void rptVotes_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "Save":
                    if(!Global.IsVotingEnabled)
                    {
                        var message = string.Format("Voting is not enabled right now!");
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                        return;
                    }

                    TextBox voteTextBox = e.Item.FindControl("Vote") as TextBox;
                    Label labelId = e.Item.FindControl("RestaurantID") as Label;
                    int restaurantId = 0;
                    Int32.TryParse(labelId.Text, out restaurantId);
                    double vote = 0;
                    Double.TryParse(voteTextBox.Text, out vote);

                    using (var ctx = new NeYesekAppContext())
                    {
                        try
                        {
                            bool isUpdate = true;
                            var userId = (int)Session["UserId"];
                            var userVote = ctx.UserVotes.Where(v => v.Restaurant.Id == restaurantId && v.User.Id == userId).SingleOrDefault();

                            var user = ctx.Users.Where(u => u.Id == userId).SingleOrDefault();

                            var restaurant = ctx.Restaurants.Include("ScheduleInformation").Where(r => r.Id == restaurantId).SingleOrDefault();

                            if(userVote == null)
                            {
                                isUpdate = false;
                                userVote = new UserVote()
                                {
                                    User = user,
                                    Restaurant = restaurant,
                                    Vote = vote,
                                };
                            }

                            var voteSum = user.Votes.Sum(v => v.Vote);

                            if(isUpdate)
                            {
                                if (voteSum - userVote.Vote + vote > 100)
                                {
                                    var message = string.Format("You voted too much! Vote total should be at most 100! Yours is {0}", (voteSum - userVote.Vote + vote));
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                                    return;
                                }
                                
                                var score = restaurant.Score;

                                score = score * (restaurant.Votes.Count);

                                score = score - userVote.Vote + vote;

                                score = score / (restaurant.Votes.Count);

                                restaurant.Score = score;

                                userVote.Vote = vote;

                            } else
                            {
                                if (voteSum + vote > 100)
                                {
                                    var message = string.Format("You voted too much! Vote total should be at most 100! Yours is {0}", (voteSum + vote));
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                                    return;
                                }

                                ctx.UserVotes.Add(userVote);
                                
                                var score = restaurant.Score;

                                score = score * (restaurant.Votes.Count - 1);

                                score = score + vote;

                                score = score / (restaurant.Votes.Count);

                                restaurant.Score = score;

                            }

                            ctx.SaveChanges();

                            rptVotes.DataSource = ctx.Restaurants.ToList();

                            rptVotes.DataBind();
                        }

                        catch (Exception ec)
                        {
                            Console.WriteLine(ec.Message);
                        }

                        
                    }
                    break;
                default:
                    break;
            }
        }

        protected void rptVotes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rest = (Restaurant) e.Item.DataItem;
                using(var ctx = new NeYesekAppContext())
                {
                    var userId = (int)Session["UserId"];
                    var vote = ctx.UserVotes.Where(v => v.User.Id == userId && v.Restaurant.Id == rest.Id).SingleOrDefault();
                    if(vote == null)
                    {
                        ((TextBox)e.Item.FindControl("Vote")).Text = "0";
                    } else
                    {
                        ((TextBox)e.Item.FindControl("Vote")).Text = vote.Vote.ToString();
                    }
                }
            }
        }
    }
}