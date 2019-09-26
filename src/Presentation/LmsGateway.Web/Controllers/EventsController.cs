using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using LmsGateway.Web.Models;

namespace LmsGateway.Web.Controllers
{
    public class EventsController : Controller
    {
        private List<EventModel> _events;

        public EventsController()
        {
            _events = new List<EventModel>()
            {
                new EventModel()
                {
                    Id =1,
                    Category ="Education Tips",
                    DatePosted =DateTime.Now,
                    Title = "Best Students Award Day 2012",
                    Content ="Mirum est notare quam littera gothica, quam nunc putamus parum claram, anteposuerit litterarum formas humanitatis per seacula",
                    ImageUrl =""
                },
                new EventModel() {Id=2, Category="General Knowledge", DatePosted=DateTime.Now, Title= "The Best Way to Make Money in 2019", Content="", ImageUrl=""  },
                new EventModel() {Id=3, Category="Tips for success in the 20th Century and One Hundrer years After", DatePosted=DateTime.Now, Title= "Do Not Procastinate", Content="", ImageUrl=""  },
                new EventModel() {Id=4, Category="Student Life", DatePosted=DateTime.Now, Title= "Rules for Reading At Night", Content="", ImageUrl=""  },
                new EventModel() {Id=5, Category="Courses", DatePosted=DateTime.Now, Title= "Software Design Principles", Content="", ImageUrl=""  },
                new EventModel() {Id=6, Category="Web", DatePosted=DateTime.Now, Title= "HTTP Request and Response", Content="", ImageUrl=""  },
                new EventModel() {Id=7, Category="Internet", DatePosted=DateTime.Now, Title= "Web Security", Content="", ImageUrl=""  },
                new EventModel() {Id=8, Category="IoT", DatePosted=DateTime.Now, Title= "Introduction to IoT", Content="Introduction to IoT", ImageUrl=""  },
                new EventModel() {Id=9, Category="Machine Learning", DatePosted=DateTime.Now, Title= "Robotics Engineering", Content="", ImageUrl=""  },
                new EventModel() {Id=10, Category="System Networks", DatePosted=DateTime.Now, Title= "Network Protocols", Content="", ImageUrl=""  },
                //new EventModel() {Id=11, Category="Databases", DatePosted=DateTime.Now, Title= "SQL & NoSQL", Content="", ImageUrl=""  },
                //new EventModel() {Id=12, Category="T-SQL", DatePosted=DateTime.Now, Title= "Sample SQL Query", Content="", ImageUrl=""  },
            };
        }

        public async Task<IActionResult> List(int? id)
        {
            return await Task.FromResult(View());


            //object[] events = new object[] { 1, 3 };

            //if (id.HasValue)
            //{
            //    return await GetEvent(id.Value);
            //}
            //else
            //{
            //    return await Task.FromResult(View(events));
            //}
        }

        public async Task<IActionResult> Index(int? id) => await Task.FromResult(View(_events));

        public async Task<IActionResult> GetEvent(int id)
        {
            //System.Threading.Thread.Sleep(1000);

            return await Task.FromResult(PartialView("_detail", _events.SingleOrDefault(x => x.Id == id)));
        }




    }


}
