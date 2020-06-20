using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThaiSpaceApi.Models;

namespace ThaiSpaceApi.Services
{
    public class EmailSender : IEmailSender
    {

        public EmailSender()
        {
        }
        
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(key);
            var message = new SendGridMessage
            {
                From = new EmailAddress("moutainqueen@gmail.com", "moutainqueen@gmail.com"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);

            return client.SendEmailAsync(message);
        }
    }
}
