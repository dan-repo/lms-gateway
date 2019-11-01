using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class RegistrationPeriod : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public int SessionId { get; set; }
        public int SemesterId { get; set; }

        public virtual Session Session { get; set; }
        public virtual Semester Semester { get; set; }

        public List<RegistrationFee> RegistrationFees { get; set; }

    }




}
