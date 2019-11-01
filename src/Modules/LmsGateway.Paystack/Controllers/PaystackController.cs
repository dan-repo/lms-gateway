using LmsGateway.Core.Configuration;
using LmsGateway.Core.Extensions;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Paystack.Domain;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Paystack.Models;
using LmsGateway.Paystack.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LmsGateway.Paystack.Controllers
{
    [ViewComponent(Name = "PaymentInfo")]
    public class PaystackController : Controller
    {
        private readonly HttpContext _httpContext;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly ITransactionStatusService _transactionStatusService;
        private readonly ISupportedCurrencyService _supportedCurrencyService;
        private readonly ITransactionLogService _transactionLogService;
        private readonly IGatewayLuncher _gatewayLuncher;
        private readonly ISettingService _settingService;

        public PaystackController(ITransactionStatusService transactionStatusService
            , ISupportedCurrencyService supportedCurrencyService
            , IHostingEnvironment env
            , IGatewayLuncher gatewayLuncher
            //, HttpContext httpContext
            , ITransactionLogService transactionLogService
            , IHttpContextAccessor httpContextAccessor
            , ISettingService settingService)
        {
            Guard.NotNull(env, nameof(env));
            //Guard.NotNull(httpContext, nameof(httpContext));
            Guard.NotNull(settingService, nameof(settingService));
            Guard.NotNull(gatewayLuncher, nameof(gatewayLuncher));
            Guard.NotNull(transactionLogService, nameof(transactionLogService));
            Guard.NotNull(transactionStatusService, nameof(transactionStatusService));
            Guard.NotNull(supportedCurrencyService, nameof(supportedCurrencyService));
            Guard.NotNull(httpContextAccessor, nameof(httpContextAccessor));
            

            _hostEnvironment = env;
            
            _settingService = settingService;
            _gatewayLuncher = gatewayLuncher;
            _transactionLogService = transactionLogService;
            _transactionStatusService = transactionStatusService;
            _supportedCurrencyService = supportedCurrencyService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        //private async Task<PaystackSetting> GetPaystackSettings()
        //{
        //    //return null;
        //    return await _settingService.GetSetting<PaystackSetting>();
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string rootPath = _hostEnvironment.ContentRootPath;

            PaymentInfoModel model = new PaymentInfoModel(rootPath);
            model.SupportedCurrencies = await _supportedCurrencyService.GetSupportedCurrencies();
            if (model.SupportedCurrencies != null && model.SupportedCurrencies.Count > 0)
            {
                foreach (PaystackSupportedCurrency currency in model.SupportedCurrencies)
                {
                    model.Currencies.Add(new SelectListItem { Text = currency.Name, Value = currency.Id.ToString() });
                }

                //model.Currencies.Add(new SelectListItem { Text = "Dollar", Value = "802" });

                //PaystackSetting paystackSetting = await _settingService.GetSetting<PaystackSetting>();
                //await httpContext.Session.SetComplexData(_gatewayLuncher.PaystackSettings, paystackSetting);
            }
            else
            {
                ViewBag.CurrencyLoadMessage = "Paystack currency list failed on load!";
            }

            return await Task.FromResult(new ViewViewComponentResult() { ViewData = new ViewDataDictionary<PaymentInfoModel>(ViewData, model)});
        }

        public async Task<IActionResult> Configure()
        {
            var status = _transactionStatusService.GetAllTransactionStatus();

            return await Task.FromResult(View(status));
        }


       







    }
}
