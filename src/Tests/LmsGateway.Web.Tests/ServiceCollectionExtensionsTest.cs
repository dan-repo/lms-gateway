using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Internal;
using LmsGateway.Web.Extensions;
using System.IO;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Data;
using LmsGateway.Web.Framework.Extensions;

namespace LmsGateway.Web.Tests
{
    public class ServiceCollectionExtensionsTest
    {
        public ServiceCollectionExtensionsTest()
        {
        }

        [Fact]
        public void CanGetAndRegisterApplicationDependenciesUsingDepencyRegistrars()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<ITypeFinder, AppTypeFinder>();

            IDictionary<string, string> connectionStrings = new Dictionary<string, string>()
            {
                ["Identity"] = "Identity:ConnectionString",
                ["Data"] = "Data:ConnectionString"
            };

            services.AddOtherServices(connectionStrings);

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var identityContext = serviceProvider.GetService<EFIdentityContext>();
            var dataContext = serviceProvider.GetService<EFDataContext>();
            
            Assert.NotNull(ServiceCollectionExtensions.Registrars);
            Assert.True(ServiceCollectionExtensions.Registrars.Count > 0);
            Assert.Equal(3, ServiceCollectionExtensions.Registrars.Count);
            Assert.NotNull(identityContext);
            Assert.NotNull(dataContext);
           
        }

        //[Fact]
        //public void CanGegistrars()
        //{
        //    HostingEnvironment env = new HostingEnvironment();
        //    env.ContentRootPath = Directory.GetCurrentDirectory();
        //    env.EnvironmentName = "Production";

        //    Startup startup = new Startup(env);
        //    ServiceCollection sc = new ServiceCollection();
        //    startup.ConfigureServices(sc);
            

        //}

        //private IServiceCollection ServiceProviderFactory()
        //{
        //    HostingEnvironment env = new HostingEnvironment();
        //    env.ContentRootPath = Directory.GetCurrentDirectory();
        //    env.EnvironmentName = "Development";

        //    Startup startup = new Startup(env);
        //    ServiceCollection sc = new ServiceCollection();
        //    startup.ConfigureServices(sc);
        //    ServiceProvider = sc.BuildServiceProvider();
        //}





    }
}
