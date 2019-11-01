using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain.Registrations;

namespace LmsGateway.Web.Areas.Student.Models
{
    public class RegistrationFeeModel
    {
        public RegistrationFeeModel()
        {
            Level = new Level();
            Department = new Department();
            RegistrationPeriod = new RegistrationPeriod();
            Programme = new Programme();
        }

        public int Id { get; set; }
        public string Name
        {
            get { return Programme.Name + " - " + Department.Name + " - " + Level.Name; }
        }

        public decimal AmountPayable { get; set; }
        public decimal AccessCharge { get; set; }
        public bool? CanMakePartPayment { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public int RegistrationPeriodId { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }
        public int ProgrammeId { get; set; }

        public Level Level { get; set; }
        public Department Department { get; set; }
        public RegistrationPeriod RegistrationPeriod { get; set; }
        public Programme Programme { get; set; }

    }
}
