using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain.Registrations;

namespace LmsGateway.Services.Registrations
{
    public interface IRegistrationFeeService
    {
        Task<List<RegistrationFee>> GetAll(string includeProperties = null);
        Task<List<RegistrationFee>> GetByPeriodId(int periodId, string includeProperties = null);
        Task<RegistrationFee> GetById(int id, string includeProperties = null);
    }



}
