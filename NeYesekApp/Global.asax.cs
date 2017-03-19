using NeYesekApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace NeYesekApp
{
    public class Global : HttpApplication
    {

        public const String SEND_EMAIL = "SendMail";
        public const String CALCULATE_NEW_RESTAURANTS = "CalculateRestaurants";

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DoSendMailTask(10);
            DoCalculateNextRestaurantsTask();
        }

        private void DoSendMailTask(int hours)
        {
            var OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);

            if (DateTime.Now.Hour < hours)
            {
                HttpRuntime.Cache.Insert(SEND_EMAIL, hours, null,
                    DateTime.Today.AddHours(hours), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
            }
            else
            {
                HttpRuntime.Cache.Insert(SEND_EMAIL, hours, null,
                    DateTime.Today.AddDays(1).AddHours(hours), Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable, OnCacheRemove);
            }
        }

        private void DoCalculateNextRestaurantsTask()
        {

        }

        public void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            switch(k)
            {
                case SEND_EMAIL:
                    DoSendMailTask(Convert.ToInt32(v));
                    break;
                case CALCULATE_NEW_RESTAURANTS:
                    DoCalculateNextRestaurantsTask();
                    break;
            }
        }

        public void SendEmail(string subject, string body)
        {
            MailMessage message = new MailMessage();
            using (var ctx = new NeYesekAppContext())
            {
                foreach (var mail in ctx.Users.Select(x => x.Email))
                {
                    message.To.Add(new MailAddress(mail));
                }
            }
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.Body += Environment.NewLine;
            message.Body += Environment.NewLine;

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