using LmsGateway.Core.Infrastructure;
using LmsGateway.Core.Notifications;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace LmsGateway.Services.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly EmailServer _emailServer;

        public EmailService(EmailServer emailServer)
        {
            Guard.NotNull(emailServer, nameof(emailServer));

            _emailServer = emailServer;
        }

        public async Task SendEmailAsync(Email email)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(email.FromEmailAddress.Name, email.FromEmailAddress.Email));
            message.To.Add(new MailboxAddress(email.ToEmailAddress.Name, email.ToEmailAddress.Email));
            message.Body = new TextPart("html") { Text = email.Message }; //plain, html, enriched, rtf, and xml

            if (email.Subject != null)
            {
                message.Subject = email?.Subject;
            }

            try
            {
                using (SmtpClient mailer = new SmtpClient())
                {
                    mailer.Connect(_emailServer.Host, _emailServer.Port, SecureSocketOptions.StartTlsWhenAvailable);
                    mailer.AuthenticationMechanisms.Remove("XOAUTH2");

                    mailer.Authenticate(_emailServer.Username, _emailServer.Password);
                    await mailer.SendAsync(message);
                    await mailer.DisconnectAsync(true);




                    ////mailer.Connect("mail.bluehorizonng.com", 587, false);
                    ////mailer.Connect("mail.bluehorizonng.com", 25, false);

                    //mailer.Connect(_emailServer.Host, _emailServer.Port, _emailServer.UseSsl);
                    //mailer.Authenticate(_emailServer.Username, _emailServer.Password);
                    //await mailer.SendAsync(message);
                    //await mailer.DisconnectAsync(true);
                }
            }
            catch (Exception)
            {
                throw;
            }


        }

    }
}
