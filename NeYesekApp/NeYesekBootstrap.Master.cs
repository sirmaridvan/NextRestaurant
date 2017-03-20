using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class NeYesekBootstrap : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session["IsLoggedIn"] = null;
            Session["Email"] = string.Empty;
            Session["UserId"] = 0;
            Response.Redirect("Default.aspx");
        }

        protected void startVotingButton_Click(object sender, EventArgs e)
        {
            using(var ctx = new NeYesekAppContext())
            {
                ctx.UserVotes.RemoveRange(ctx.UserVotes);
                foreach (var res in ctx.Restaurants.ToList()) {
                    res.Score = 0;
                }
                ctx.SaveChanges();
                AddVotingCache();

            }
        }

        void AddVotingCache()
        {

            var OnCacheRemove = new CacheItemRemovedCallback(Global.CacheItemRemoved);
            HttpRuntime.Cache.Insert(Global.VOTING, 1, null,
                    DateTime.Today.AddDays(1).AddHours(12), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
            Global.IsVotingEnabled = true;
            var message = string.Format("Voting is enabled until tomorrow noon!");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Success!", "<script>alert('" + message + "');</script>");
            return;
        }
    }
}