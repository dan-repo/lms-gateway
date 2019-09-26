using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Web.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }



        
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
    }
}
