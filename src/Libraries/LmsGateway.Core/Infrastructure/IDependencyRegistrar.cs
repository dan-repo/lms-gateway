using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace LmsGateway.Core.Infrastructure
{
    public interface IDependencyRegistrar
    {
        int Order { get; }
        void Register(IServiceCollection service, string connectionString = null);

    }
}
