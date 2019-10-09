using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LmsGateway.Domain;
using Microsoft.AspNetCore.Authorization;
using LmsGateway.Web.Models;
using LmsGateway.Core.Notifications;
using LmsGateway.Core.Extensions;

namespace LmsGateway.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signinManager;
        private readonly IEmailService _emailService;
        private readonly EmailServer _emailServer;

        public AccountController(UserManager<User> userManager, SignInManager<User> signinManager, IEmailService emailService, EmailServer emailServer)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _emailService = emailService;
            _emailServer = emailServer;
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            LoginModel loginModel = new LoginModel() { ReturnUrl = returnUrl };

            return await Task.FromResult(View(loginModel));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    await _signinManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult result = await _signinManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        //return Redirect(loginModel.ReturnUrl ?? "/");

                        if (loginModel.ReturnUrl.IsEmpty())
                        {
                            return RedirectToAction("Index", "Dashboard", new { Area = "Student" });
                        }
                        else
                        {
                            return Redirect(loginModel.ReturnUrl);
                        }
                    }
                }

                ModelState.AddModelError(nameof(loginModel.Password), "Email or password is invalid!");
            }

            return View(loginModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Signup(string returnUrl = null)
        {
            UserModel userModel = new UserModel() { ReturnUrl = returnUrl };
            return await Task.FromResult(View(userModel));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IAgree)
                {
                    User user = new User() { Name = model.Name, Email = model.Email, UserName = model.Email };

                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                        await SendMail(user, callbackUrl);
                        
                        return RedirectToAction(nameof(Login), new { returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError(nameof(model.ConfirmPassword), error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(model.IAgree), "You must agree to the terms and condition");
                }
            }

            return View(model);
        }

        //[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            //Find User Details by userId
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            ViewBag.Message = "Your email has been confirmed. Please Login now. ";
            //ViewData["Message"] = "Your email has been confirmed. Please Login now. ";

            ViewData["MessageValue"] = "1";

            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string returnUrl = null)
        {
            LoginModel loginModel = new LoginModel() { ReturnUrl = returnUrl };

            return await Task.FromResult(View(loginModel));
        }

        private async Task SendMail(User user, string callbackUrl)
        {
            //EmailServer emailServer = new EmailServer("Mail Admin", "info@bluehorizonng.com", "password", "mail.bluehorizonng.com");
            //EmailServer emailServer = new EmailServer("Mail Admin", "info@bluehorizonng.com", "password", "mail.bluehorizonng.com", port: 587);

            Email email = new Email();
            email.ToEmailAddress = new EmailAddress() { Name = user.Name, Email = user.Email };
            email.FromEmailAddress = new EmailAddress() { Name = _emailServer.Name, Email = _emailServer.Username };

            //email.FromEmailAddress = new EmailAddress() { Name = "Isioma", Email = "linkdanex@yahoo.co.uk" };
            //email.ToEmailAddress = new EmailAddress() { Name = emailServer.Name, Email = emailServer.Username };
            //email.Message = $"Click {callBackUrl} to activate your account";

            var builder = new MimeKit.BodyBuilder();
            builder.HtmlBody = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";

            email.Message = builder.HtmlBody;
            email.Subject = "Activate Your Account";

            try
            {
                //IEmailService emailService = new EmailService(emailServer);
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }




    }
}
