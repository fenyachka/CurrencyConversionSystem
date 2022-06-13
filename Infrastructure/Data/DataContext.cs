using System.Reflection;
using Domain.Entities.Currencies;
using Domain.Entities.Customer;
using Domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }
        
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Customer>().HasIndex(e => e.PersonalNumber);
            modelBuilder.Entity<Transaction>().HasIndex(e => e.PersonalNumber);
        }
    }
}