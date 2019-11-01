using LmsGateway.Core.Domain.Payments;
using LmsGateway.Domain.Registrations;
using Microsoft.AspNetCore.Http;

namespace LmsGateway.Core.Payments
{
    public class ProcessPaymentRequest
    {
        public string RedirectUrl { get; set; }
        public Registration Registration { get; set; }
        public string TransactionReference { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentMethodName { get; set; }
        public int SelectedCurrencyId { get; set; }
        public HttpContext HttpContext { get; set; }
        public decimal AmountPayable { get; set; }

        //public bool IsRePostProcessPayment { get; set; }


    }
}
