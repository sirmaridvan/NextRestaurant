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
        private NeYesekAppContext db = new NeYesekAppContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            GroupMember member = db.GroupMembers.Add(new GroupMember()
            {
                Id = 6,
                Name = "Hasan Akhasan",
                Category = "AKP",
                Price = 3.5
            });
            db.SaveChanges();
        }
    }
}