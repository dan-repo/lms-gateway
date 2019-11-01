using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LmsGateway.Domain.Users
{
    public class User : IdentityUser
    {
        public string RegNo { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
        public bool Verified { get; set; }
        public DateTime? DateVerified { get; set; }
        public DateTime? CreatedOn { get; set; }

    }


}
