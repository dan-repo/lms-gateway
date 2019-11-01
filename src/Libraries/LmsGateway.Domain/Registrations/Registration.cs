using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class Registration : BaseEntity
    {
        public string StudentId { get; set; }
        
        public int RegistrationPeriodId { get; set; }
        public int RegistrationFeeId { get; set; }
        public DateTime RegisteredOn { get; set; }

        public RegistrationPeriod RegistrationPeriod { get; set; }
        public RegistrationFee RegistrationFee { get; set; }
        public virtual List<RegistrationDetail> Details { get; set; }

    }



}
