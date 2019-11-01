using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Core.Payments;
using LmsGateway.Paystack.Domain;
using LmsGateway.Paystack.Settings;
using LmsGateway.Paystack.Models;
using LmsGateway.Core.Domain.Payments;
using LmsGateway.Core.Extensions;
using Newtonsoft.Json;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Core.Configuration;

namespace LmsGateway.Paystack.Providers
{
    public class PaystackProvider : IPaymentMethod
    {
        private readonly ISettingService _settingService;
        private readonly IGatewayLuncher _gatewayLuncher;
        private readonly ISupportedCurrencyService _supportedCurrencyService;
        private readonly ITransactionLogService _transactionLogService;

        private PaymentMetadata _metadata;

        public PaystackProvider(IGatewayLuncher gatewayLuncher,
            ISupportedCurrencyService supportedCurrencyService,
            ITransactionLogService transactionLogService,
            ISettingService settingService)
        {
            Guard.NotNull(settingService, nameof(settingService));
            Guard.NotNull(gatewayLuncher, nameof(gatewayLuncher));
            Guard.NotNull(supportedCurrencyService, nameof(supportedCurrencyService));
            Guard.NotNull(transactionLogService, nameof(transactionLogService));

            _settingService = settingService;
            _gatewayLuncher = gatewayLuncher;
            _supportedCurrencyService = supportedCurrencyService;
            _transactionLogService = transactionLogService;

            _metadata = new PaymentMetadata()
            {
                Name = "Paystack",
                SystemName = "LmsGateway.Paystack",
                Author = "Daniel Egenti",
                IsActive = true,
                Version = "1.0.0",
                Category = "Payments",
                Company = "Nitware Solutions Ltd.",
                Website = "http://www.nitware.com.ng"
            };
        }

        public bool IsActive { get; set; }

        public PaymentMethodType PaymentMethodType => PaymentMethodType.Redirection;

        public PaymentMetadata Metadata
        {
            get { return _metadata; }
            set { _metadata = value; }
        }

        public async Task PostProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            HttpContext httpContext = processPaymentRequest.HttpContext;

            try
            {
                if (processPaymentRequest.PaymentStatus == PaymentStatus.Paid)
                    return;

                int selectedCurrencyId = processPaymentRequest.SelectedCurrencyId;
                PaystackSupportedCurrency supportedCurrency = await _supportedCurrencyService.GetSupportedCurrencyById(selectedCurrencyId);
                if (supportedCurrency == null || supportedCurrency.Id <= 0)
                {
                    throw new ArgumentNullException("Plugins.SmartStore.Paystack.SupportedCurrencyNullArgument");
                }

                PaystackSetting paystackSettings = await _settingService.GetSetting<PaystackSetting>();
                if (paystackSettings == null)
                {
                    throw new ArgumentNullException("Plugins.SmartStore.Paystack.PaystackSettingsNullArgument");
                }
                
                PaystackTransaction transactionResponse = await _gatewayLuncher.MakePayment(processPaymentRequest, paystackSettings, supportedCurrency, httpContext);
                if (transactionResponse != null && transactionResponse.status && !string.IsNullOrWhiteSpace(transactionResponse.Data.authorization_url))
                {
                    await _gatewayLuncher.LogTransaction(transactionResponse);
                    await _gatewayLuncher.LunchPaymentPage(httpContext, transactionResponse.Data.authorization_url);
                }
                else
                {
                    string errorMessage = transactionResponse != null ? transactionResponse.message : string.Format("Plugins.SmartStore.Paystack.EmptyGatewayResponseMessage");
                    httpContext.Session.SetString(_gatewayLuncher.ErrorMessage, errorMessage);
                }
            }
            catch (Exception ex)
            {
                httpContext.Session.SetString(_gatewayLuncher.ErrorMessage, ex.Message);
            }



         
        }

       




    }
}
