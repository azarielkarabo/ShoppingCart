using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace ShoppingCart.Services
{
    public class MessageService
    {
        public async static Task SendEmailAsync(Contact contact)
        {
            try
            {
                var _email = ConfigurationManager.AppSettings["UserName"];
                var _password = ConfigurationManager.AppSettings["EmailPassword"];
                var _displayName = "Food Order App Consultation";

                string body = GetBody(contact);


                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(_email);
                mailMessage.From = new MailAddress(contact.Email, _displayName);
                mailMessage.Subject = contact.Subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(_email, _password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    smtp.Send(mailMessage);
                }

            }
            catch (SmtpException ex)
            {
                throw ex;
            }

        }
        public static string EmailTemplate(string template)
        {
            var templatePath = HostingEnvironment.MapPath("~/Content/Template/") + template + ".cshtml";
            string body = null;
            using (StreamReader reader = new StreamReader(templatePath))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }
        public static string GetBody(Contact contact)
        {
            string message = EmailTemplate("Acknoledgement");

            message = message.Replace("@Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(contact.FirstName));
            message = message.Replace("@LastName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(contact.LastName));
            message = message.Replace("@CellPhone", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(contact.CellNumber));
            message = message.Replace("@Email", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(contact.Email.ToLower()));
            message = message.Replace("@Message", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(contact.Message));


            return message;
        }

    }

}