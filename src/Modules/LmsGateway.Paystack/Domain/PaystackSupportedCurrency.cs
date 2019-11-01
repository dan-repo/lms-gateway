using LmsGateway.Domain;

namespace LmsGateway.Paystack.Domain
{
    public class PaystackSupportedCurrency : BaseEntity
    {
        public int Code { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LeastValueUnitMultiplier { get; set; }
        public bool IsSupported { get; set; }

    }




}
