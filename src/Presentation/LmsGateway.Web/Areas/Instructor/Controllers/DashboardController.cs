using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LmsGateway.Web.Areas.Instructor.Controllers
{
    [Authorize]
    [Area("Instructor")]
    public class DashboardController : Controller
    {
        public DashboardController()
        {

        }

        public async Task<IActionResult> Index(string id = null) => await Task.FromResult(View());

    }




}
