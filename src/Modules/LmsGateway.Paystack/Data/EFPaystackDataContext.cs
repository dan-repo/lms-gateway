
using System;
using LmsGateway.Core.Data;
using LmsGateway.Paystack.Data.Mappings;
using LmsGateway.Paystack.Domain;
using Microsoft.EntityFrameworkCore;

namespace LmsGateway.Paystack.Data
{
    public class EFPaystackDataContext : DbContext //, ICustomModelBuilder
    {
        public EFPaystackDataContext(DbContextOptions<EFPaystackDataContext> options)
          : base(options)
        {
            
        }

        //public ModelBuilder Build(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PaystackSupportedCurrency>(x => new PaystackSupportedCurrencyMap(x));
        //    modelBuilder.Entity<PaystackTransactionLog>(x => new PaystackTransactionLogMap(x));
        //    modelBuilder.Entity<PaystackTransactionStatus>(x => new PaystackTransactionStatusMap(x));

        //    return modelBuilder;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //var builder = new ConfigurationBuilder();
        //    //builder.SetBasePath(Directory.GetCurrentDirectory());
        //    //builder.AddJsonFile("appsettings.json");
        //    //IConfiguration Configuration = builder.Build();

        //    //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Data"));
        //    //base.OnConfiguring(optionsBuilder);

        //    //optionsBuilder.UseSqlServer("data source=.;Database=LmsGateway;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("LmsGateway.Paystack"));



        //    optionsBuilder.UseSqlServer("data source=.;Database=LmsGateway;Trusted_Connection=True;MultipleActiveResultSets=true");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var builder = Build(modelBuilder);

            modelBuilder.Entity<PaystackSupportedCurrency>(x => new PaystackSupportedCurrencyMap(x));
            modelBuilder.Entity<PaystackTransactionLog>(x => new PaystackTransactionLogMap(x));
            modelBuilder.Entity<PaystackTransactionStatus>(x => new PaystackTransactionStatusMap(x));

            base.OnModelCreating(modelBuilder);
        }


    }
}
