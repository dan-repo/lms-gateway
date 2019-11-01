using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Domain;
using LmsGateway.Paystack.Models;
using LmsGateway.Core.Extensions;
using LmsGateway.Core.Infrastructure;

namespace LmsGateway.Paystack.Extensions
{
    public static class PaystackTransactionExtensions
    {
        public static PaystackTransactionLog ToTransactionLog(this PaystackTransaction paystackTransaction)
        {
            if (paystackTransaction == null || paystackTransaction.Data == null)
                throw new ArgumentNullException("paystackTransaction");

            PaystackTransactionLog transactionLog = new PaystackTransactionLog();
            transactionLog = ToTransactionLogHelper(paystackTransaction, transactionLog);

            return transactionLog;
        }

        public static PaystackTransactionLog ToTransactionLog(this PaystackTransaction paystackTransaction, PaystackTransactionLog transactionLog)
        {
            if (paystackTransaction == null || paystackTransaction.Data == null)
                throw new ArgumentNullException("paystackTransaction");

            if (transactionLog == null || transactionLog.Id <= 0)
                throw new ArgumentNullException("transactionLog");

            return ToTransactionLogHelper(paystackTransaction, transactionLog);
        }

        public static IList<TransactionLogModel> ToModel(this IList<PaystackTransactionLog> transactionLogs)
        {
            //if (transactionLogs.IsNullOrEmpty())
            //{
            //    throw new ArgumentNullException("transactionLogs");
            //}

            Guard.NotEmpty(transactionLogs, nameof(transactionLogs));

            IList<TransactionLogModel> transactionLogModels = new List<TransactionLogModel>();
            foreach (PaystackTransactionLog transactionLog in transactionLogs)
            {
                TransactionLogModel transactionLogModel = new TransactionLogModel();
                transactionLogModel.Id = transactionLog.Id;
                transactionLogModel.RegistrationId = transactionLog.RegistrationId;
                transactionLogModel.Amount = transactionLog.Amount;
                transactionLogModel.Currency = transactionLog.Currency;
                transactionLogModel.TransactionDate = transactionLog.TransactionDate;
                transactionLogModel.Status = transactionLog.Status;
                transactionLogModel.Reference = transactionLog.Reference;
                transactionLogModel.Domain = transactionLog.Domain;
                transactionLogModel.GatewayResponse = transactionLog.GatewayResponse;
                transactionLogModel.IPAddress = transactionLog.IPAddress;
                transactionLogModel.Fees = transactionLog.Fees;
                transactionLogModel.AuthorizationUrl = transactionLog.AuthorizationUrl;
                transactionLogModel.AccessCode = transactionLog.AccessCode;
                transactionLogModel.AuthorizationCode = transactionLog.AuthorizationCode;
                transactionLogModel.CardType = transactionLog.CardType;
                transactionLogModel.Last4 = transactionLog.Last4;
                transactionLogModel.ExpiryMonth = transactionLog.ExpiryMonth;
                transactionLogModel.ExpiryYear = transactionLog.ExpiryYear;
                transactionLogModel.Bin = transactionLog.Bin;
                transactionLogModel.Bank = transactionLog.Bank;
                transactionLogModel.Channel = transactionLog.Channel;
                transactionLogModel.Signature = transactionLog.Signature;
                transactionLogModel.Brand = transactionLog.Brand;
                transactionLogModel.Reusable = transactionLog.Reusable;
                transactionLogModel.CountryCode = transactionLog.CountryCode;
                transactionLogModel.Message = transactionLog.Message;

                transactionLogModels.Add(transactionLogModel);
            }

            return transactionLogModels;
        }

