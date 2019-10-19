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
using LmsGateway.Core.Models;

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

            //IAssemblyProvider
            //DefaultAssemblyProvider
            //IAssemblyLoaderContainer
            //IAssemblyLoadContextAccessor
        }

        #region Utilities

        [NonAction]
        private async Task SendMail(User user, string callbackUrl)
        {
            Email email = new Email();
            email.ToEmailAddress = new EmailAddress() { Name = user.Name, Email = user.Email };
            email.FromEmailAddress = new EmailAddress() { Name = _emailServer.Name, Email = _emailServer.Username };

            var builder = new MimeKit.BodyBuilder();
            builder.HtmlBody = $"Please confirm your email by clicking <b><a href='{callbackUrl}'>here</a></b>";

            email.Message = builder.HtmlBody;
            email.Subject = "Activate Your Account";

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

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
                        if (user.EmailConfirmed)
                        {
                            if (user.Verified)
                            {
                                //if (loginModel.ReturnUrl.IsEmpty())
                                //{
                                //    return RedirectToAction("Index", "Dashboard", new { Area = "Student" });
                                //}
                                //else
                                //{
                                
                                switch (user.Type)
                                {
                                    case UserType.Admin:
                                        {
                                            return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                                        }
                                    case UserType.Instructor:
                                        {
                                            return RedirectToAction("Index", "Dashboard", new { Area = "Instructor" });
                                        }
                                    case UserType.Student:
                                        {
                                            return RedirectToAction("Index", "Dashboard", new { Area = "Student" });
                                        }
                                    default:
                                        {
                                            return Redirect(loginModel.ReturnUrl);
                                        }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError(nameof(loginModel.Password), "Your verification is still pending! Please contact your system administrator.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(loginModel.Password), "Your email has not been confirmed! Please confirm your email from your registered email");
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
                    User user = new User() { Name = model.Name, Email = model.Email, UserName = model.Email, Type = (UserType)Enum.Parse(typeof(UserType), model.Type), Verified = false };

                    try
                    {
                        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                            await SendMail(user, callbackUrl);

                            return RedirectToAction(nameof(SignedUp));

                            //return RedirectToAction(nameof(Login), new { returnUrl = model.ReturnUrl });
                        }
                        else
                        {
                            foreach (IdentityError error in result.Errors)
                            {
                                ModelState.AddModelError(nameof(model.ConfirmPassword), error.Description);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(nameof(model.IAgree), ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(model.IAgree), "You must agree to the terms and condition");
                }
            }
            //else
            //{
            //    List<string> errors = new List<string>();
            //    foreach (var modelState in ModelState.Values)
            //    {
            //        foreach (var modelError in modelState.Errors)
            //        {
            //            errors.Add(modelError.ErrorMessage);
            //        }
            //    }

            //    var str = new System.Text.StringBuilder();
            //    errors.ForEach(x => str.Append(x + "\n"));
            //    ModelState.AddModelError(nameof(model.ConfirmPassword), str.ToString());

            //}

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignupForm(UserModel model)
        {
            string message = null;

            if (ModelState.IsValid)
            {
                if (model.IAgree)
                {
                    User user = new User() { Name = model.Name, Email = model.Email, UserName = model.Email, Type = (UserType)Enum.Parse(typeof(UserType), model.Type), Verified = false };

                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                        try
                        {
                            await SendMail(user, callbackUrl);
                        }
                        catch(Exception ex)
                        {
                            message += "Your account was successfully created, but sending email to your email failed with the following error:\n\n " + ex.Message + "\n\nKindly contact your system administrator to verify your email\n";
                        }
                        
                        return Json("ok");

                        //return RedirectToAction(nameof(Login), new { returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            message += error.Description + "\n";

                            //ModelState.AddModelError(nameof(model.ConfirmPassword), error.Description);
                        }
                    }
                }
                else
                {
                    message += "You must agree to the terms and condition\n";
                }
            }

            return Json(message);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignedUp()
        {
            return await Task.FromResult(View());
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            TempData["Message"] = "Your email has been confirmed. Please contact your system adminstrator to verify your account.";

            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string returnUrl = null)
        {
            LoginModel loginModel = new LoginModel() { ReturnUrl = returnUrl };

            return await Task.FromResult(View(loginModel));
        }

        




    }
}
