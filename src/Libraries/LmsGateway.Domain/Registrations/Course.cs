using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class Course : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public bool IsOptional { get; set; }

        public int LevelId { get; set; }
        public int SemesterId { get; set; }
        public int ProgrammeId { get; set; }
        public int DepartmentId { get; set; }

        public Level Level { get; set; }
        public Semester Semester { get; set; }
        public Programme Programme { get; set; }
        public Department Department { get; set; }
    }




}
