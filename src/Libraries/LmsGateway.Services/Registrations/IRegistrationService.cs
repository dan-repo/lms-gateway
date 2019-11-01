using LmsGateway.Domain.Registrations;
using System.Threading.Tasks;

namespace LmsGateway.Services.Registrations
{
    public interface IRegistrationService
    {
        Task<Registration> RegisterAsync(Registration registration);
    }
}
