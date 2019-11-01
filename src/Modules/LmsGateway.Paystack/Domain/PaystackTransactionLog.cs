using System;
using LmsGateway.Domain;

namespace LmsGateway.Paystack.Domain
{
    public class PaystackTransactionLog : BaseEntity
    {
        public int RegistrationId { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public string Domain { get; set; }
        public string GatewayResponse { get; set; }
        public string Message { get; set; }
        public string IPAddress { get; set; }

        public int? Fees { get; set; }

        public string AuthorizationCode { get; set; }
        public string CardType { get; set; }
        public string Last4 { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Bin { get; set; }
        public string Bank { get; set; }
        public string Channel { get; set; }
        public string Signature { get; set; }
        public string Brand { get; set; }
        public bool? Reusable { get; set; }
        public string CountryCode { get; set; }

        public string AuthorizationUrl { get; set; }
        public string AccessCode { get; set; }

    }
}
