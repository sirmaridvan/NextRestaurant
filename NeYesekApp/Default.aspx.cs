using System;

namespace NeYesekApp
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["IsLoggedIn"] != null && (Session["IsLoggedIn"] is bool) == true)
                {
                    Response.Redirect("Dashboard.aspx");
                    return;
                }


            }
        }
    }
}