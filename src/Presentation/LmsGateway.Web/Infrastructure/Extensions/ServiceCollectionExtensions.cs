using System;
using System.Collections.Generic;
using System.Linq;

using LmsGateway.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LmsGateway.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static List<IDependencyRegistrar> Registrars { get; set; }

        public static void RegisterCustomServices(this IServiceCollection services, IDictionary<string, string> connectionStrings = null)
        {
            // get type finder
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ITypeFinder typeFinder = serviceProvider.GetService<ITypeFinder>();

            //get registrars
            List<TypeInfo> registrarList = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            // create registrar instance list
            Registrars = new List<IDependencyRegistrar>();
            registrarList.ForEach(dr => Registrars.Add(Activator.CreateInstance(dr.AsType()) as IDependencyRegistrar));

            //sort
            Registrars = Registrars.OrderBy(x => x.Order).ToList();

            //register dependencies
            Registrars.ForEach(dr => dr.Register(services, connectionStrings));
        }




    }
}
