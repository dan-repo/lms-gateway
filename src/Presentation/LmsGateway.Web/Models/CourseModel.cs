using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Web.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Rating { get; set; }
        public decimal Cost { get; set; }
        public string Image { get; set; }
        public string URL { get; set; }
    }





}
