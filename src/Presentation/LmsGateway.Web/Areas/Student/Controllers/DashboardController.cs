using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace LmsGateway.Web.Areas.Student.Controllers
{
    [Area("Student")]
    public class DashboardController : Controller
    {
        public DashboardController()
        {

        }
        
        public async Task<IActionResult> Index() => await Task.FromResult(View());

    }



}
