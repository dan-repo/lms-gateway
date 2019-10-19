using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class Registration : BaseEntity
    {
        public int PeriodId { get; set; }
        public string StudentId { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime RegisteredOn { get; set; }

        public Session Session { get; set; }
        public Semester Semester { get; set; }
        public RegistrationPeriod Period { get; set; }
         

    }
}
