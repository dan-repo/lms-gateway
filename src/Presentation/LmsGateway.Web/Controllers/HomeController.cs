using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LmsGateway.Web.Models;
using LmsGateway.Core.Notifications;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Core.Extensions;

namespace LmsGateway.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly EmailServer _emailServer;

        public HomeController(IEmailService emailService, EmailServer emailServer)
        {
            Guard.NotNull(emailService, nameof(emailService));
            Guard.NotNull(emailServer, nameof(emailServer));

            _emailServer = emailServer;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }
       
        public async Task<IActionResult> About() => await Task.FromResult(View());
       
        public async Task<IActionResult> Contact() => await Task.FromResult(View(new ContactFormModel()));
      
        public async Task<IActionResult> Error() => await Task.FromResult(View());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitEnquiry(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Contact));
            }

            Email email = new Email();
            email.FromEmailAddress = new EmailAddress() { Name = model.Name, Email = model.Email };
            email.ToEmailAddress = new EmailAddress() { Name = _emailServer.Name, Email = _emailServer.Username };
            email.Message = model.Comment;
            //mail.Subject = model.Subject;

            string error = null;
            string message = null;

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                error = "Error occurred!\n" + ex.Message;
            }

            message = error.IsEmpty() ? "Your message has been successfully posted." : error;

            return Json(message);
        }

        //public async Task<Paystack.Models.PaystackTransaction> MakePayment()
        //{
        //    try
        //    {
        //        Microsoft.AspNetCore.Http.HttpContext httpContext = null;
        //        Paystack.Settings.PaystackSetting paystackSetting = null;

        //        string reference = processPaymentRequest.TransactionReference; //httpContext.Session[TransactionRef] as string;
        //        string apiEndPoint = paystackSetting.InitializeTransactionEndPoint;
        //        string callbackUrl = GetRedirectUrl(httpContext.Request, "Details", "Order", processPaymentRequest.Registration.Id);
        //        //OrderTotal = processPaymentRequest.Registration.Details.Sum(x => x.AmountPaid); // Math.Truncate(processPaymentRequest.Order.OrderTotal * currency.LeastValueUnitMultiplier);

        //        RestRequest request = new RestRequest(apiEndPoint, Method.POST);
        //        request.AddHeader(paystackSetting.APIAcceptHeaderKey, paystackSetting.APIAcceptHeaderValue);
        //        request.AddHeader(paystackSetting.APIAuthorizationHeaderKey, paystackSetting.APIAuthorizationHeaderValue + " " + paystackSetting.Key);
        //        request.AddParameter(paystackSetting.APIReferenceParameterKey, reference);
        //        request.AddParameter(paystackSetting.APIAmountParameterKey, 125200);
        //        request.AddParameter(paystackSetting.APICallbackUrlParameterKey, callbackUrl);



        //        //request.AddParameter(paystackSetting.APIEmailParameterKey, postProcessPaymentRequest.Order.Customer.BillingAddress.Email);

        //        //Dictionary<string, List<CustomField>> metadata = new Dictionary<string, List<CustomField>>();
        //        //List<CustomField> customfields = BuildCustomField(postProcessPaymentRequest.Registration.BillingAddress);
        //        //metadata.Add(paystackSetting.APICustomFieldsParameterKey, customfields);

        //        //var javaScriptSerializer = new JavaScriptSerializer();
        //        //var serializedMetadata = javaScriptSerializer.Serialize(metadata);
        //        //request.AddParameter(paystackSetting.APIMetaDataParameterKey, serializedMetadata);

        //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        //RestClient client = new RestClient(paystackSetting.ApiBaseUrl);
        //        //IRestResponse response = client.Execute(request);
        //        //if (response.ErrorException != null)
        //        //{
        //        //    throw response.ErrorException;
        //        //}

        //        //return JsonConvert.DeserializeObject<PaystackTransaction>(response.Content);



        //        return await Post(paystackSetting, request);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}




    }
}
