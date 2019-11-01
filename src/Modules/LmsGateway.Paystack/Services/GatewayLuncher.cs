using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Interfaces;
using LmsGateway.Paystack.Models;
using LmsGateway.Paystack.Settings;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using LmsGateway.Paystack.Domain;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Paystack.Extensions;
using LmsGateway.Core.Payments;
using LmsGateway.Core.Extensions;
using RestSharp;

namespace LmsGateway.Paystack.Services
{
    public class GatewayLuncher : IGatewayLuncher
    {
        private int noOfTrial;
        private readonly ITransactionLogService _transactionLogService;

        private string _gateway_response = "gateway_response";
        private string _selected_currency_id = "selected_currency_id";
        private string _paystack_settings = "PaystackSettings";
        private string _errorMessage = "errorMessage";

        public GatewayLuncher(ITransactionLogService transactionLogService)
        {
            Guard.NotNull(transactionLogService, nameof(transactionLogService));

            _transactionLogService = transactionLogService;
        }

        public bool HasLunchedPaymentPage { get; set; }
        public bool IsSuccessful { get; set; }
        public static int RegistrationId { get; set; }
        public static decimal OrderTotal { get; set; }
        public string TransactionRef { get; set; }
        public string ErrorMessage { get { return _errorMessage; } }
        public string SelectedCurrencyId { get { return _selected_currency_id; } }
        public string PaystackSettings { get { return _paystack_settings; } }
        public string GatewayResponse { get { return _gateway_response; } }

        public async Task<PaystackTransaction> GetTransactionTotal(PaystackSetting paystackSettings, TransactionSearchModel searchModel)
        {
            Guard.NotNull(paystackSettings, nameof(paystackSettings));


            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiEndPoint = paystackSettings.TransactionTotalEndPoint;

            RestRequest request = new RestRequest(apiEndPoint, Method.GET);
            request.AddHeader(paystackSettings.APIAcceptHeaderKey, paystackSettings.APIAcceptHeaderValue);
            request.AddHeader(paystackSettings.APIAuthorizationHeaderKey, paystackSettings.APIAuthorizationHeaderValue + " " + paystackSettings.Key);

            if (searchModel != null)
            {
                if (searchModel.DateFrom > DateTime.MinValue)
                {
                    request.AddQueryParameter(paystackSettings.APIFromQueryParameterKey, searchModel.DateFrom.ToString(paystackSettings.TransactionSearchDateFormat));
                }
                if (searchModel.DateTo > DateTime.MinValue)
                {
                    request.AddQueryParameter(paystackSettings.APIToQueryParameterKey, searchModel.DateTo.AddDays(1).ToString(paystackSettings.TransactionSearchDateFormat));
                }
            }

            //RestClient client = new RestClient(paystackSettings.ApiBaseUrl);
            //var transactionResponse = client.Execute(request);
            //return JsonConvert.DeserializeObject<PaystackTransaction>(transactionResponse.Content);

            return await Post(paystackSettings, request);
        }

        public async Task<PaystackTransactionList> GetTransactionList(PaystackSetting paystackSettings, TransactionSearchModel searchModel)
        {
            Guard.NotNull(paystackSettings, nameof(paystackSettings));


            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiEndPoint = paystackSettings.ListTransactionEndPoint;
            RestRequest request = new RestRequest(apiEndPoint, Method.GET);
            request.AddHeader(paystackSettings.APIAcceptHeaderKey, paystackSettings.APIAcceptHeaderValue);
            request.AddHeader(paystackSettings.APIAuthorizationHeaderKey, paystackSettings.APIAuthorizationHeaderValue + " " + paystackSettings.Key);

            if (searchModel != null)
            {
                request.AddQueryParameter(paystackSettings.APIStatusQueryParameterKey, searchModel.TransactionStatus);

                if (searchModel.From > DateTime.MinValue)
                {
                    request.AddQueryParameter(paystackSettings.APIFromQueryParameterKey, searchModel.From.ToString(paystackSettings.TransactionSearchDateFormat));
                }
                if (searchModel.To > DateTime.MinValue)
                {
                    request.AddQueryParameter(paystackSettings.APIToQueryParameterKey, searchModel.To.AddDays(1).ToString(paystackSettings.TransactionSearchDateFormat));
                }
                if (searchModel.Amount > 0)
                {
                    request.AddQueryParameter(paystackSettings.APIAmountQueryParameterKey, searchModel.Amount.ToString());
                }
            }

            //RestClient client = new RestClient(paystackSettings.ApiBaseUrl);
            //var transactionResponse = client.Execute(request);
            //return JsonConvert.DeserializeObject<PaystackTransactionList>(transactionResponse.Content);

            return await PostRequest(paystackSettings, request);
        }

