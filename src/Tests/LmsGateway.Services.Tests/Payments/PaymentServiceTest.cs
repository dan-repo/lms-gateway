using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using LmsGateway.Tests;
using Microsoft.Extensions.DependencyInjection;
using LmsGateway.Services.Payments;
using LmsGateway.Core.Payments;
using Microsoft.AspNetCore.Http;

namespace LmsGateway.Services.Tests.Payments
{
    public class PaymentServiceTest
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentServiceTest()
        {
           IServiceCollection services = Registrar.RegisterServices();
            services.AddTransient<HttpContext>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void LoadAllPaymentMethods_Should_LoadAllPaymentMethodsSuccessfully()
        {
            IPaymentService paymentService = _serviceProvider.GetService<PaymentService>();

            List<IPaymentMethod> paymentProviders = paymentService.LoadAllPaymentMethods();

            Assert.NotNull(paymentProviders);
            Assert.NotNull(paymentProviders[0].Metadata);
            Assert.True(paymentProviders.Count > 0);
          
        }

        [Fact]
        public void LoadPaymentMethodByName_Should_LoadPaymentMethodByNameSuccessfully()
        {
            IPaymentService paymentService = _serviceProvider.GetService<PaymentService>();

            IPaymentMethod paymentProvider = paymentService.LoadPaymentMethodByName("Paystack");

            Assert.NotNull(paymentProvider);
            Assert.NotNull(paymentProvider.Metadata);
            Assert.True(paymentProvider.Metadata.SystemName == "LmsGateway.Paystack");

        }
    }





}

