using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class Student //: BaseEntity
    {
        public string Id { get; set; }
        public string RegNo { get; set; }
        public StudentStatus Status { get; set; }

        public int ProgrammeId { get; set; }
        public int DepartmentId { get; set; }
        public int CurrentLevelId { get; set; }
        public int? AdmissionListDetailId { get; set; }
        public DateTime CreatedOn { get; set; }

        public Programme Programme { get; set; }
        public Department Department { get; set; }
        public Level CurrentLevel { get; set; }
        public AdmissionListDetail AdmissionListDetail { get; set; }

        public List<StudentLevel> LevelHistory { get; set; }
    }



}
