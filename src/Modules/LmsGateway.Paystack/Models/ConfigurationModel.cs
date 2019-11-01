using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Settings;

namespace LmsGateway.Paystack.Models
{
    public class ConfigurationModel
    {
        public ConfigurationModel()
        {
            PaystackSetting = new PaystackSetting();
            TransactionSearchModel = new TransactionSearchModel();
            PaystackTransaction = new PaystackTransaction();
            PaystackTransaction.Data = new Data();
        }

        public bool IsAdmin { get; set; }
        public PaystackSetting PaystackSetting { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.TransactionId")]
        public string TransactionId { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.TransactionReference")]
        public string TransactionReference { get; set; }

        public PaystackTransaction PaystackTransaction { get; set; }
        public TransactionSearchModel TransactionSearchModel { get; set; }

        public List<Data> Data { get; set; }


    }
}
