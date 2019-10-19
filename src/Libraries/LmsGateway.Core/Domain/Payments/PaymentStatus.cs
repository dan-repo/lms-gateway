using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Domain.Payments
{
    public enum PaymentStatus
    {
        Pending = 1,
        Authorized = 2,
        Paid = 3,
        PartiallyRefunded = 3,
        Refunded = 4,
        Voided = 5,
    }




}
