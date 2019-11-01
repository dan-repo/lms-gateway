using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Models;
using LmsGateway.Paystack.Domain;
using LmsGateway.Paystack.Settings;
using Microsoft.AspNetCore.Http;
using LmsGateway.Core.Payments;

namespace LmsGateway.Paystack.Interfaces
{
    public interface IGatewayLuncher
    {
        bool HasLunchedPaymentPage { get; set; }
        bool IsSuccessful { get; set; }
        string ErrorMessage { get; }
        string GatewayResponse { get; }
        string SelectedCurrencyId { get; }
        string PaystackSettings { get; }
        string TransactionRef { get; set; }

        Task<string> CreateTransactionRef(string transactionReferencePrefix);
        Task LunchPaymentPage(HttpContext httpContext, string authorizationUrl);
        Task<PaystackTransaction> GetTransaction(PaystackSetting paystackSettings, int tranxid);
        Task<PaystackTransaction> VerifyTransaction(PaystackSetting paystackSettings, string tranxid);
        Task<PaystackTransactionList> GetTransactionList(PaystackSetting paystackSettings, TransactionSearchModel searchModel);
        Task<PaystackTransactionList> FetchSettlements(PaystackSetting paystackSettings, TransactionSearchModel searchModel);
        Task<PaystackTransaction> GetTransactionTotal(PaystackSetting paystackSettings, TransactionSearchModel searchModel);
        Task<PaystackTransaction> MakePayment(ProcessPaymentRequest processPaymentRequest, PaystackSetting paystackSetting, PaystackSupportedCurrency supportedCurrency, HttpContext httpContext);
        string GetRedirectUrl(HttpRequest request, string action, string controller, string area = null, int id = 0);
        Task LogTransaction(PaystackTransaction paystackTransaction);

    }
}
