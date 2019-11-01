using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class StudentLevel : BaseEntity
    {
        public string StudentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public int LevelId { get; set; }

        public Student Student { get; set; }
        public Level Level { get; set; }
    }



}
