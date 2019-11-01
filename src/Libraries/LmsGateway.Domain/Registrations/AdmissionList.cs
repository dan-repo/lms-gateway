using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain.Medias;

namespace LmsGateway.Domain.Registrations
{
    public class AdmissionList : BaseEntity
    {
        public int SessionId { get; set; }
        public int? MediaId { get; set; }

        public DateTime DatePosted { get; set; }
        public string PostedBy { get; set; }
        
        public Session Session { get; set; }
        public Media Media { get; set; }

    }
}
