﻿using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NeYesekApp
{
    public partial class Default : System.Web.UI.Page
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

            /*using (var db = new NeYesekAppContext()) {

        public string GetUserIP()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0].Split(':')[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void email_send_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(sender_address.Text) || string.IsNullOrEmpty(receiver_address.Text) || !IsValidEmail(sender_address.Text) || !IsValidEmail(receiver_address.Text))
            {
                var message = string.Format("Please indicate sender and receiver email addresses.");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error!", "<script>alert('" + message + "');</script>");
                return;
            }

            SendEmail(sender_address.Text, receiver_address.Text,"deneme", "Seni cok seviyorum ben askimcim :)");
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void SendEmail(string sender, string receiver, string subject, string body)
        {
            MailMessage message = new MailMessage(new MailAddress(sender), new MailAddress(receiver));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.Body += Environment.NewLine;
            message.Body += Environment.NewLine;
            message.Body += sender;

            var client = new SmtpClient();
            client.EnableSsl = true;

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {

            }
        }
    }
}