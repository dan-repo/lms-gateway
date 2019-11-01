using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Paystack.Models;
using LmsGateway.Paystack.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using LmsGateway.Core.Extensions;
using LmsGateway.Paystack.Settings;
using LmsGateway.Core.Configuration;
using LmsGateway.Web.Framework.UI;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace LmsGateway.Paystack.ViewComponents
{
    public class TransactionReference : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IGatewayLuncher _gatewayLuncher;
        private readonly ITransactionLogService _transactionLogService;
        private readonly IWidgetProvider _widgetProvider;

        public TransactionReference(ITransactionLogService transactionLogService, IGatewayLuncher gatewayLuncher, ISettingService settingService, IWidgetProvider widgetProvider)
        {
            Guard.NotNull(widgetProvider, nameof(widgetProvider));
            Guard.NotNull(settingService, nameof(settingService));
            Guard.NotNull(gatewayLuncher, nameof(gatewayLuncher));
            Guard.NotNull(transactionLogService, nameof(transactionLogService));

            _widgetProvider = widgetProvider;
            _settingService = settingService;
            _gatewayLuncher = gatewayLuncher;
            _transactionLogService = transactionLogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewViewComponentResult viewComponent = null;
            bool isRegistered = _widgetProvider.IsRegistered(WidgetZone.TransactionReference);
            if (isRegistered)
            {
                string transactionRef = await LogTransactionReference();
                viewComponent = await Task.FromResult(View(new ConfigurationModel() { TransactionReference = transactionRef }));
            }
            else
            {
                viewComponent = await Task.FromResult(View(new ConfigurationModel()));
            }

            return viewComponent;
        }

        private async Task<string> LogTransactionReference()
        {
            PaystackSetting setting = await _settingService.GetSetting<PaystackSetting>();
            if (setting == null)
                return null;

            string transactionRef = await _gatewayLuncher.CreateTransactionRef(setting.ReferencePrefix);

            PaystackTransactionLog transactionLog = new PaystackTransactionLog()
            {
                Reference = transactionRef,
                TransactionDate = DateTime.UtcNow,
            };

            await _transactionLogService.SaveAsync(transactionLog);

            return transactionRef;
        }



    }
}
