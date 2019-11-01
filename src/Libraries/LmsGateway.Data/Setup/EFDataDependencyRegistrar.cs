using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain;
using LmsGateway.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LmsGateway.Data.Setup
{
    public class EFDataDependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(IServiceCollection services, IDictionary<string, string> connectionStrings = null)
        {
            string connectionString = connectionStrings["Data"];

            services.AddDbContext<EFDataContext>(o => o.UseSqlServer(connectionString));
            services.AddTransient<DbContext, EFDataContext>();
        }

      


    }



}
