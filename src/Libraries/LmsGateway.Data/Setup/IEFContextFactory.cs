using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace LmsGateway.Data.Setup
{
    public interface IEFContextFactory
    {
        DbContext GetDbContext(string contextName, IDictionary<string, string> connectionString);
        

    }
}
