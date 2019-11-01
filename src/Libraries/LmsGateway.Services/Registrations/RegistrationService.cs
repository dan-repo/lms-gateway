using LmsGateway.Core.Data;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Domain.Registrations;
using LmsGateway.Services.Configuration;
using LmsGateway.Services.Registrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Services.Registrations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<Registration> _registrationRepository;

        public RegistrationService(IRepository<Registration> registrationRepository)
        {
            Guard.NotNull(registrationRepository, nameof(registrationRepository));

            _registrationRepository = registrationRepository;
        }
                
        public async Task<Registration> RegisterAsync(Registration registration)
        {
            Registration newRegistration = null;
            try
            {
                newRegistration = await _registrationRepository.AddAsync(registration);
            }
            catch(Exception ex)
            {
                string e = ex.ToString();
            }

            return newRegistration;
        }









    }
}
