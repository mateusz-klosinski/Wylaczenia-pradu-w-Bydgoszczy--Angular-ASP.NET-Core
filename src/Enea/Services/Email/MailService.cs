using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enea.Models;

namespace Enea.Services.Email
{
    public class MailService
    {
        private IConfigurationRoot _config;

        public MailService(IConfigurationRoot config)
        {
            _config = config;
        }

        public string GenerateEmailBody(List<Disconnection> disconnections, List<string> keyWords)
        {
            string text = "Dzień dobry!" + Environment.NewLine + Environment.NewLine;

            text += "Nasza aplikacja wykryła na podstawie podanych słów kluczy, że jutro w Twojej okolicy nie będzie prądu."
                + Environment.NewLine + Environment.NewLine;

            text += "Słowa, które zostały wykryte to: ";

            foreach (var word in keyWords)
            {
                text += word + " ";
            }

            text += Environment.NewLine + Environment.NewLine;

            text += "Dokładna treść wyłączeń, w których zostały wykryte słowa klucze: " + Environment.NewLine;

            foreach (var disconnection in disconnections)
            {
                text += disconnection.Area + Environment.NewLine + disconnection.Date.ToString("dd-MM-yyy") + ", " 
                    + disconnection.Time + Environment.NewLine + disconnection.Details 
                    + Environment.NewLine + Environment.NewLine;
            }

            return text;
        }

        public MimeMessage GenerateEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();

            email.To.Add(new MailboxAddress("", to));
            email.From.Add(new MailboxAddress("Przerwy w dostawach prądu", _config["EmailCredentials:MailAddress"]));
            email.Subject = subject;
            email.Body = new TextPart
            {
                Text = body,
            };

            return email;
        }

        public void SendEmail(List<MimeMessage> emails)
        {
            using (var client = new SmtpClient())
            {

                client.Connect(_config["EmailCredentials:Server"], int.Parse(_config["EmailCredentials:Port"]));

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_config["EmailCredentials:MailAddress"], _config["EmailCredentials:Password"]);

                foreach (MimeMessage email in emails)
                {
                    client.Send(email);
                }

                client.Disconnect(true);
            }
        }
    }
}
