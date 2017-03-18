using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login_button_Click(object sender, EventArgs e)
        {
            using (var ctx = new NeYesekAppContext())
            {
                var user = ctx.Users.Where(x => x.Email == login_email.Text).Single();
                if(user == null)
                {
                    var message = string.Format("User with email = {0} not found", login_email.Text);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                    return;
                }

                var sha1data = General.GetHashString(login_password.Text + user.Salt);

                if(sha1data == user.Hash)
                {
                    Session["IsLoggedIn"] = true;
                    Session["Email"] = user.Email;
                    Session["UserId"] = user.Id;
                    Response.Redirect("Dashboard.aspx");
                    return;
                }

                var loginError = string.Format("Invalid credentials!");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + loginError + "');</script>");
                return;

            }
        }
    }
}