using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using LmsGateway.Core.Payments;

namespace LmsGateway.Web.Framework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static List<IDependencyRegistrar> Registrars { get; set; }

        public static void AddOtherServices(this IServiceCollection services, IDictionary<string, string> connectionStrings = null)
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

        public static void AddTransientPaymentProviders(this IServiceCollection services)
        {
            // get type finder
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ITypeFinder typeFinder = serviceProvider.GetService<ITypeFinder>();

            //get registrars
            List<TypeInfo> paymentProviders = typeFinder.FindClassesOfType<IPaymentMethod>();

            //paymentProviders.ForEach(x => )

            foreach (TypeInfo type in paymentProviders)
            {
                if (type.IsClass)
                {
                    Type tp = type.UnderlyingSystemType;
                    services.AddTransient(typeof(IPaymentMethod), tp);
                }
            }
        }

        public static void AddPluginDependencies(this IServiceCollection services, IList<ModuleInfo> plugins, IDictionary<string, string> connectionStrings = null)
        {
            foreach (ModuleInfo plugin in plugins)
            {
                // Register dependency in modules
                var dependencyRegistrar = plugin.Assembly.GetTypes().Where(x => typeof(IDependencyRegistrar).IsAssignableFrom(x)).FirstOrDefault();
                if (dependencyRegistrar != null && dependencyRegistrar != typeof(IDependencyRegistrar))
                {
                    var dependencyRegistrarInstance = (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar);
                    dependencyRegistrarInstance.Register(services, connectionStrings);
                }
            }
        }






    }
}
