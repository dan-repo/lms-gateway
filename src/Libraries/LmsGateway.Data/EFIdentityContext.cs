using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LmsGateway.Data.Mappings;
using LmsGateway.Domain.Users;
using LmsGateway.Data.Mappings.Users;

namespace LmsGateway.Data
{
    public class EFIdentityContext : IdentityDbContext<User>
    {
        public EFIdentityContext(DbContextOptions<EFIdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(x => new UserMap(x));

            base.OnModelCreating(builder);

        }




    }
}