        public async Task<PaystackTransaction> VerifyTransaction(PaystackSetting paystackSettings, string tranxid)
        {
            Guard.NotNull(paystackSettings, nameof(paystackSettings));

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiEndPoint = paystackSettings.VerifyTransactionEndPoint + tranxid;
            RestRequest request = new RestRequest(apiEndPoint, Method.GET);
            request.AddHeader(paystackSettings.APIAcceptHeaderKey, paystackSettings.APIAcceptHeaderValue);
            request.AddHeader(paystackSettings.APIAuthorizationHeaderKey, paystackSettings.APIAuthorizationHeaderValue + " " + paystackSettings.Key);

            //RestClient client = new RestClient(paystackSettings.ApiBaseUrl);
            //var transactionResponse = client.Execute(request);
            //return JsonConvert.DeserializeObject<PaystackTransaction>(transactionResponse.Content);

            return await Post(paystackSettings, request);
        }

        public async Task<PaystackTransaction> GetTransaction(PaystackSetting paystackSetting, int tranxid)
        {
            try
            {
                Guard.NotNull(paystackSetting, nameof(paystackSetting));

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string apiEndPoint = paystackSetting.FetchTransactionEndPoint + tranxid;

                RestRequest request = new RestRequest(apiEndPoint, Method.GET);
                request.AddHeader(paystackSetting.APIAcceptHeaderKey, paystackSetting.APIAcceptHeaderValue);
                request.AddHeader(paystackSetting.APIAuthorizationHeaderKey, paystackSetting.APIAuthorizationHeaderValue + " " + paystackSetting.Key);

                //RestClient client = new RestClient(paystackSetting.ApiBaseUrl);
                //var result = client.Execute(request);
                //PaystackTransaction paystackRepsonse = JsonConvert.DeserializeObject<PaystackTransaction>(result.Content);
                //return paystackRepsonse;

                return await Post(paystackSetting, request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaystackTransactionList> FetchSettlements(PaystackSetting paystackSettings, TransactionSearchModel searchModel)
        {
            Guard.NotNull(paystackSettings, nameof(paystackSettings));

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiEndPoint = paystackSettings.FetchSettlementsEndPoint;

            RestRequest request = new RestRequest(apiEndPoint, Method.GET);
            request.AddHeader(paystackSettings.APIAcceptHeaderKey, paystackSettings.APIAcceptHeaderValue);
            request.AddHeader(paystackSettings.APIAuthorizationHeaderKey, paystackSettings.APIAuthorizationHeaderValue + " " + paystackSettings.Key);

            if (searchModel != null)
            {
                if (searchModel.DateFrom > DateTime.MinValue)
                {
                    request.AddQueryParameter(paystackSettings.APIFromQueryParameterKey, searchModel.DateFrom.ToString(paystackSettings.TransactionSearchDateFormat));
                }
                if (searchModel.DateTo > DateTime.MinValue)
                {
                    request.AddQueryParameter(paystackSettings.APIToQueryParameterKey, searchModel.DateTo.AddDays(1).ToString(paystackSettings.TransactionSearchDateFormat));
                }
            }

            //TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            //RestRequestAsyncHandle handle = restClient.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            //RestResponse response = (RestResponse)(await taskCompletion.Task);
            //return JsonConvert.DeserializeObject<SomeObject>(response.Content);

            return await PostRequest(paystackSettings, request);
        }

        private async Task<PaystackTransactionList> PostRequest(PaystackSetting paystackSettings, RestRequest request)
        {
            RestClient client = new RestClient(paystackSettings.ApiBaseUrl);
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle requestHandle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            RestResponse transactionResponse = (RestResponse)(await taskCompletion.Task);

            if (transactionResponse.ErrorException != null)
            {
                throw transactionResponse.ErrorException;
            }

            return JsonConvert.DeserializeObject<PaystackTransactionList>(transactionResponse.Content);
        }
        private async Task<PaystackTransaction> Post(PaystackSetting paystackSettings, RestRequest request)
        {
            RestClient client = new RestClient(paystackSettings.ApiBaseUrl);
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle requestHandle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            RestResponse transactionResponse = (RestResponse)(await taskCompletion.Task);

            if (transactionResponse.ErrorException != null)
            {
                throw transactionResponse.ErrorException;
            }

            return JsonConvert.DeserializeObject<PaystackTransaction>(transactionResponse.Content);
        }

        public async Task<PaystackTransaction> MakePayment(ProcessPaymentRequest processPaymentRequest, PaystackSetting paystackSetting, PaystackSupportedCurrency supportedCurrency, HttpContext httpContext)
        {
            try
            {
                RegistrationId = processPaymentRequest.Registration.Id;
                string reference = processPaymentRequest.TransactionReference; 
                string apiEndPoint = paystackSetting.InitializeTransactionEndPoint;
                string callbackUrl = GetRedirectUrl(httpContext.Request, "Completed", "Registration", "Student", RegistrationId);
                OrderTotal = Math.Truncate(processPaymentRequest.AmountPayable * supportedCurrency.LeastValueUnitMultiplier);

                RestRequest request = new RestRequest(apiEndPoint, Method.POST);
                request.AddHeader(paystackSetting.APIAcceptHeaderKey, paystackSetting.APIAcceptHeaderValue);
                request.AddHeader(paystackSetting.APIAuthorizationHeaderKey, paystackSetting.APIAuthorizationHeaderValue + " " + paystackSetting.Key);
                request.AddParameter(paystackSetting.APIReferenceParameterKey, reference);
                request.AddParameter(paystackSetting.APIAmountParameterKey, OrderTotal);
                request.AddParameter(paystackSetting.APICallbackUrlParameterKey, callbackUrl);
                request.AddParameter(paystackSetting.APIEmailParameterKey, httpContext.User.Identity.Name);
               
                return await Post(paystackSetting, request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LunchPaymentPage(HttpContext httpContext, string authorizationUrl)
        {
            Microsoft.AspNetCore.Http.HttpResponse response = httpContext.Response;
            response.Clear();

            StringBuilder form = new StringBuilder();
            form.Append("<html>");
            form.AppendFormat("<body onload='document.forms[0].submit()'>");
            form.AppendFormat("<form action='{0}' method='post'>", authorizationUrl);
            form.Append("</form>");
            form.Append("</body>");
            form.Append("</html>");

            HasLunchedPaymentPage = true;
            await response.WriteAsync(form.ToString());

            


            //response.Write(form.ToString());
            //response.End();
        }

        //private List<CustomField> BuildCustomField(Address billingAddress)
        //{
        //    List<CustomField> customfields = new List<CustomField>();
        //    CustomField nameCustomeField = new CustomField();
        //    nameCustomeField.display_name = "Name";
        //    nameCustomeField.variable_name = "name";
        //    nameCustomeField.value = GetCustomerName(billingAddress);
        //    customfields.Add(nameCustomeField);

        //    CustomField orderIdCustomeField = new CustomField();
        //    orderIdCustomeField.display_name = "Order ID";
        //    orderIdCustomeField.variable_name = "order_id";
        //    orderIdCustomeField.value = OrderId.ToString();
        //    customfields.Add(orderIdCustomeField);

        //    CustomField amountCustomeField = new CustomField();
        //    amountCustomeField.display_name = "Amount";
        //    amountCustomeField.variable_name = "amount";
        //    amountCustomeField.value = OrderTotal.ToString();
        //    customfields.Add(amountCustomeField);

        //    CustomField PhoneCustomeField = new CustomField();
        //    PhoneCustomeField.display_name = "Phone Number";
        //    PhoneCustomeField.variable_name = "phone_number";
        //    PhoneCustomeField.value = billingAddress.PhoneNumber;
        //    customfields.Add(PhoneCustomeField);

        //    CustomField addressCustomeField = new CustomField();
        //    addressCustomeField.display_name = "Address";
        //    addressCustomeField.variable_name = "address";
        //    addressCustomeField.value = GetCustomerAddress(billingAddress);
        //    customfields.Add(addressCustomeField);

        //    CustomField emailCustomeField = new CustomField();
        //    emailCustomeField.display_name = "Email";
        //    emailCustomeField.variable_name = "email";
        //    emailCustomeField.value = billingAddress.Email;
        //    customfields.Add(emailCustomeField);
        //    return customfields;
        //}



        //private string GetCustomerName(Address customerAddress)
        //{
        //    string name = null;

        //    if (customerAddress != null)
        //    {
        //        name = string.Format("{0} {1}", customerAddress.FirstName, customerAddress.LastName);
        //        name = name.Trim();
        //    }

        //    return name;
        //}

        //private string GetCustomerAddress(Address customerAddress)
        //{
        //    string address = null;

        //    if (customerAddress != null)
        //    {
        //        address = string.Format("{0}, {1}, {2}", customerAddress.Address1, customerAddress.Address2, customerAddress.City);
        //        address = address.Trim();
        //    }

        //    return address;
        //}

        public string GetRedirectUrl(HttpRequest request, string action, string controller, string area = null, int id = 0)
        {
            if (request == null)
            {
                return null;
            }
            
            string url = null;
            if (area.HasValue())
            {
                url = $"{request.Scheme}://{request.Host.Value}/{area}/{controller}/{action}";
            }
            else
            {
                url = $"{request.Scheme}://{request.Host.Value}/{controller}/{action}";
            }

            return url;
        }

        public async Task<string> CreateTransactionRef(string transactionReferencePrefix)
        {
            int maximumTrial = 20;
            string transactionRefNo = null;

            while (noOfTrial <= maximumTrial)
            {
                ++noOfTrial;
                string tranxRefNo = CreateTransactionRefHelper(transactionReferencePrefix);
                if (await _transactionLogService.TransactionReferenceExistAsync(tranxRefNo))
                {
                    await CreateTransactionRef(transactionReferencePrefix);
                }
                else
                {
                    transactionRefNo = tranxRefNo;
                    break;
                }
            }

            return transactionRefNo;
        }

        private string CreateTransactionRefHelper(string transactionReferencePrefix)
        {
            Random rng = new Random();
            StringBuilder builder = new StringBuilder();
            while (builder.Length < 20)
            {
                builder.Append(rng.Next(10).ToString());
            }

            return transactionReferencePrefix + builder.ToString();
        }

        public async Task LogTransaction(PaystackTransaction paystackTransaction)
        {
            if (paystackTransaction == null || paystackTransaction.Data == null)
            {
                throw new ArgumentNullException("paystackTransaction");
            }
            
            PaystackTransactionLog transactionLog = await _transactionLogService.GetByReferenceAsync(paystackTransaction.Data.reference);
            if (transactionLog == null || transactionLog.Id <= 0)
            {
                throw new ArgumentNullException("Transaction Log failed on Retrieval!");
            }

            paystackTransaction.RegisterationId = GatewayLuncher.RegistrationId;

            transactionLog = paystackTransaction.ToTransactionLog(transactionLog);
            _transactionLogService.Update(transactionLog);
        }





    }
}
