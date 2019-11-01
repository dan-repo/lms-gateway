using LmsGateway.Domain;

namespace LmsGateway.Paystack.Domain
{
    public class PaystackTransactionStatus : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
