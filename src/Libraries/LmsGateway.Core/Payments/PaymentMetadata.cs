using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Payments
{
    public class PaymentMetadata
    {
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Author { get; set; }
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public string Category { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }

    }


}
