using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LmsGateway.Web.Models;

namespace LmsGateway.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private List<CourseModel> _courses;

        public CoursesController()
        {
            _courses = new List<CourseModel>()
            {
                new CourseModel() { Id = 4, Name = "Business Administration", Description = "Introduction to business management", Cost = 1100, Level= 1, Rating= 4, Image = "/images/courses/ba.png", URL="http://lms.com/business-administration" },
                new CourseModel() { Id = 5, Name = "Marketing", Description = "Digital marketing", Cost = 2200, Level= 2, Rating= 5, Image = "/images/courses/mt.png", URL="http://lms.com/marketing" },
                new CourseModel() { Id = 1, Name = "Basics of VueJs", Description = "Learn VueJs the easy way!", Cost = 1200, Level= 2, Rating= 4, Image = "/images/courses/vuejs.png", URL="http://lms.com/vuejs" },
                new CourseModel() { Id = 2, Name = "Npm & Gulp", Description = "Npm & Gulp Advanced Workflow", Cost = 900, Level= 1, Rating= 5, Image = "/images/courses/gulp.png", URL="http://lms.com/npm-gulp" },
                new CourseModel() { Id = 3, Name = "Github Webhooks for Beginners", Description = "Developing static website with fast and advanced gulp setup.", Cost = 1500, Level= 2, Rating= 5, Image = "/images/courses/github.png", URL="http://lms.com/github" },
            };
        }

        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View(_courses));
        }

        public async Task<IActionResult> List()
        {
            return await Task.FromResult(View(_courses));
        }

    }
}
