using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Interfaces;
using LmsGateway.Core.Data;
using LmsGateway.Paystack.Domain;
using System.Linq.Expressions;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Core.Extensions;

namespace LmsGateway.Paystack.Services
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly IRepository<PaystackTransactionLog> _paystackTransactionLogRepository;

        public TransactionLogService(IRepository<PaystackTransactionLog> paystackTransactionLogRepository)
        {
            _paystackTransactionLogRepository = paystackTransactionLogRepository;
        }

        public List<PaystackTransactionLog> GetLatest500Transactions()
        {
            const int FIVE_HUNDRED = 500;
            Expression<Func<PaystackTransactionLog, bool>> selector = t => t.Id > 0;
            Func<IQueryable<PaystackTransactionLog>, IOrderedQueryable<PaystackTransactionLog>> orderBy = t => t.OrderByDescending(x => x.Id);

            return _paystackTransactionLogRepository.FindBy(selector, orderBy).Take(FIVE_HUNDRED).ToList();
        }

        public async Task<List<PaystackTransactionLog>> GetAllAsync()
        {
            return await _paystackTransactionLogRepository.GetAllAsync();
        }

        public virtual async Task<PaystackTransactionLog> GetByAsync(int transactionId)
        {
            if (transactionId <= 0)
                return null;

            Expression<Func<PaystackTransactionLog, bool>> selector = t => t.Id == transactionId;
            return await _paystackTransactionLogRepository.GetSingleByAsync(selector);
        }
        public virtual async Task<PaystackTransactionLog> GetByReferenceAsync(string transactionReference)
        {
            if (transactionReference == null)
                return null;

            return await _paystackTransactionLogRepository.GetSingleByAsync(x => x.Reference == transactionReference);
        }

        public async Task SaveAsync(PaystackTransactionLog paystackTransactionLog)
        {
            Guard.NotNull(paystackTransactionLog, nameof(paystackTransactionLog));

            await _paystackTransactionLogRepository.AddAsync(paystackTransactionLog);
        }

        public async Task AddRangeAsync(List<PaystackTransactionLog> paystackTransactionLogs)
        {
            Guard.NotNull(paystackTransactionLogs, nameof(paystackTransactionLogs));

            await _paystackTransactionLogRepository.AddRangeAsync(paystackTransactionLogs);
        }

        public async Task<bool> TransactionReferenceExistAsync(string transactionReference)
        {
            bool isExist = false;
            if (!transactionReference.HasValue())
                throw new ArgumentNullException("transactionReference");

            PaystackTransactionLog transactionLog = await GetByReferenceAsync(transactionReference);
            if (transactionLog != null && transactionLog.Reference.HasValue())
            {
                isExist = true;
            }

            return isExist;
        }

        public void Update(PaystackTransactionLog paystackTransactionLog)
        {
            if (paystackTransactionLog == null)
                throw new ArgumentNullException("paystackTransactionLog");

            _paystackTransactionLogRepository.Update(paystackTransactionLog);
        }

        public void DeleteAll(IList<PaystackTransactionLog> transactionLogs)
        {
            if (transactionLogs == null)
            {
                throw new ArgumentNullException("transactionLogs");
            }

            foreach (PaystackTransactionLog transactionLog in transactionLogs)
            {
                _paystackTransactionLogRepository.Delete(transactionLog);
            }
        }


    }
}
