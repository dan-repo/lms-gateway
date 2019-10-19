
using LmsGateway.Core.Configuration;
using System.Collections.Generic;

namespace LmsGateway.Core.Domain.Payments
{
    public class PaymentSettings : ISetting
    {
        public PaymentSettings()
        {
            ActivePaymentMethodSystemNames = new List<string>();
            BypassPaymentMethodSelectionIfOnlyOne = false;
            //AllowRePostingPayments = true;
        }

        public bool BypassPaymentMethodSelectionIfOnlyOne { get; set; }
        public List<string> ActivePaymentMethodSystemNames { get; set; }
        
    }


}
