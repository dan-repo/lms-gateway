using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Notifications
{
    public class EmailAddress
    {
        public EmailAddress() { }
        public EmailAddress(string name, string email)
        {
            Name = name;
            Email = email;
        }
        
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
