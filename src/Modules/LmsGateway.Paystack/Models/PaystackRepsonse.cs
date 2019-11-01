using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Paystack.Models
{
    public class PaystackRepsonseBase
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Meta meta { get; set; }
    }
    public class PaystackTransactionList : PaystackRepsonseBase
    {
        public List<Data> data { get; set; }
    }
    public class PaystackTransaction : PaystackRepsonseBase
    {
        public int? RegisterationId { get; set; }
        public Data Data { get; set; }
    }
    public class Customer
    {
        public int id { get; set; }
        public string customer_code { get; set; }
        public object first_name { get; set; }
        public object last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public object metadata { get; set; }
        public string risk_action { get; set; }
    }
    public class Meta
    {
        public int meta { get; set; }
        public int total { get; set; }
        public decimal total_volume { get; set; }
        public int skipped { get; set; }
        public int perPage { get; set; }
        public int page { get; set; }
        public int pageCount { get; set; }
    }
    public class Metadata
    {
        public string referrer { get; set; }
    }
    public class CustomField
    {
        public string display_name { get; set; }
        public string variable_name { get; set; }
        public string value { get; set; }
    }
    public class Data
    {
        public int id { get; set; }
        public int? amount { get; set; }
        public string currency { get; set; }
        public DateTime? transaction_date { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public string domain { get; set; }
        public Metadata metadata { get; set; }
        public string gateway_response { get; set; }
        public object paid_at { get; set; }
        public string created_at { get; set; }
        public object message { get; set; }
        public string channel { get; set; }
        public string ip_address { get; set; }
        public Log log { get; set; }
        public int? fees { get; set; }
        public LmsGateway.Paystack.Models.Authorization authorization { get; set; }
        public Customer customer { get; set; }
        public object plan { get; set; }
        public string authorization_url { get; set; }
        public string access_code { get; set; }


        public int? total_transactions { get; set; }
        public int? unique_customers { get; set; }
        public decimal? total_volume { get; set; }
        public int? pending_transfers { get; set; }
        public List<TotalVolume> total_volume_by_currency { get; set; }
        public List<TotalVolume> pending_transfers_by_currency { get; set; }


        public int? integration { get; set; }
        public string settled_by { get; set; }
        public string settlement_date { get; set; }
        public decimal? total_amount { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public SubAccount subaccount { get; set; }
    }
    public class TotalVolume
    {
        public string currency { get; set; }
        public decimal? amount { get; set; }
    }
    public class SubAccount
    {
        public string domain { get; set; }
        public string subaccount_code { get; set; }
        public string business_name { get; set; }
        public string description { get; set; }
        public string primary_contact_name { get; set; }
        public string primary_contact_email { get; set; }
        public string primary_contact_phone { get; set; }
        public string metadata { get; set; }
        public int? percentage_charge { get; set; }
        public bool? is_verified { get; set; }
        public string settlement_bank { get; set; }
        public string account_number { get; set; }
        public string settlement_schedule { get; set; }
        public bool? active { get; set; }
        public string migrate { get; set; }
        public int? id { get; set; }
        public int? integration { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }

    //public class pending_transfers_by_currency
    //{
    //    public string currency { get; set; }
    //    public decimal? amount { get; set; }
    //}

    public class Authorization
    {
        public string authorization_code { get; set; }
        public string card_type { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string bin { get; set; }
        public string bank { get; set; }
        public string channel { get; set; }
        public string signature { get; set; }
        public bool reusable { get; set; }
        public string country_code { get; set; }
        public string brand { get; set; }
    }
    public class Log
    {
        public int time_spent { get; set; }
        public int attempts { get; set; }
        public object authentication { get; set; }
        public int errors { get; set; }
        public bool success { get; set; }
        public bool mobile { get; set; }
        public List<object> input { get; set; }
        public object channel { get; set; }
        public List<History> history { get; set; }
    }
    public class History
    {
        public string type { get; set; }
        public string message { get; set; }
        public int time { get; set; }
    }

}
