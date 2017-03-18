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
                    TextBox score = e.Item.FindControl("vote") as TextBox;
                    Label labelID = e.Item.FindControl("RestaurantID") as Label;
                    int id = 0;
                    Int32.TryParse(labelID.Text.ToString(), out id);
                    double vote = 0;
                    Double.TryParse(score.Text.ToString(), out vote);
                    using (var ctx = new NeYesekAppContext())
                    {
                        try
                        {
                            var userVote = new UserVote()
                            {
                                UserId = Convert.ToInt32(Session["Id"]),
                                RestaurantId = id,
                                Vote = vote,
                            };
                            ctx.UserVotes.Add(userVote);
                            ctx.SaveChanges();
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
    }
}