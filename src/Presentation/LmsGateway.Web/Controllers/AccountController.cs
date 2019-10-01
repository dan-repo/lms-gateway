using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LmsGateway.Domain;
using Microsoft.AspNetCore.Authorization;
using LmsGateway.Web.Models;

namespace LmsGateway.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signinManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Signup(string returnUrl = null)
        {
            LoginModel loginModel = new LoginModel() { ReturnUrl = returnUrl };

            return await Task.FromResult(View(loginModel));
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
                        return Redirect(loginModel.ReturnUrl ?? "/");
                    }
                }

                ModelState.AddModelError(nameof(loginModel.Password), "Email or password is invalid!");
            }

            return View(loginModel);
        }



        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string returnUrl = null)
        {
            LoginModel loginModel = new LoginModel() { ReturnUrl = returnUrl };

            return await Task.FromResult(View(loginModel));
        }




    }
}
