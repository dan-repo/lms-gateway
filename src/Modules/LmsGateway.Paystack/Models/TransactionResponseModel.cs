using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Paystack.Models
{
    public class TransactionResponseModel
    {
        public string ThankYou { get; set; }
        public string AlertType { get; set; }
        public string BorderColor { get; set; }
        public string TransactionSummary { get; set; }
        public string GatewayResponse { get; set; }
        public string TransactionReference { get; set; }
        public string TransactionStatus { get; set; }
        public string Currency { get; set; }
        public Data ResponseData { get; set; }
        public string HomePageUrl { get; set; }
        public bool PaymentSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }



}
