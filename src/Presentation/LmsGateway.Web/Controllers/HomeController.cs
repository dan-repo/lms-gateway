using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LmsGateway.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index() => await Task.FromResult(View());
       
        public async Task<IActionResult> About() => await Task.FromResult(View());
       
        public async Task<IActionResult> Contact() => await Task.FromResult(View());
      
        public async Task<IActionResult> Error() => await Task.FromResult(View());
      




    }
}
