using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Notifications
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);

    }
}
