using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class RegistrationFee : BaseEntity
    {
        public decimal AmountPayable { get; set; }
        public bool? CanMakePartPayment { get; set; }

        public int PeriodId { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }

        public Level Level { get; set; }
        public Department Department { get; set; }
        public RegistrationPeriod Period { get; set; }

    }






}
