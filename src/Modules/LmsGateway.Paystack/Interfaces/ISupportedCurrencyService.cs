using LmsGateway.Paystack.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LmsGateway.Paystack.Interfaces
{
    public interface ISupportedCurrencyService
    {
        Task<List<PaystackSupportedCurrency>> GetAllCurrencies();
        Task Add(PaystackSupportedCurrency supportedCurrency);
        Task AddRange(List<PaystackSupportedCurrency> supportedCurrencies);
        Task<List<PaystackSupportedCurrency>> GetSupportedCurrencies();
        Task<PaystackSupportedCurrency> GetSupportedCurrencyById(int id);
        Task<PaystackSupportedCurrency> GetSupportedCurrencyByCode(int code);
        void UpdateSupportedCurrency(PaystackSupportedCurrency supportedCurrency);
        void DeleteSupportedCurrency(PaystackSupportedCurrency supportedCurrency);
        Task DeleteAll();


    }
}
