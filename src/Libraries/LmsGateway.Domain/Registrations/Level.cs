using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class Level : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
