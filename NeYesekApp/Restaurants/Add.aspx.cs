using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
            if (!this.IsPostBack)
            {

            }

            if (Session["IsLoggedIn"] == null || Session["IsLoggedIn"] is bool == false)
            {
                Response.Redirect("/Default.aspx");
            }
        }

        protected void add_button_Click(object sender, EventArgs e)
        {
            if (restaurant_picture.PostedFile == null || restaurant_picture.PostedFile.ContentLength <= 0)
            {
                var message = string.Format("Please select a file to upload.");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                return;
            }

            if (restaurant_name.Text == null || restaurant_name.Text == String.Empty)
            {
                var message = string.Format("Restaurant name cannot be empty!.");
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

            using (var ctx = new NeYesekAppContext())
            {

                var restaurant = ctx.Restaurants.Add(new Restaurant()
                {
                    Name = restaurant_name.Text,
                    IsOpen = restaurant_isopen.Checked,
                    IsValidForWalking = restaurant_iswalking.Checked,
                    Score = 0.0
                });

                ctx.SaveChanges();

                string fn = "" + restaurant.Id;
                string SaveLocation = Server.MapPath("~/Data");

                if (!System.IO.Directory.Exists(SaveLocation))
                {
                    System.IO.Directory.CreateDirectory(SaveLocation);
                }

                System.Drawing.Image bm = System.Drawing.Image.FromStream(restaurant_picture.PostedFile.InputStream);
                bm = ResizeBitmap((Bitmap)bm, 290, 300); /// new width, height
                bm.Save(Path.Combine(SaveLocation, fn + ext));

                restaurant.PictureUrl = fn + ext;

                ctx.SaveChanges();
            }

        }

        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }
    }
}