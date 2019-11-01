using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Design.Internal;
using LmsGateway.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace LmsGateway.Tests
{
    public static class Registrar
    {
        public static IServiceCollection RegisterServices()
        {
            IHostingEnvironment hostEnvironment = new HostingEnvironment();
            hostEnvironment.ContentRootPath = "C:\\Users\\LENOVO\\Documents\\Repositories\\lms-gateway\\src\\Presentation\\LmsGateway.Web";
            hostEnvironment.ContentRootFileProvider = new PhysicalFileProvider(hostEnvironment.ContentRootPath);
            hostEnvironment.EnvironmentName = "Development";

            Startup startup = new Startup(hostEnvironment);
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);

            return services;
        }




    }
}
