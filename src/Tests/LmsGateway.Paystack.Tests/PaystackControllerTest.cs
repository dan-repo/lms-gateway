using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.FileProviders;
using LmsGateway.Web;
using Microsoft.Extensions.DependencyInjection;
using LmsGateway.Paystack.Controllers;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Core.Configuration;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using LmsGateway.Paystack.Models;

namespace LmsGateway.Paystack.Tests
{
    public class PaystackControllerTest
    {
        [Fact]
        public async Task c()
        {
            HostingEnvironment hostEnvironment = new HostingEnvironment();
            hostEnvironment.ContentRootPath = "C:\\Users\\LENOVO\\Documents\\Repositories\\lms-gateway\\src\\Presentation\\LmsGateway.Web";
            hostEnvironment.ContentRootFileProvider = new PhysicalFileProvider(hostEnvironment.ContentRootPath);
            hostEnvironment.EnvironmentName = "Development";

            Startup startup = new Web.Startup(hostEnvironment);
            IServiceCollection sc = new ServiceCollection();
            startup.ConfigureServices(sc);

            IServiceProvider serviceProvider = sc.BuildServiceProvider();
            ISupportedCurrencyService supportedCurrencyService = serviceProvider.GetService<ISupportedCurrencyService>();
            IGatewayLuncher gatewayLuncher = serviceProvider.GetService<IGatewayLuncher>();
            ITransactionLogService transactionLogService = serviceProvider.GetService<ITransactionLogService>();
            ITransactionStatusService transactionStatusService = serviceProvider.GetService<ITransactionStatusService>();
            ISettingService settingService = serviceProvider.GetService<ISettingService>();

            //PaystackController paystackController = new PaystackController(transactionStatusService
            //    , supportedCurrencyService
            //    , hostEnvironment
            //    , gatewayLuncher
            //    , transactionLogService
            //    , settingService);
            
            //var response = await paystackController.InvokeAsync();
            //var result = (ViewViewComponentResult)response;

            //Assert.NotNull(result);
            //Assert.NotNull(response);
            //Assert.IsType<ViewViewComponentResult>(response);
            //Assert.IsType<PaymentInfoModel>(result.ViewData.Model);
            //Assert.True((result.ViewData.Model as PaymentInfoModel).IconUrl != null);
        }




    }
}
