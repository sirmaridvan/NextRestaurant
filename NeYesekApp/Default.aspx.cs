using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var db = new NeYesekAppContext()) {

                var user = (from d in db.Users where d.Name == "Elif Benli" select d).Single();

                var restaurant = (from d in db.Restaurants where d.Name == "Sampi" select d).Single();

                db.UserVotes.Add(new UserVote()
                {
                    UserId = user.Id,
                    RestaurantId = restaurant.Id,
                    Vote = 6.5
                });

                db.SaveChanges();

                foreach (var vote in user.Votes)
                {
                    
                }

            }
        }
    }
}