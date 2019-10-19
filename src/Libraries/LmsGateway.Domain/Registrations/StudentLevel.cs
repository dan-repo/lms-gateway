using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Domain.Registrations
{
    public class StudentLevel : BaseEntity
    {
        public string UserId { get; set; }

        public int LevelId { get; set; }

        public Level Level { get; set; }
    }



}
