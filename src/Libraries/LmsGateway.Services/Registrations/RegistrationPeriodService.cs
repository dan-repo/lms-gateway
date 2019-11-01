using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain.Registrations;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Core.Data;

namespace LmsGateway.Services.Registrations
{
    public class RegistrationPeriodService : IRegistrationPeriodService
    {
        private readonly IRepository<RegistrationPeriod> _registrationPeriodRepository;

        public RegistrationPeriodService(IRepository<RegistrationPeriod> registrationPeriodRepository)
        {
            Guard.NotNull(registrationPeriodRepository, nameof(registrationPeriodRepository));

            _registrationPeriodRepository = registrationPeriodRepository;
        }

        public async Task<List<RegistrationPeriod>> GetAll(string includeProperties = null)
        {
            return await _registrationPeriodRepository.GetAllAsync(includeProperties: includeProperties);
        }
       


    }
}
