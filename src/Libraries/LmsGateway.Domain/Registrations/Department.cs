using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int FacultyId { get; set; }

        public Faculty Faculty { get; set; }
    }


}