        public static IList<PaystackTransactionLog> ToTransactionLog(this IList<TransactionLogModel> transactionLogModels, bool includeId)
        {
            //if (transactionLogModels.IsNullOrEmpty())
            //{
            //    throw new ArgumentNullException("transactionLogModels");
            //}

            Guard.NotEmpty(transactionLogModels, nameof(transactionLogModels));

            IList<PaystackTransactionLog> transactionLogs = new List<PaystackTransactionLog>();
            foreach (TransactionLogModel transactionLogModel in transactionLogModels)
            {
                PaystackTransactionLog transactionLog = new PaystackTransactionLog();
                if (includeId)
                {
                    transactionLog.Id = transactionLogModel.Id;
                }

                transactionLog.RegistrationId = transactionLogModel.RegistrationId;
                transactionLog.Amount = transactionLogModel.Amount;
                transactionLog.Currency = transactionLogModel.Currency;
                transactionLog.TransactionDate = transactionLogModel.TransactionDate;
                transactionLog.Status = transactionLogModel.Status;
                transactionLog.Reference = transactionLogModel.Reference;
                transactionLog.Domain = transactionLogModel.Domain;
                transactionLog.GatewayResponse = transactionLogModel.GatewayResponse;
                transactionLog.IPAddress = transactionLogModel.IPAddress;
                transactionLog.Fees = transactionLogModel.Fees;
                transactionLog.AuthorizationUrl = transactionLogModel.AuthorizationUrl;
                transactionLog.AccessCode = transactionLogModel.AccessCode;
                transactionLog.AuthorizationCode = transactionLogModel.AuthorizationCode;
                transactionLog.CardType = transactionLogModel.CardType;
                transactionLog.Last4 = transactionLogModel.Last4;
                transactionLog.ExpiryMonth = transactionLogModel.ExpiryMonth;
                transactionLog.ExpiryYear = transactionLogModel.ExpiryYear;
                transactionLog.Bin = transactionLogModel.Bin;
                transactionLog.Bank = transactionLogModel.Bank;
                transactionLog.Channel = transactionLogModel.Channel;
                transactionLog.Signature = transactionLogModel.Signature;
                transactionLog.Brand = transactionLogModel.Brand;
                transactionLog.Reusable = transactionLogModel.Reusable;
                transactionLog.CountryCode = transactionLogModel.CountryCode;
                transactionLog.Message = transactionLogModel.Message;

                transactionLogs.Add(transactionLog);
            }

            return transactionLogs;
        }

        private static PaystackTransactionLog ToTransactionLogHelper(PaystackTransaction paystackTransaction, PaystackTransactionLog transactionLog)
        {
            transactionLog.Message = paystackTransaction.message;
            transactionLog.RegistrationId = paystackTransaction.RegisterationId.GetValueOrDefault();

            if (paystackTransaction.Data != null)
            {
                transactionLog.Amount = paystackTransaction.Data.amount;
                transactionLog.Currency = paystackTransaction.Data.currency;
                transactionLog.TransactionDate = paystackTransaction.Data.transaction_date;
                transactionLog.Status = paystackTransaction.Data.status;
                transactionLog.Reference = paystackTransaction.Data.reference;
                transactionLog.Domain = paystackTransaction.Data.domain;
                transactionLog.GatewayResponse = paystackTransaction.Data.gateway_response;
                transactionLog.IPAddress = paystackTransaction.Data.ip_address;
                transactionLog.Fees = paystackTransaction.Data.fees;

                if (!paystackTransaction.Data.authorization_url.IsEmpty())
                {
                    transactionLog.AuthorizationUrl = paystackTransaction.Data.authorization_url;
                }
                if (!paystackTransaction.Data.access_code.IsEmpty())
                {
                    transactionLog.AccessCode = paystackTransaction.Data.access_code;
                }
            }

            if (paystackTransaction.Data.authorization != null)
            {
                transactionLog.AuthorizationCode = paystackTransaction.Data.authorization.authorization_code;
                transactionLog.CardType = paystackTransaction.Data.authorization.card_type;
                transactionLog.Last4 = paystackTransaction.Data.authorization.last4;
                transactionLog.ExpiryMonth = paystackTransaction.Data.authorization.exp_month;
                transactionLog.ExpiryYear = paystackTransaction.Data.authorization.exp_year;
                transactionLog.Bin = paystackTransaction.Data.authorization.bin;
                transactionLog.Bank = paystackTransaction.Data.authorization.bank;
                transactionLog.Channel = paystackTransaction.Data.authorization.channel;
                transactionLog.Signature = paystackTransaction.Data.authorization.signature;
                transactionLog.Brand = paystackTransaction.Data.authorization.brand;
                transactionLog.Reusable = paystackTransaction.Data.authorization.reusable;
                transactionLog.CountryCode = paystackTransaction.Data.authorization.country_code;
            }

            return transactionLog;
        }



    }
}
