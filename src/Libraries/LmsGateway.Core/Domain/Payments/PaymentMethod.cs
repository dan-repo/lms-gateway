using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Domain.Payments
{
    public class PaymentMethod
    {
        public string SystemName { get; set; }
        public string Description { get; set; }
    }
}
