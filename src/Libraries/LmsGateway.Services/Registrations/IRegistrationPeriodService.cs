using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain.Registrations;

namespace LmsGateway.Services.Registrations
{
    public interface IRegistrationPeriodService
    {
        Task<List<RegistrationPeriod>> GetAll(string includeProperties = null);
    }
}
