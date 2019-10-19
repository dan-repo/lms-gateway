using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LmsGateway.Domain.Users;

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

        public async Task<IActionResult> SubscriptionDetail(string view = null)
        {
            PartialViewResult partialView = null;

            switch (view)
            {
                case "subscription":
                    {
                        partialView = await Task.FromResult(PartialView("_account/_subscription"));
                        break;
                    }
                case "upgrade":
                    {
                        partialView = await Task.FromResult(PartialView("_account/_upgrade"));
                        break;
                    }
                case "payment":
                    {
                        partialView = await Task.FromResult(PartialView("_account/_paymentInfo"));
                        break;
                    }
                case "pay-history":
                    {
                        partialView = await Task.FromResult(PartialView("_account/_billing"));
                        break;
                    }
                case "invoice":
                    {
                        partialView = await Task.FromResult(PartialView("_account/_invoice"));
                        break;
                    }
                default:
                    {
                        partialView = await Task.FromResult(PartialView("_account/_subscription"));
                        break;
                    }
            }

            return partialView;

        }

        public async Task<IActionResult> Profile() => await Task.FromResult(View());

        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Login", "Account", new { area = "" });

            //return RedirectToAction(nameof(Index), "Home", new { area = "" });
        }




    }
}
