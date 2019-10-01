using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LmsGateway.Core.Infrastructure;

namespace LmsGateway.Data.Setup
{
    public class EFIdentityDependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(IServiceCollection services, IDictionary<string, string> connectionStrings = null)
        {
            string connectionString = connectionStrings["Identity"];

            services.AddDbContext<EFIdentityContext>(o => o.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.User.RequireUniqueEmail = true;

                o.Password.RequiredLength = 6;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireDigit = false;

                o.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
            })
            .AddEntityFrameworkStores<EFIdentityContext>()
            .AddDefaultTokenProviders();

          
        }

    }
}
