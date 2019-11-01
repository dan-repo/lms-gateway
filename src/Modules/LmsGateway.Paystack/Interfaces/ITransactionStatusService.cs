
using LmsGateway.Paystack.Domain;
using System.Collections.Generic;

namespace LmsGateway.Paystack.Interfaces
{
    public interface ITransactionStatusService
    {
        //void Add(PaystackTransactionStatus transactionStatus);
        //void AddRange(List<PaystackTransactionStatus> transactionStatusList);
        //void UpdateTransactionStatus(PaystackTransactionStatus transactionStatus);
        //void DeleteTransactionStatus(PaystackTransactionStatus transactionStatus);
        //PaystackTransactionStatus GetTransactionStatusById(int id);

        List<PaystackTransactionStatus> GetAllTransactionStatus();

        //void DeleteAll();

    }
}
