using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LmsGateway.Web.Models;
using LmsGateway.Core.Notifications;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Core.Extensions;

namespace LmsGateway.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly EmailServer _emailServer;

        public HomeController(IEmailService emailService, EmailServer emailServer)
        {
            Guard.NotNull(emailService, nameof(emailService));
            Guard.NotNull(emailServer, nameof(emailServer));

            _emailServer = emailServer;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index() => await Task.FromResult(View());
       
        public async Task<IActionResult> About() => await Task.FromResult(View());
       
        public async Task<IActionResult> Contact() => await Task.FromResult(View(new ContactFormModel()));
      
        public async Task<IActionResult> Error() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitEnquiry(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Contact));
            }

            Email email = new Email();
            email.FromEmailAddress = new EmailAddress() { Name = model.Name, Email = model.Email };
            email.ToEmailAddress = new EmailAddress() { Name = _emailServer.Name, Email = _emailServer.Username };
            email.Message = model.Comment;
            //mail.Subject = model.Subject;

            string error = null;
            string message = null;

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                error = "Error occurred!\n" + ex.Message;
            }

            message = error.IsEmpty() ? "Your message has been successfully posted." : error;

            return Json(message);
        }




    }
}
