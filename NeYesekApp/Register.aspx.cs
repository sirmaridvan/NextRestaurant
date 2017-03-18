using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register_button_Click(object sender, EventArgs e)
        {

            using (var ctx = new NeYesekAppContext())
            {
                if(register_password.Text != register_password_again.Text)
                {
                    var message = "Passwords do not match!";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                    return;
                }
                var returnedUser = ctx.Users.Where(x => x.Email == register_email.Text).SingleOrDefault();
                if (returnedUser != null)
                {
                    var message = string.Format("User with email {0} already exists!", register_email.Text);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message+ "');</script>");
                    return;
                }

                var salt = Guid.NewGuid().ToString();

                var user = new User()
                {
                    Name = register_name.Text,
                    Email = register_email.Text,
                    Salt = salt,
                    Hash = General.GetHashString(register_password.Text + salt)
                };

                ctx.Users.Add(user);

                ctx.SaveChanges();

                Response.Redirect("/Login.aspx");
            }

        }
    }
}