using LmsGateway.Core.Data;
using LmsGateway.Paystack.Domain;
using LmsGateway.Paystack.Interfaces;
using System.Collections.Generic;

namespace LmsGateway.Paystack.Services
{
    public class TransactionStatusService : ITransactionStatusService
    {
        private readonly IRepository<PaystackTransactionStatus> _transactionStatusRepository;

        public TransactionStatusService(IRepository<PaystackTransactionStatus> transactionStatusRepository)
        {
            _transactionStatusRepository = transactionStatusRepository;
        }

        //public void Add(PaystackTransactionStatus transactionStatus)
        //{
        //    if (transactionStatus == null)
        //        throw new ArgumentNullException("transactionStatus");

        //    _transactionStatusRepository.Insert(transactionStatus);
        //}

        //public void AddRange(List<PaystackTransactionStatus> transactionStatusList)
        //{
        //    if (transactionStatusList == null || transactionStatusList.Count <= 0)
        //        throw new ArgumentNullException("transactionStatusList");

        //    _transactionStatusRepository.InsertRange(transactionStatusList);
        //}

        //public void DeleteTransactionStatus(PaystackTransactionStatus transactionStatus)
        //{
        //    if (transactionStatus == null)
        //        throw new ArgumentNullException("transactionStatus");

        //    _transactionStatusRepository.Delete(transactionStatus);
        //}

        public List<PaystackTransactionStatus> GetAllTransactionStatus()
        {
            return _transactionStatusRepository.GetAll();
        }

        //public PaystackTransactionStatus GetTransactionStatusById(int id)
        //{
        //    return _transactionStatusRepository.Get(c => c.Id == id).SingleOrDefault();
        //}

        //public void UpdateTransactionStatus(PaystackTransactionStatus transactionStatus)
        //{
        //    if (transactionStatus == null)
        //        throw new ArgumentNullException("transactionStatus");

        //    _transactionStatusRepository.Update(transactionStatus);
        //}

        //public void DeleteAll()
        //{
        //    List<PaystackTransactionStatus> allTransactionStatus = GetAllTransactionStatus();
        //    if (allTransactionStatus == null)
        //    {
        //        return;
        //    }

        //    foreach (PaystackTransactionStatus transactionStatus in allTransactionStatus)
        //    {
        //        _transactionStatusRepository.Delete(transactionStatus);
        //    }
        //}



    }
}
