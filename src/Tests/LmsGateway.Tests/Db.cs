using LmsGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace LmsGateway.Tests
{
    public class Db
    {
        public static EFDataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<EFDataContext>()
               .UseInMemoryDatabase(databaseName: "EFData")
               .Options;

            return new EFDataContext(options);


            //var connection = DbConnectionFactory.CreateTransient();
            //CFAContext context = new CFAContext(connection, true);
            //Database.SetInitializer(new DropCreateDatabaseAlways<CFAContext>());
            //DataSeeder.SeedData(context);

            //return context;
        }

    }
}
