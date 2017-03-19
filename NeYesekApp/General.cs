using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NeYesekApp
{
    public class General
    {
        public static void SendEmail(string sender, string receiver, string subject, string body)
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

        public static bool IsValidEmail(string email)
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

        public static string GetUserIP()
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

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

    }
}