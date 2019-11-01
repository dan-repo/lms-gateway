using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Core.Data;
using LmsGateway.Domain.Registrations;
using LmsGateway.Core.Infrastructure;

namespace LmsGateway.Services.Registrations
{
    public class RegistrationFeeService : IRegistrationFeeService
    {
        private readonly IRepository<RegistrationFee> _registrationFeeRepository;

        public RegistrationFeeService(IRepository<RegistrationFee> registrationFeeRepository)
        {
            Guard.NotNull(registrationFeeRepository, nameof(registrationFeeRepository));

            _registrationFeeRepository = registrationFeeRepository;
        }
        
        public async Task<List<RegistrationFee>> GetAll(string includeProperties = null)
        {
            return await _registrationFeeRepository.GetAllAsync(includeProperties: includeProperties);
        }

        public async Task<List<RegistrationFee>> GetByPeriodId(int periodId, string includeProperties = null)
        {
            return await _registrationFeeRepository.FindByAsync(x => x.RegistrationPeriodId == periodId, includeProperties: includeProperties);
        }

        public async Task<RegistrationFee> GetById(int id, string includeProperties = null)
        {
            return await _registrationFeeRepository.GetSingleByAsync(x => x.Id == id, includeProperties: includeProperties);
        }


    }
}
