using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class RegistrationDetail : BaseEntity
    {
        public int RegistrationId { get; set; }

        public int PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime RegisteredOn { get; set; }

        public Registration Registration { get; set; }

    }
}
