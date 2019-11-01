using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LmsGateway.Data;
using LmsGateway.Services.Configuration;
using LmsGateway.Data.Setup;
using LmsGateway.Domain.Configuration;
using LmsGateway.Core.Data;
using LmsGateway.Core.Configuration;
using LmsGateway.Services.Notifications;
using LmsGateway.Core.Notifications;
using LmsGateway.Web.Framework.UI;
using LmsGateway.Services.Registrations;
using LmsGateway.Domain.Registrations;
using LmsGateway.Core.Payments;
using System.Reflection;
using LmsGateway.Web.Framework.Extensions;
using LmsGateway.Services.Payments;

namespace LmsGateway.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 3;
       
        public void Register(IServiceCollection services, IDictionary<string, string> connectionString = null)
        {
            services.AddScoped<IWidgetProvider, WidgetProvider>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEFContextFactory, EFContextFactory>();
            services.AddTransientPaymentProviders();

            //services.AddTransient(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient(typeof(IRepository<Setting>), x =>
            {
                var dbContextFactory = x.GetService<IEFContextFactory>();
                DbContext dbContext = dbContextFactory.GetDbContext(nameof(EFDataContext), connectionString);

                return new EFRepository<Setting>(dbContext);
            });
            services.AddTransient(typeof(ISettingService), x =>
            {
                return new SettingService(x.GetService<IRepository<Setting>>());
            });



            services.AddTransient(typeof(IRepository<RegistrationFee>), x =>
            {
                var dbContextFactory = x.GetService<IEFContextFactory>();
                DbContext dbContext = dbContextFactory.GetDbContext(nameof(EFDataContext), connectionString);

                return new EFRepository<RegistrationFee>(dbContext);
            });
            services.AddTransient(typeof(IRegistrationFeeService), x =>
            {
                return new RegistrationFeeService(x.GetService<IRepository<RegistrationFee>>());
            });



            services.AddTransient(typeof(IRepository<RegistrationPeriod>), x =>
            {
                var dbContextFactory = x.GetService<IEFContextFactory>();
                DbContext dbContext = dbContextFactory.GetDbContext(nameof(EFDataContext), connectionString);

                return new EFRepository<RegistrationPeriod>(dbContext);
            });
            services.AddTransient(typeof(IRegistrationPeriodService), x =>
            {
                return new RegistrationPeriodService(x.GetService<IRepository<RegistrationPeriod>>());
            });



            services.AddTransient(typeof(IRepository<Registration>), x =>
            {
                var dbContextFactory = x.GetService<IEFContextFactory>();
                DbContext dbContext = dbContextFactory.GetDbContext(nameof(EFDataContext), connectionString);

                return new EFRepository<Registration>(dbContext);
            });
            services.AddTransient(typeof(IRegistrationService), x =>
            {
                return new RegistrationService(x.GetService<IRepository<Registration>>());
            });
        }

        //private static void AddService<IService, T>(IServiceCollection services, string dbContextName, IDictionary<string, string> connectionString) where TDomain : class, IDataContext : 
        //{
        //    services.AddTransient(typeof(IRepository<T>), x =>
        //    {
        //        var dbContextFactory = x.GetService<IEFContextFactory>();
        //        DbContext dbContext = dbContextFactory.GetDbContext(dbContextName, connectionString);

        //        return new EFRepository<T>(dbContext);
        //    });
        //    services.AddTransient(typeof(IService), x =>
        //    {
        //        return new RegistrationPeriodService(x.GetService<IRepository<T>>());
        //    });
        //}




        //private static void AddService(IServiceCollection services, IDictionary<string, string> connectionString)
        //{
        //    services.AddTransient(typeof(IRepository<RegistrationPeriod>), x =>
        //    {
        //        var dbContextFactory = x.GetService<IEFContextFactory>();
        //        DbContext dbContext = dbContextFactory.GetDbContext(nameof(EFDataContext), connectionString);

        //        return new EFRepository<RegistrationPeriod>(dbContext);
        //    });
        //    services.AddTransient(typeof(IRegistrationPeriodService), x =>
        //    {
        //        return new RegistrationPeriodService(x.GetService<IRepository<RegistrationPeriod>>());
        //    });
        //}




    }
}
