using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Paystack.Models
{
    public class SupportedCurrencyModel
    {
        public int Id { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.SupportedCurrency.Fields.Code")]
        public int Code { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.SupportedCurrency.Fields.Alias")]
        public string Alias { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.Name")]
        public string Name { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.Description")]
        public string Description { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.SupportedCurrency.Fields.LeastValueUnitMultiplier")]
        public int LeastValueUnitMultiplier { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.SupportedCurrency.Fields.IsSupported")]
        public bool IsSupported { get; set; }

    }
}
