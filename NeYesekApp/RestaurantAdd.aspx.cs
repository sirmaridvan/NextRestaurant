using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class RestaurantAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void add_button_Click(object sender, EventArgs e)
        {
            if(restaurant_picture.PostedFile == null)
            {
                var message = string.Format("Please select a file to upload.");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                return;
            }

            string[] validFileTypes = { "png", "jpg", "jpeg" };
            string ext = System.IO.Path.GetExtension(restaurant_picture.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile)
            {
                var message = string.Format("File extension should be .png, .jpg or .jpeg");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                return;
            }



        }
    }
}