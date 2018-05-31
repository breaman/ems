using EMS.Web.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Services
{
    public class ElasticEmailEmailSender : IEmailSender
    {
        public AppSettings AppSettings { get; }
        public ElasticEmailEmailSender(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

        // TODO: Stokes needs to make this pull in values from configuration
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(AppSettings.Email.FromName, AppSettings.Email.FromAddress));
            mimeMessage.To.Add(new MailboxAddress(email, email));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.elasticemail.com", 2525, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(AppSettings.Email.Username, AppSettings.Email.Password);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            return Task.FromResult(0);
        }
    }
}
