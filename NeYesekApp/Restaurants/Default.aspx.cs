using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Restaurants : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

            }

            if (Session["IsLoggedIn"] != null && Session["IsLoggedIn"] is bool == true)
            {
                Response.Redirect("Dashboard.aspx");
            }

            using (var ctx = new NeYesekAppContext())
            {
                rptRestaurants.DataSource = ctx.Restaurants.ToList();
                rptRestaurants.DataBind();
            }
        }

    }
}