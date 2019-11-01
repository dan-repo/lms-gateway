using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Domain;
using LmsGateway.Paystack.Interfaces;
using LmsGateway.Core.Data;
using LmsGateway.Core.Infrastructure;

namespace LmsGateway.Paystack.Services
{
    public class SupportedCurrencyService : ISupportedCurrencyService
    {
        private readonly IRepository<PaystackSupportedCurrency> _supportedCurrencyRepository;

        public SupportedCurrencyService(IRepository<PaystackSupportedCurrency> supportedCurrencyRepository)
        {
            _supportedCurrencyRepository = supportedCurrencyRepository;
        }

        public async Task Add(PaystackSupportedCurrency supportedCurrency)
        {
            Guard.NotNull(supportedCurrency, nameof(supportedCurrency));

            await _supportedCurrencyRepository.AddAsync(supportedCurrency);
        }

        public async Task AddRange(List<PaystackSupportedCurrency> supportedCurrencies)
        {
            Guard.NotNull(supportedCurrencies, nameof(supportedCurrencies));

            foreach (PaystackSupportedCurrency supportedCurrency in supportedCurrencies)
            {
                await Add(supportedCurrency);
            }
        }

        public async Task<List<PaystackSupportedCurrency>> GetAllCurrencies()
        {
            return await _supportedCurrencyRepository.GetAllAsync();
        }

        public async Task<List<PaystackSupportedCurrency>> GetSupportedCurrencies()
        {
            return await _supportedCurrencyRepository.FindByAsync(c => c.IsSupported == true);
        }

        public async Task<PaystackSupportedCurrency> GetSupportedCurrencyByCode(int code)
        {
            return await _supportedCurrencyRepository.GetSingleByAsync(c => c.Code == code);
        }

        public async Task<PaystackSupportedCurrency> GetSupportedCurrencyById(int id)
        {
            return await _supportedCurrencyRepository.GetSingleByAsync(c => c.Id == id);
        }

        public void UpdateSupportedCurrency(PaystackSupportedCurrency supportedCurrency)
        {
            Guard.NotNull(supportedCurrency, nameof(supportedCurrency));

            //_supportedCurrencyRepository.Update(supportedCurrency);
        }

        public void DeleteSupportedCurrency(PaystackSupportedCurrency supportedCurrency)
        {
            Guard.NotNull(supportedCurrency, nameof(supportedCurrency));

            //_supportedCurrencyRepository.Delete(supportedCurrency);
        }

        public async Task DeleteAll()
        {
            List<PaystackSupportedCurrency> supportedCurrencies = await GetAllCurrencies();
            if (supportedCurrencies == null)
            {
                return;
            }

            foreach (PaystackSupportedCurrency supportedCurrency in supportedCurrencies)
            {
                //_supportedCurrencyRepository.Delete(supportedCurrency);
            }
        }

    }
}
