using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LmsGateway.Web.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    public class DashboardController : Controller
    {
        public DashboardController()
        {

        }
        
        public async Task<IActionResult> Index(string id = null) => await Task.FromResult(View());

    }



}
