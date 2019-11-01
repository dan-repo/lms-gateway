using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LmsGateway.Data.Setup
{
    public class EFContextFactory : IEFContextFactory
    {
        public DbContext GetDbContext(string contextName, IDictionary<string, string> connectionString)
        {
            DbContext dbContext = null;

            switch (contextName)
            {
                case nameof(EFDataContext):
                    {
                        DbContextOptionsBuilder<EFDataContext> optionsBuilder = new DbContextOptionsBuilder<EFDataContext>();
                        optionsBuilder.UseSqlServer(connectionString["Data"]);
                        dbContext = new EFDataContext(optionsBuilder.Options);
                        break;
                    }
                case nameof(EFIdentityContext):
                    {
                        DbContextOptionsBuilder<EFIdentityContext> optionsBuilder = new DbContextOptionsBuilder<EFIdentityContext>();
                        optionsBuilder.UseSqlServer(connectionString["Identity"]);
                        dbContext = new EFIdentityContext(optionsBuilder.Options);
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            return dbContext;
        }
    }
}
