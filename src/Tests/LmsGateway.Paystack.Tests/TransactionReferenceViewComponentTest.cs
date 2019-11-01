using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using LmsGateway.Paystack.ViewComponents;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Paystack.Services;
using LmsGateway.Paystack.Domain;
using LmsGateway.Core.Data;
using LmsGateway.Data;
using Microsoft.EntityFrameworkCore;
using LmsGateway.Paystack.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Internal;
using LmsGateway.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using LmsGateway.Paystack.Models;
using LmsGateway.Core.Configuration;
using Moq;
using LmsGateway.Paystack.Settings;
using LmsGateway.Web.Framework.UI;

namespace LmsGateway.Paystack.Tests
{
    public class TransactionReferenceViewComponentTest
    {
        private Mock<IWidgetProvider> _widgetProvider;
        private Mock<IGatewayLuncher> _gatewayLuncher;
        private Mock<ISettingService> _settingService;
        private Mock<ITransactionLogService> _transactionLogService;

        public TransactionReferenceViewComponentTest()
        {
            _widgetProvider = new Mock<IWidgetProvider>();
            _gatewayLuncher = new Mock<IGatewayLuncher>();
            _settingService = new Mock<ISettingService>();
            _transactionLogService = new Mock<ITransactionLogService>();
        }

        [Fact]
        public async Task CanGenerateTransactionReference_Unit()
        {
            PaystackSetting paystackSetting = new PaystackSetting()
            {
                UsePublicKey = false,
                ApiBaseUrl = "https://api.paystack.co/",
                ReferencePrefix = "PV",
                PublicKey = "pk_test_0b413fb09eeb29832cded5a56918a0f5f6a4c2f0",
                SecretKey = "sk_test_6ce9543acf5dd68127387f49ce437f7f82aafb47",

                ListTransactionEndPoint = "/transaction",
                VerifyTransactionEndPoint = "/transaction/verify/",
                FetchTransactionEndPoint = "/transaction/",
                InitializeTransactionEndPoint = "/transaction/initialize",
                TransactionTotalEndPoint = "/transaction/totals",
                FetchSettlementsEndPoint = "/settlement",

                APIAcceptHeaderKey = "accept",
                APIAcceptHeaderValue = "application/json",
                APIAuthorizationHeaderKey = "Authorization",
                APIAuthorizationHeaderValue = "Bearer",

                APIFromQueryParameterKey = "from",
                APIToQueryParameterKey = "to",
                APIAmountQueryParameterKey = "amount",
                APIStatusQueryParameterKey = "status",

                APIReferenceParameterKey = "reference",
                APIAmountParameterKey = "amount",
                APICallbackUrlParameterKey = "callback_url",
                APIEmailParameterKey = "email",
                APICustomFieldsParameterKey = "custom_fields",
                APIMetaDataParameterKey = "metadata",

                TransactionSearchDateFormat = "yyyy-MM-dd",
            };

            string tranxRef = "099885321155456";
            PaystackTransactionLog transactionLog = new PaystackTransactionLog()
            {
                Reference = tranxRef,
                TransactionDate = DateTime.UtcNow,
            };
            
            _settingService.Setup(x => x.GetSetting<PaystackSetting>()).Returns(Task.FromResult(paystackSetting));
            _gatewayLuncher.Setup(x => x.CreateTransactionRef(paystackSetting.ReferencePrefix)).Returns(Task.FromResult(tranxRef));
            _transactionLogService.Setup(x => x.SaveAsync(transactionLog)).Verifiable();

            var transactionRef = new TransactionReference(_transactionLogService.Object, _gatewayLuncher.Object, _settingService.Object, _widgetProvider.Object);
            var response = await transactionRef.InvokeAsync();
            var result = (ViewViewComponentResult)response;

            Assert.NotNull(result);
            Assert.NotNull(response);
            Assert.IsType<ViewViewComponentResult>(response);
            Assert.IsType<string>(result.ViewData.Model);
        }
        
        [Fact]
        public async Task CanGenerateTransactionReference()
        {
            HostingEnvironment hostEnvironment = new HostingEnvironment();
            hostEnvironment.ContentRootPath = "C:\\Users\\LENOVO\\Documents\\Repositories\\lms-gateway\\src\\Presentation\\LmsGateway.Web";
            hostEnvironment.ContentRootFileProvider = new PhysicalFileProvider(hostEnvironment.ContentRootPath);
            hostEnvironment.EnvironmentName = "Development";

            Startup startup = new Web.Startup(hostEnvironment);
            ServiceCollection sc = new ServiceCollection();
            startup.ConfigureServices(sc);

            IServiceProvider serviceProvider = sc.BuildServiceProvider();
            ITransactionLogService transactionLogService = serviceProvider.GetService<ITransactionLogService>();
            IGatewayLuncher gatewayLuncher = serviceProvider.GetService<IGatewayLuncher>();
            ISettingService settingService = serviceProvider.GetService<ISettingService>();
            IWidgetProvider widgetProvider = serviceProvider.GetService<IWidgetProvider>();

            var transactionRef = new TransactionReference(transactionLogService, gatewayLuncher, settingService, widgetProvider);
            var response = await transactionRef.InvokeAsync();
            var result = (ViewViewComponentResult)response;

            //Assert.NotNull(result);
            //Assert.NotNull(response);
            //Assert.IsType<ViewViewComponentResult>(response);
            //Assert.IsType<string>(result.ViewData.Model);
            

        }



    }
}
