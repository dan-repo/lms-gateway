using LmsGateway.Core.Notifications;
using LmsGateway.Services.Notifications;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LmsGateway.Services.Tests
{
    public class EmailServiceTest
    {
        [Fact]
        public async Task CanSendEmail()
        {
            EmailServer emailServer = new EmailServer("Mail Admin", "info@bluehorizonng.com", "password", "mail.bluehorizonng.com");
            //EmailServer emailServer = new EmailServer("Mail Admin", "info@bluehorizonng.com", "password", "mail.bluehorizonng.com", port: 587);

            Email email = new Email();
            //email.ToEmailAddress = new EmailAddress() { Name = "Isioma", Email = "linkdanex@yahoo.co.uk" };
            email.ToEmailAddress = new EmailAddress() { Name = "Isioma", Email = "egenti.daniel@gmail.com" };
            email.FromEmailAddress = new EmailAddress() { Name = emailServer.Name, Email = emailServer.Username };

            //email.FromEmailAddress = new EmailAddress() { Name = "Isioma", Email = "linkdanex@yahoo.co.uk" };
            //email.ToEmailAddress = new EmailAddress() { Name = emailServer.Name, Email = emailServer.Username };
            email.Message = "Please throw more light on your e-commerce service";
            email.Subject = "Information Request";

            try
            {
                IEmailService emailService = new EmailService(emailServer);
                await emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

    }
}
