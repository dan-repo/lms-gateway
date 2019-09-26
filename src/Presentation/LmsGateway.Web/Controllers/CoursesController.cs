using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace LmsGateway.Web.Controllers
{
    public class CoursesController : Controller
    {
        public async Task<IActionResult> List()
        {
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> Index(int? id) => await Task.FromResult(View());



    }
}
