﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Elif : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void denemeButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Button clicked!')</script>");
        }
    }
}