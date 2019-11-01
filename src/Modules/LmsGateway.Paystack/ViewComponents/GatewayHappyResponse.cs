using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Web.Framework.UI;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Core.Configuration;
using LmsGateway.Paystack.Models;
using Microsoft.AspNetCore.Http;
using LmsGateway.Core.Extensions;
using LmsGateway.Paystack.Settings;
using LmsGateway.Paystack.Services;

namespace LmsGateway.Paystack.ViewComponents
{
    public class GatewayHappyResponse : ViewComponent
    {
        private readonly HttpContext _httpContext;
        private readonly ISettingService _settingService;
        private readonly IGatewayLuncher _gatewayLuncher;
        private readonly ITransactionLogService _transactionLogService;
        private readonly IWidgetProvider _widgetProvider;

        public GatewayHappyResponse(ITransactionLogService transactionLogService, 
            IGatewayLuncher gatewayLuncher, 
            ISettingService settingService, 
            IWidgetProvider widgetProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            Guard.NotNull(widgetProvider, nameof(widgetProvider));
            Guard.NotNull(settingService, nameof(settingService));
            Guard.NotNull(gatewayLuncher, nameof(gatewayLuncher));
            Guard.NotNull(transactionLogService, nameof(transactionLogService));
            Guard.NotNull(httpContextAccessor, nameof(httpContextAccessor));

            _widgetProvider = widgetProvider;
            _settingService = settingService;
            _gatewayLuncher = gatewayLuncher;
            _transactionLogService = transactionLogService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            PaystackTransaction transactionResponse = null;
            TransactionResponseModel transactionResponseModel = null;
            PaystackSetting paystackSetting = null;

            try
            {
                string tranxid = _httpContext.Request.Query["reference"].ToString();
                if (!tranxid.IsEmpty())
                {
                    paystackSetting = await _settingService.GetSetting<PaystackSetting>();
                    transactionResponse = await _gatewayLuncher.VerifyTransaction(paystackSetting, tranxid);
                    await _httpContext.Session.SetComplexData(_gatewayLuncher.GatewayResponse, transactionResponse);

                    if (transactionResponse != null && transactionResponse.Data != null && transactionResponse.Data.customer != null)
                    {
                        transactionResponse.status = false;
                        transactionResponseModel = new TransactionResponseModel() { ResponseData = transactionResponse.Data };
                        transactionResponseModel.HomePageUrl = _gatewayLuncher.GetRedirectUrl(_httpContext.Request, "Index", "Home");
                        if (transactionResponse.status)
                        {
                            _gatewayLuncher.IsSuccessful = true;
                            transactionResponseModel.AlertType = "primary";
                            transactionResponseModel.BorderColor = "blue";
                            transactionResponseModel.ThankYou = "Your transaction was successful";
                            transactionResponseModel.PaymentSuccessful = true;
                           
                        }
                        else
                        {
                            _gatewayLuncher.IsSuccessful = false;
                            transactionResponseModel.BorderColor = "red";
                            transactionResponseModel.AlertType = "danger";
                            transactionResponseModel.ThankYou = "Your transaction failed!";
                            transactionResponseModel.ErrorMessage = transactionResponse.Data.gateway_response;
                            transactionResponseModel.PaymentSuccessful = false;
                            

                            //transactionResponseModel.ErrorMessage = transactionResponse.Data.gateway_response;
                            //_httpContext.Session.SetString(_gatewayLuncher.ErrorMessage, transactionResponseModel.TransactionSummary);

                        }

                        transactionResponseModel.Currency = transactionResponse.Data.currency;
                        transactionResponseModel.GatewayResponse = transactionResponse.Data.gateway_response;
                        transactionResponseModel.TransactionReference = transactionResponse.Data.reference;
                        transactionResponseModel.TransactionStatus = transactionResponse.Data.status;
                    }
                    else
                    {
                        transactionResponseModel.ErrorMessage = "Gateway response is empty! Please click on the 'Continue button' below to initiate another transaction.";
                        //_httpContext.Session.SetString(_gatewayLuncher.ErrorMessage, "Gateway response is empty! Please click on the 'Continue button' below to initiate another transaction."); // "Transction response is empty";
                    }
                }
                else
                {
                    transactionResponseModel.ErrorMessage = "No Transction Reference returned!";
                    //_httpContext.Session.SetString(_gatewayLuncher.ErrorMessage, "No Transction Reference returned!"); // "No Transction Reference returned!";
                }
            }
            catch (Exception ex)
            {
                transactionResponseModel.ErrorMessage = ex.Message;

                ////_logger.Error(ex);
                //_httpContext.Session.SetString(_gatewayLuncher.ErrorMessage, ex.Message);
            }

            try
            {
                await _gatewayLuncher.LogTransaction(transactionResponse);

                //Order order = _orderService.GetOrderById(GatewayLuncher.RegistrationId);
                //SetOrderStatus(order, transactionResponse.status);

                //bool sendMail = paystackSetting.SendMailOnFailedTransaction == true || _gatewayLuncher.IsSuccessful == true;

                //SendEmail(order, sendMail);
            }
            catch (Exception ex)
            {
                transactionResponseModel.ErrorMessage = ex.Message;

                //_logger.Error(ex);
                //_httpContext.Session.SetString(_gatewayLuncher.ErrorMessage, ex.Message);
            }

            //ViewViewComponentResult viewComponent = null;
            //bool isRegistered = _widgetProvider.IsRegistered(WidgetZone.PaymentGatewayHappyResponse);
            //if (isRegistered && _gatewayLuncher.IsSuccessful)
            //{
            //    viewComponent = await Task.FromResult(View(transactionResponseModel));
            //}
            //else
            //{
            //    viewComponent = await Task.FromResult(View(new TransactionResponseModel()));
            //}



            ViewViewComponentResult viewComponent = null;
            bool isRegistered = _widgetProvider.IsRegistered(WidgetZone.PaymentGatewayResponse);
            if (isRegistered)
            {
                viewComponent = await Task.FromResult(View(transactionResponseModel));
            }
           
            return viewComponent;
        }



    }
}
