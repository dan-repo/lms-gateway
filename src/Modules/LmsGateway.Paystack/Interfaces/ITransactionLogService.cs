using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Domain;

namespace LmsGateway.Paystack.Interfaces
{
    public interface ITransactionLogService
    {
        Task<List<PaystackTransactionLog>> GetAllAsync();
        List<PaystackTransactionLog> GetLatest500Transactions();
        Task<bool> TransactionReferenceExistAsync(string transactionReference);
        Task SaveAsync(PaystackTransactionLog paystackTransactionLog);
        void Update(PaystackTransactionLog paystackTransactionLog);
        Task AddRangeAsync(List<PaystackTransactionLog> paystackTransactionLogs);
        Task<PaystackTransactionLog> GetByReferenceAsync(string transactionReference);
        Task<PaystackTransactionLog> GetByAsync(int transactionId);
        void DeleteAll(IList<PaystackTransactionLog> transactionLogs);

    }
}
