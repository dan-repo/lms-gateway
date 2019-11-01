using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class RegistrationFee : BaseEntity
    {
        public decimal AmountPayable { get; set; }
        public decimal AccessCharge { get; set; }
        public bool? CanMakePartPayment { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public int RegistrationPeriodId { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }
        public int ProgrammeId { get; set; }

        public virtual Level Level { get; set; }
        public virtual Department Department { get; set; }
        public virtual RegistrationPeriod RegistrationPeriod { get; set; }
        public virtual Programme Programme { get; set; }

    }






}
