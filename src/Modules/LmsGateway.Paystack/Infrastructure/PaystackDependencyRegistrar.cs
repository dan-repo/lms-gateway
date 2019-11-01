using LmsGateway.Core.Data;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Data;
using LmsGateway.Paystack.Data;
using LmsGateway.Paystack.Domain;
using LmsGateway.Paystack.Filters;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Paystack.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace LmsGateway.Paystack.Infrastructure
{
    public class PaystackDependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 2;

        public void Register(IServiceCollection services, IDictionary<string, string> connectionStrings = null)
        {
            string connectionString = connectionStrings["Data"];

            services.AddDbContext<EFPaystackDataContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<DbContext, EFPaystackDataContext>();
            //services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddTransient<IRepository<PaystackTransactionStatus>, EFRepository<PaystackTransactionStatus>>();
            services.AddTransient<IRepository<PaystackSupportedCurrency>, EFRepository<PaystackSupportedCurrency>>();
            services.AddTransient<IRepository<PaystackTransactionLog>, EFRepository<PaystackTransactionLog>>();

            services.AddTransient<ITransactionStatusService, TransactionStatusService>();
            services.AddTransient<ISupportedCurrencyService, SupportedCurrencyService>();
            services.AddTransient<ITransactionLogService, TransactionLogService>();
            services.AddSingleton<IGatewayLuncher, GatewayLuncher>();

            //filters
            services.AddScoped<PaystackRegistrationConfirmFilter>();
            services.AddScoped<PaystackRegistrationCompletedFilter>();



            //services.AddTransient(typeof(IRepository<PaystackTransactionLog>), x =>
            //{
            //    return new EFRepository<PaystackTransactionLog>(x.GetService<EFPaystackDataContext>());
            //});
            //services.AddTransient(typeof(ITransactionLogService), x =>
            //{
            //    return new TransactionLogService(x.GetService<IRepository<PaystackTransactionLog>>());
            //});







        }

    }
}
