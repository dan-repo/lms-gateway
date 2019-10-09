using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LmsGateway.Domain;

namespace LmsGateway.Web.Areas.Student.Controllers
{
    [Area("Student")]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
       
        public async Task<IActionResult> Index() => await Task.FromResult(View());

        public async Task<IActionResult> Edit() => await Task.FromResult(View());

        public async Task<IActionResult> Subscription() => await Task.FromResult(View());

        public async Task<IActionResult> Profile() => await Task.FromResult(View());

        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction(nameof(Index), "Home", new { area = "" });
        }




    }
}
