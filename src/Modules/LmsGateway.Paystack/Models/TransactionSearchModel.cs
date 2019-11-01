using System;
using System.Collections.Generic;
using LmsGateway.Paystack.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LmsGateway.Paystack.Models
{
    public class TransactionSearchModel
    {
        public TransactionSearchModel()
        {
            TransactionStatuses = new List<SelectListItem>();
        }

        public int PerPage { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.TransactionSearchModel.Fields.From")]
        public DateTime From { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.TransactionSearchModel.Fields.To")]
        public DateTime To { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.TransactionSearchModel.Fields.DateFrom")]
        public DateTime DateFrom { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.TransactionSearchModel.Fields.DateTo")]
        public DateTime DateTo { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.TransactionSearchModel.Fields.Amount")]
        public int Amount { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.TransactionSearchModel.Fields.TransactionStatus")]
        public string TransactionStatus { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.TransactionSearchModel.Fields.TransactionStatus")]
        public IList<SelectListItem> TransactionStatuses { get; set; }

        public PaystackTransactionStatus PaystackTransactionStatus { get; set; }
        public List<PaystackTransactionStatus> PaystackTransactionStatusList { get; set; }




    }
}
