using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Paystack.Models
{
    public class TransactionStatusModel
    {
        public int Id { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.Name")]
        public string Name { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.Description")]
        public string Description { get; set; }

    }
}
