using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace LmsGateway.Web.Areas.Student.Controllers
{
    [Area("Student")]
    public class AccountController : Controller
    {
        public AccountController()
        {

        }

        public async Task<IActionResult> Index() => await Task.FromResult(View());

        public async Task<IActionResult> Edit() => await Task.FromResult(View());

        public async Task<IActionResult> Subscription() => await Task.FromResult(View());

        public async Task<IActionResult> Profile() => await Task.FromResult(View());

        


    }
}
