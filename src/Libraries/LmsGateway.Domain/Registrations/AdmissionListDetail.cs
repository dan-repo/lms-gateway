using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class AdmissionListDetail : BaseEntity
    {
        public string RegNo { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string OtherName { get; set; }

        public int AdmissionListId { get; set; }
        public int LevelId { get; set; }
        public int ProgrammeId { get; set; }
        public int DepartmentId { get; set; }
        public int SessionId { get; set; }

        public AdmissionList AdmissionList { get; set; }
        public Level Level { get; set; }
        public Programme Programme { get; set; }
        public Department Department { get; set; }
        
    }




}
