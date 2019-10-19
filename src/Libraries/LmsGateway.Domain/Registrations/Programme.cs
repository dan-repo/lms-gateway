using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class Programme : BaseEntity
    {
        public string Name { get; set; }
        public string Alias { get; set; }
    }
}
